using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfire : MonoBehaviour
{

    public enemy_movement enemy_Movement;
    public bool isfiring = false;
    public Transform player;
    public Transform fire_position;
    public GameObject prefab;
    private float speed = 250;
    public float timer=0f;
    public float waittime = 2f;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_Movement.isdetected==true)
        {
            isfiring = true;
        }


        if(isfiring)
        {
           
            fire();
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
        if(timer>waittime)
        {
            timer = 0f;
        }


    }
}
