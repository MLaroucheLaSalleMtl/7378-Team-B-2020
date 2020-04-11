using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireForHeavyTank : MonoBehaviour
{
    public EnemyMovementForHeavyTank EnemyMovementForHeavyTank;

    public Transform playerturret;
    public Transform generator;
    public Transform turret;
    public bool isfiring = false;
    public Transform player;
    public Transform fire_position;
    public GameObject prefab;
    private float speed = 50;

    public Vector3 direction;
    public Quaternion rotation;
    public float rotationspeed = 0.01f;
    public LayerMask LayerMask;
    RaycastHit hit;
    [Header("predictive Shooting")] public Vector3 VelocityofPlayer;
    public Rigidbody PlayerRigi;
    public float PredictiveTime;
    public float FireWaitTime = 10f;
    public float time;
    public bool Fired = false;

    [SerializeField] private int maxAIShellCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigi = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime(EnemyMovementForHeavyTank.DistanceBetweenTwo, speed);


        Debug.DrawRay(generator.position, generator.forward, Color.red);
        if (EnemyMovementForHeavyTank.isdetected == true)
        {
            isfiring = true;
        }


        if (isfiring)
        {
            TurretRotation();
        }
    }

    public void fire()
    {
        Debug.Log("Nomal Fire");
        GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
    }
    IEnumerator ContinueFire()
    {
        Debug.Log("Continue Fire");
        for (int i = 0; i < maxAIShellCount; i++)
        {
            GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
            yield return 1f;
        }
        time = 0f;
       
    }
    public void TurretRotation()
    {
        VelocityofPlayer = PlayerRigi.velocity;
        direction = playerturret.position - turret.position;
        rotation = Quaternion.LookRotation((direction + VelocityofPlayer) * PredictiveTime);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * rotationspeed);
        time = time + Time.deltaTime;
        if (time >= FireWaitTime)
        {
            ContinueAttackAI();
          //  fire();
            Fired = true;
          
        }
        else
        {
            Fired = false;
        }
    }

    private void ContinueAttackAI()
    {
        int randomVal = UnityEngine.Random.Range(0, 2);
        switch (randomVal)
        {
        case 0:
            fire();
            time = 0f;
            break;
        case 1:
            StartCoroutine(ContinueFire());
            break;
        }
    }


    public void CalculateTime(float distance, float speedofshell)
    {
        PredictiveTime = distance / speedofshell;
    }
}