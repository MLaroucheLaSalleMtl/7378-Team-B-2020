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
    public LayerMask LayerMask;
    public bool ReachDestination = false;
    float timer = 0f;
    public float waittime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ReachDestination!=true)
        {
            timer += Time.deltaTime;
            if (timer > waittime)
            {
                destination = player.transform.position;
                SetDestination();
            } 
            
        }
        
        if (Physics.SphereCast(transform.position, distance, transform.forward, out hit, 60,LayerMask))
        {
            Debug.Log(hit.transform.tag);
            if(hit.transform.tag=="Player")
            {
                timer = 0f;
                destination = transform.position;
                ReachDestination = true;
            }
            
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
