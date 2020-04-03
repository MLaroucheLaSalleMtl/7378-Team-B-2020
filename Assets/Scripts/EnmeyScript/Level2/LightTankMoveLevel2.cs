using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LightTankMoveLevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isdetected = false;
    public float Detect = 50f;
    public float DistanceBetweenTwo;
    public Transform player;
    public Vector3 destination;
    
    NavMeshAgent enemy;
    public GameManagerLelvel2 GameManagerLelvel2;
    Rigidbody enemyrigi;
    Vector3 velocity = Vector3.zero;

    [Header("PathFinding")]
    public float randomX;
    public float randomY;
    public float decideX;
    public float decideY;
    NavMeshPath path;
    public bool pathfind = false;
    public float Randomtime;
    public float time;

    RaycastHit linehit;
    public bool collider_exist = false;
    public LayerMask LineMask;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<NavMeshAgent>();
        enemyrigi = this.GetComponent<Rigidbody>();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        DetectedDistance();
        DetectCollider();
        if(!pathfind&&isdetected)
        {
            ChooseDestination();

        }
        else
        {
            if(Vector3.Distance(destination,transform.position)<2)
            {
                pathfind = false;
            }
            else
            {
                Randomtime = Random.Range(10, 20);
                time = time + Time.deltaTime;
                if (time >= Randomtime)
                {
                    pathfind = false;
                    time = 0f;
                }
            }
            
        }
        
        
    }

    public void DetectedDistance()
    {
        DistanceBetweenTwo = Vector3.Distance(transform.position, player.position);


        if (DistanceBetweenTwo <= Detect)
        {
            isdetected = true;
        }//if the distance between player and enemy is lower than detect_distance, the player is detected
    }
    public void SetDestination()
    {

        enemy.SetDestination(destination);
    }
    public void DetectCollider()
    {
        if (Physics.Linecast(transform.position, player.position, out linehit, LineMask))
        {
            Debug.Log(linehit.transform.tag+ linehit.transform.name);
            collider_exist = true;
        }
        else
        {
            collider_exist = false;
        }
    }
    public void ChooseDestination()
    {
        randomX = Random.Range(10, 15);
        randomY = Random.Range(10,15);
        decideX = Random.Range(0, 99);
        decideY = Random.Range(0, 99);
        if (decideX<=49)
        {
            randomX = -randomX;
        }


        if ( decideY<= 49)
        {
            randomY = -randomY;
        }

        destination = player.transform.position + player.transform.right * randomX + player.transform.forward * randomY;
        
        if (enemy.CalculatePath(destination, path))
        {
            enemy.SetPath(path);
            pathfind = true;
        }
        else
        {
            ChooseDestination();
        }
    }
    
}
