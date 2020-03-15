using UnityEngine;
using System.Collections;

public class MoveTank : MonoBehaviour
{
    public Rigidbody FrontLeft;
    public Rigidbody RearLeft;
    public Rigidbody FrontRight;
    public Rigidbody RearRight;
    public Transform hull;

    private TankTrackAnimation[] tracks;
    //
    //	public Rigidbody LeftRoll;
    //	public Rigidbody RightRoll;
    //
    private int wheelTorque;

    private int fLDir;
    private int fRDir;
    private int rLDir;
    private int rRDir;

    private void Start()
    {
        tracks = GetComponentsInChildren<TankTrackAnimation>();
    }
    void Update()
    {

    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            wheelTorque = 10000;
            //hull.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1) * 55000);
            foreach (TankTrackAnimation track in tracks)
            {
                track.MoveTrack(new Vector2(-1f, 0));
            }
            FrontLeft.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
            RearLeft.AddRelativeTorque(Vector3.right*wheelTorque, ForceMode.Acceleration);
            FrontRight.AddRelativeTorque(Vector3.right * wheelTorque, ForceMode.Acceleration);
            RearRight.AddRelativeTorque(Vector3.right * wheelTorque, ForceMode.Acceleration);
            //LeftRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
            //RightRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.S))
        {
            foreach (TankTrackAnimation track in tracks)
            {
                track.MoveTrack(new Vector2(0.5f, 0));
            }
            hull.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -25000);
            if (Input.GetKey(KeyCode.A))
            {
                hull.transform.Rotate(0, 35 * Time.deltaTime, 0, Space.Self);
            }
            if (Input.GetKey(KeyCode.D))
            {
                hull.transform.Rotate(0, -35 * Time.deltaTime, 0, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hull.transform.Rotate(0, -35 * Time.deltaTime, 0, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hull.transform.Rotate(0, 35 * Time.deltaTime, 0, Space.Self);
        }
    }
}
