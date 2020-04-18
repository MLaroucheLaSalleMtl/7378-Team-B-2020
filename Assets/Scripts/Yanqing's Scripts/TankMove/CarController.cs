using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
internal enum CarDriveType
{
    FrontWheelDrive,
    RearWheelDrive,
    FourWheelDrive
}

internal enum SpeedType
{
    MPH,
    KPH
}
internal enum Direction
{
    idle,
    forward,
    backward
}

public class CarController : MonoBehaviour
{
    [SerializeField] private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive;
    [SerializeField] private GameObject Hull;
    [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4];
    [SerializeField] private GameObject[] m_WheelMeshes = new GameObject[4];
    [SerializeField] private WheelEffects[] m_WheelEffects = new WheelEffects[4];
    [SerializeField] private GameObject[] m_TrackBones = new GameObject[12];
    [SerializeField] private Vector3 m_CentreOfMassOffset;
    [SerializeField] private float m_MaximumSteerAngle;
    [Range(0, 1)] [SerializeField] private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
    [Range(0, 1)] [SerializeField] private float m_TractionControl; // 0 is no traction control, 1 is full interference
    [SerializeField] private float m_FullTorqueOverAllWheels;
    [SerializeField] private float m_ReverseTorque;
    [SerializeField] private float m_MaxHandbrakeTorque;
    [SerializeField] private float m_Downforce = 100f;
    [SerializeField] private SpeedType m_SpeedType;
    [SerializeField] private float m_Topspeed = 200;
    [SerializeField] private static int NoOfGears = 5;
    [SerializeField] private float m_RevRangeBoundary = 1f;
    [SerializeField] private float m_SlipLimit;
    [SerializeField] private float m_BrakeTorque;
    [SerializeField] private float m_BoneOffset;
    [SerializeField] private float m_TurnRate;


    private Quaternion[] m_WheelMeshLocalRotations;
    private Vector3 m_Prevpos, m_Pos;
    private float m_SteerAngle;
    private int m_GearNum;
    private float m_GearFactor;
    private float m_OldRotation;
    private float m_CurrentTorque;
    private bool triggerAnime;
    public float m_PrevSpeed = 0;
    private Direction current_State;

    public Rigidbody m_Rigidbody;
    public Animator anim;
    public AudioSource engine;
    private const float k_ReversingThreshold = 0.01f;

    public bool Skidding { get; private set; }
    public float BrakeInput { get; private set; }
    public float CurrentSteerAngle { get { return m_SteerAngle; } }
    public float CurrentSpeed { get { return m_Rigidbody.velocity.magnitude * 3.6f; } }
    public float MaxSpeed { get { return m_Topspeed; } }
    public float Revs { get; private set; }
    public float AccelInput { get; private set; }

    // Use this for initialization
    public void Start()
    {
        m_WheelMeshLocalRotations = new Quaternion[18];
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;
        }
        m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;

        m_MaxHandbrakeTorque = float.MaxValue;

