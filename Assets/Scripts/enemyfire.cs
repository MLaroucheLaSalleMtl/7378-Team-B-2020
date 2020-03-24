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
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
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

        timer += Time.deltaTime;
        if (timer >= waittime)
        {
            Debug.Log("fire");
            //this.gameObject.GetComponent<AudioSource>().Play();
            GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
        }
        if (timer > waittime)
        {
            timer = 0f;
        }


    }
    public void TurretRotation()
    {
        direction = playerturret.position - turret.position;
        rotation = Quaternion.LookRotation(direction);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * rotationspeed);
        
        if (Physics.Raycast(generator.position, turret.forward, out hit, 40))
        {
            Debug.Log("fire111");
            Debug.Log(hit.transform.name);
            if (hit.transform.tag=="Player")
            {
                
                fire();
            }
        }
    }
}
