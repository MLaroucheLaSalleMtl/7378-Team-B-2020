using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfire : MonoBehaviour
{

    public enemy_movement enemy_Movement;
    
    public Transform playerturret;
    public Transform generator;
    public Transform turret;
    public bool isfiring = false;
    public Transform player;
    public Transform fire_position;
    public GameObject prefab;
    private float speed = 50;
    public float timer=0f;
    public float waittime = 2f;
    public Vector3 direction;
    public Quaternion rotation;
    public float rotationspeed=0.01f;
    public LayerMask LayerMask;
    RaycastHit hit;
    [Header("predictive Shooting")]
    public Vector3 VelocityofPlayer;
    public Rigidbody PlayerRigi;
    public float PredictiveTime;



    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerturret = player.Find("TurrentBase");
        PlayerRigi = player.GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime(enemy_Movement.DistanceBetweenTwo, speed);




        Debug.DrawRay(generator.position, generator.forward, Color.red);
        if (enemy_Movement.isdetected == true)
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

        
            
           
            //this.gameObject.GetComponent<AudioSource>().Play();
            GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
            
        
        //if (timer > waittime)
        //{
        //    timer = 0f;
        //}


    }
    public void TurretRotation()
    {
        VelocityofPlayer = PlayerRigi.velocity;
       
        direction = player.position - turret.position;
        rotation = Quaternion.LookRotation((direction+  VelocityofPlayer)*PredictiveTime);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * rotationspeed);

        //if (Physics.Raycast(generator.position, turret.forward, out hit, 40))
        //{
        timer += Time.deltaTime;
        if (timer >= waittime)
        {
            fire();
            timer = 0;
        }

            //if (hit.transform.tag=="Player")
            //{

            
            //}
        //}
    }


    public void CalculateTime(float distance,float speedofshell)
    {
        PredictiveTime = distance / speedofshell;
        
    }
}
