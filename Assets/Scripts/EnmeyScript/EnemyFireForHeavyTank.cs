using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireForHeavyTank : MonoBehaviour
{
    public EnemyMovementForHeavyTank EnemyMovementForHeavyTank;
    private AudioSource AudioSource;
    
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
    
    RaycastHit hit;
    [Header("predictive Shooting")]
    public Vector3 VelocityofPlayer;
    public Rigidbody PlayerRigi;
    public float PredictiveTime;
    public float FireWaitTime = 10f;
    public float time;
    public bool Fired = false;

    [SerializeField] private int maxAIShellCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerRigi = player.GetComponent<Rigidbody>();
        AudioSource = this.GetComponent<AudioSource>();
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
        AudioSource.Play();
        Debug.Log("Nomal Fire");
        GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
    }
    
    public void TurretRotation()
    {
        VelocityofPlayer = PlayerRigi.velocity;
        direction = player.position - turret.position;
        rotation = Quaternion.LookRotation((direction + VelocityofPlayer) * PredictiveTime);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * rotationspeed);
        time = time + Time.deltaTime;
        if (time >= FireWaitTime&& !EnemyMovementForHeavyTank.collider_exist)
        {
            
                Fired = true;
                fire();
                time = 0;
            
            

        }
        else
        {
            
            Fired = false;
        }
    }

   


    public void CalculateTime(float distance, float speedofshell)
    {
        PredictiveTime = distance / speedofshell;
    }
}