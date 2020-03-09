using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public GameObject explode;
    public GameObject self;
    public float speed = 10;
    public float timer = 0;
    public float waittime = 1;
    public bool collided = false;
    public bool onlyonce=false;
    RaycastHit hit;
    public float ExplodeRadius = 10;
    public LayerMask LayerMask;
    public float ExplodeDistance;//the distance between shell and enemy, it is used to figure out how much damage the enemy will take
    public EnemyHealth EnemyHealth;
    void Start()
    {
        
    }


    private void Update()
    {
        if(collided)
        {
            if(onlyonce==false)
            {
                timer += Time.deltaTime;
                if (timer >= waittime)
                {

                    self.SetActive(false);
                }
                if (timer > waittime)
                {

                    onlyonce = true;
                }
            }
           
        }
        
    }
    // Update is called once per frame


   
    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        Instantiate(explode, self.transform.position,Quaternion.identity);
        if (Physics.SphereCast(self.transform.position, ExplodeRadius, self.transform.forward, out hit, 0, LayerMask))
        {
            if( hit.transform.tag=="Enemy")
            {
                ExplodeDistance = Vector3.Distance(self.transform.position, hit.transform.position);
                if(ExplodeDistance<3)
                {
                    EnemyHealth.DoDamage();
                }
            }
        }

        
    }

}