        //m_Rigidbody = this.GetComponent<Rigidbody>();
        m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl * m_FullTorqueOverAllWheels);

        triggerAnime = false;
    }


    private void GearChanging()
    {
        float f = Mathf.Abs(CurrentSpeed / MaxSpeed);
        float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
        float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

        if (m_GearNum > 0 && f < downgearlimit)
        {
            m_GearNum--;
        }

        if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
        {
            m_GearNum++;
        }
    }


    // simple function to add a curved bias towards 1 for a value in the 0-1 range
    private static float CurveFactor(float factor)
    {
        return 1 - (1 - factor) * (1 - factor);
    }


    // unclamped version of Lerp, to allow value to exceed the from-to range
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }


    private void CalculateGearFactor()
    {
        float f = (1 / (float)NoOfGears);
        // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
        // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
        var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(CurrentSpeed / MaxSpeed));
        m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
    }


    private void CalculateRevs()
    {
        // calculate engine revs (for display / sound)
        // (this is done in retrospect - revs are not used in force/power calculations)
        CalculateGearFactor();
        var gearNumFactor = m_GearNum / (float)NoOfGears;
        var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
        var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
        Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
    }

    private void FixedUpdate()
    {
        if (CurrentSpeed < 0.1f)
        {
            current_State = Direction.idle;
        }
    }

    public void Move(float steering, float accel, float footbrake, float handbrake)
    {
        StartCoroutine(Savespeed(CurrentSpeed));
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            Quaternion quat;
            Vector3 position;
            m_WheelColliders[i].GetWorldPose(out position, out quat);//-0.66802
            Vector3 b_Pos = m_WheelColliders[i].transform.parent.InverseTransformPoint(position);
            b_Pos.y -= m_BoneOffset;
            //Vector3 b_Pos = new Vector3(position.x, position.y - m_BoneOffset, position.z);
            m_TrackBones[i].transform.localPosition = b_Pos;
            m_WheelMeshes[i].transform.position = position;
            foreach (GameObject wm in m_WheelMeshes)
            {
                wm.transform.rotation = quat;
            }
            //m_WheelMeshes[i].transform.rotation = quat;
        }
        if(accel > 0)
        {
            anim.SetFloat("v", accel);
        }
        else if(footbrake < 0)
        {
            anim.SetFloat("v", footbrake);
        }


        //StopAnimation(Direction.forward);
        //clamp input values
        steering = Mathf.Clamp(steering, -1, 1);
        AccelInput = accel = Mathf.Clamp(accel, 0, 1);
        BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);
        handbrake = Mathf.Clamp(handbrake, 0, 1);

        SteerHelper(steering, footbrake);


        if(Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f)
        {
            current_State = Direction.forward;
        }
        else if(Vector3.Angle(-transform.forward, m_Rigidbody.velocity) < 50f)
        {
            current_State = Direction.backward;
        }


        switch(current_State)
        {
            case Direction.forward:
                StopAnimation(Direction.forward);
                break;
            case Direction.backward:
                StopAnimation(Direction.backward);
                break;
        }
        SoundControl();
        ApplyDrive(accel, footbrake);
        CapSpeed(); 


        //Set the handbrake.
        if ((accel == 0 && footbrake == 0) || handbrake > 0f)
        {
            var hbTorque = 1 * m_MaxHandbrakeTorque;
            for (int i = 0; i < m_WheelColliders.Length; i++)
            {
                m_WheelColliders[i].brakeTorque = hbTorque;
            }
        }


        CalculateRevs();
        GearChanging();

        AddDownForce();
        CheckForWheelSpin();
        TractionControl();

    }
    IEnumerator Savespeed(float CurrentSpeed)
    {
        yield return new WaitForSeconds(0.1f);
        m_PrevSpeed = CurrentSpeed;
    }

    private void StopAnimation(Direction direction)
    {
        float speedDiff = m_PrevSpeed - CurrentSpeed;
        //print("CurrentSpeed is: " + CurrentSpeed);
        if (CurrentSpeed > 10)
        {
            triggerAnime = true;
        }
        else
        {
            triggerAnime = false;
        }
        //if (Input.GetKeyDown(KeyCode.Space) && triggerAnime)
        //{
        //    anim.SetTrigger("Stop");
        //}
        if (triggerAnime && speedDiff >= 10)
        {
            //anim.SetBool("Stop",true);
            //print("Current: " + CurrentSpeed + "  1s before: " + m_PrevSpeed + "SpeedDiff is: " + speedDiff);
            switch (direction)
            {
                case Direction.forward:
                    anim.SetBool("Stop", true);
                    triggerAnime = false;
                    break;
                case Direction.backward:
                    anim.SetBool("BStop", true);
                    triggerAnime = false;
                    break;
            }
        }
        else
        {
            anim.SetBool("Stop", false);
            anim.SetBool("BStop", false);
        }
    }

    private void CapSpeed()
    {
        float speed = m_Rigidbody.velocity.magnitude;
        switch (m_SpeedType)
        {
            case SpeedType.MPH:

                speed *= 2.23693629f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 2.23693629f) * m_Rigidbody.velocity.normalized;
                break;

            case SpeedType.KPH:
                speed *= 3.6f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 3.6f) * m_Rigidbody.velocity.normalized;
                break;
        }
    }


    private void ApplyDrive(float accel, float footbrake)
    {
        float thrustTorque = accel * (m_CurrentTorque / 4f); ;
        float reverseTorque = -m_ReverseTorque * footbrake;
        //print("accel:" + accel + " footbrake: " + footbrake);
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            if(accel > 0)
            {
                if (CurrentSpeed > 5 && Vector3.Angle(-transform.forward, m_Rigidbody.velocity) < 50f)
                {
                    m_WheelColliders[i].brakeTorque = m_BrakeTorque * accel;
                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.S))
                    {
                        m_WheelColliders[i].brakeTorque = m_MaxHandbrakeTorque;
                    }
                    else
                    {
                        m_WheelColliders[i].brakeTorque = 0;
                        m_WheelColliders[i].motorTorque = thrustTorque;
                    }
                }
            }
            if (footbrake > 0)
            {
                if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f) //backward while forwarding
                {
                    m_WheelColliders[i].brakeTorque = m_BrakeTorque * footbrake;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        m_WheelColliders[i].brakeTorque = m_MaxHandbrakeTorque;
                    }
                    else
                    {
                        m_WheelColliders[i].brakeTorque = 0;
                        m_WheelColliders[i].motorTorque = reverseTorque;
                    }
                }
            }        
        }
    }

    public void DefaultBrake(float handbrake)
    {
        var hbTorque = handbrake * m_MaxHandbrakeTorque;
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            m_WheelColliders[i].brakeTorque = hbTorque;
        }
    }

    private void SteerHelper(float steering, float footbrake)
    {
        if (footbrake > 0)
        {
            //foreach (TankTrackAnimation track in tracks)
            //{
            //    track.MoveTrack(new Vector2(0.5f, 0));
            //}
            if (steering < 0)
            {
                Hull.transform.Rotate(0, m_TurnRate * Time.deltaTime, 0, Space.Self);
            }
            if (steering > 0)
            {
                Hull.transform.Rotate(0, -m_TurnRate * Time.deltaTime, 0, Space.Self);
            }
        }
        else if (steering < 0)
        {
            Hull.transform.Rotate(0, -m_TurnRate * Time.deltaTime, 0, Space.Self);
        }
        else if (steering > 0)
        {
            Hull.transform.Rotate(0, m_TurnRate * Time.deltaTime, 0, Space.Self);
        }
    }


    // this is used to add more grip in relation to speed
    private void AddDownForce()
    {
        m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * m_Downforce *
                                                     m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
    }


    // checks if the wheels are spinning and is so does three things
    // 1) emits particles
    // 2) plays tiure skidding sounds
    // 3) leaves skidmarks on the ground
    // these effects are controlled through the WheelEffects class
    private void CheckForWheelSpin()
    {
        // loop through all wheels
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            WheelHit wheelHit;
            m_WheelColliders[i].GetGroundHit(out wheelHit);

            // is the tire slipping above the given threshhold
            if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit)
            {
                //m_WheelEffects[i].EmitTyreSmoke();

                // avoiding all four tires screeching at the same time
                // if they do it can lead to some strange audio artefacts
                if (!AnySkidSoundPlaying())
                {
                    m_WheelEffects[i].PlayAudio();
                }
                continue;
            }

            //// if it wasnt slipping stop all the audio
            //if (m_WheelEffects[i].PlayingAudio)
            //{
            //    m_WheelEffects[i].StopAudio();
            //}
            // end the trail generation
            //m_WheelEffects[i].EndSkidTrail();
        }
    }

    // crude traction control that reduces the power to wheel if the car is wheel spinning too much
    private void TractionControl()
    {
        WheelHit wheelHit;
        switch (m_CarDriveType)
        {
            case CarDriveType.FourWheelDrive:
                // loop through all wheels
                for (int i = 0; i < m_WheelColliders.Length; i++)
                {
                    m_WheelColliders[i].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                }
                break;

            case CarDriveType.RearWheelDrive:
                m_WheelColliders[2].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);

                m_WheelColliders[3].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);
                break;

            case CarDriveType.FrontWheelDrive:
                m_WheelColliders[0].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);

                m_WheelColliders[1].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);
                break;
        }
    }


    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= m_SlipLimit && m_CurrentTorque >= 0)
        {
            m_CurrentTorque -= 10 * m_TractionControl;
        }
        else
        {
            m_CurrentTorque += 10 * m_TractionControl;
            if (m_CurrentTorque > m_FullTorqueOverAllWheels)
            {
                m_CurrentTorque = m_FullTorqueOverAllWheels;
            }
        }
    }

    private void SoundControl()
    {
        float offset = CurrentSpeed / this.MaxSpeed;
        engine.pitch = 0.6f * offset + 1;
    }
    private bool AnySkidSoundPlaying()
    {
        for (int i = 0; i < 4; i++)
        {
            //if (m_WheelEffects[i].PlayingAudio)
            {
                return true;
            }
        }
        return false;
    }
}
