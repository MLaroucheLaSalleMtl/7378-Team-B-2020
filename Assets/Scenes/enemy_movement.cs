using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemy_movement : MonoBehaviour
{
    public Transform player;
    NavMeshAgent enemy;
    Vector3 destination;
    public float distance=50;//the enemy will get close to the player and stop "distance" away from player
    RaycastHit hit;
    RaycastHit linehit;
    public LayerMask LayerMask;
    public LayerMask layerMask;
    public bool ReachDestination = false;
    float timer = 0f;
    public float waittime = 3f;
    public bool collider_exist = false;
    public float DistanceBetweenTwo;

    public bool isdetected = false;
    public float detect_distance = 50;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceBetweenTwo= Vector3.Distance(transform.position, player.position);
        

        if(DistanceBetweenTwo<=detect_distance)
        {
            isdetected = true;
        }






        if((ReachDestination==false||collider_exist==true)&&isdetected==true)
        {

            timer += Time.deltaTime;
            if (timer > waittime)
            {
                destination = player.transform.position;
                SetDestination();
            }

        }
        else if(ReachDestination==true&&collider_exist==false)
        {

            destination = transform.position;
            SetDestination();
        }
        
        if (DistanceBetweenTwo<=distance)
        {
            
                timer = 0f;
                
                ReachDestination = true;
            
            
        }
        else
        {
            ReachDestination = false;
        }


        if(Physics.Linecast(transform.position,player.position,out linehit,LayerMask))
        {
            collider_exist = true;
        }
        else
        {
            collider_exist = false;
        }
        

    }
    public void SetDestination()
    {
        
        enemy.SetDestination(destination);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
