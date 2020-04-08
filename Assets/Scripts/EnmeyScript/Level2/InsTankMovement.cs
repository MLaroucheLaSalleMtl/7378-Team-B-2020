using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class InsTankMovement : MonoBehaviour
{
    [Header("The First Nav")]
    public float Firsttimer = 0f;
    public bool OnlyRunOneTime = false;
    [Header("ForNav")]
    public Transform player;
    NavMeshAgent enemy;
    Vector3 destination;
    public float distance = 50;//the enemy will get close to the player and stop "distance" away from player
    RaycastHit hit;
    RaycastHit linehit;
    public LayerMask LineMask;

    public bool ReachDestination = false;
    public float timer = 0f;
    public float waittime = 3f;
    public bool collider_exist = false;
    public float DistanceBetweenTwo;

    public bool isdetected = false;
    public float detect_distance = 50;

    [Header("ForDifferentStates")]
    public float DestinationDistance = 5f;//it is used to figure out where the enemy goes
    public int EnemyDecision = 0;
    public int EnemyPreviousDecision = 10;
    public float StateTime = 0f;
    public float RandomNumber = 0f;
    public bool SetDestinationOnce=false;
    [Header("Death")]
    public enemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        StateTime = Random.Range(20, 40);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        DetectedDistance();
        DetectCollider();
        
            if (isdetected)
            {
                timer += Time.deltaTime;
                if (timer > StateTime)
                {
                    SetDestinationOnce = false;
                    StateTime = Random.Range(20, 40);
                    RandomNumber = Random.Range(0, 99);
                    {
                        if (EnemyPreviousDecision == 0)
                        {
                            if (RandomNumber <= 49)
                            {
                                EnemyDecision = 2;
                            }
                            else
                            {
                                EnemyDecision = 3;
                            }
                        }
                        else if (EnemyPreviousDecision == 1)
                        {
                            if (RandomNumber <= 49)
                            {
                                EnemyDecision = 2;
                            }
                            else
                            {
                                EnemyDecision = 3;
                            }
                        }
                        else if (EnemyPreviousDecision == 2)
                        {
                            if (RandomNumber <= 49)
                            {
                                EnemyDecision = 0;
                            }
                            else
                            {
                                EnemyDecision = 1;
                            }
                        }
                        else if (EnemyPreviousDecision == 3)
                        {
                            if (RandomNumber <= 49)
                            {
                                EnemyDecision = 0;
                            }
                            else
                            {
                                EnemyDecision = 1;
                            }
                        }
                    }

                    timer = 0;
                }
                else
                {
                    selectState();
                    EnemyPreviousDecision = EnemyDecision;

                }
            }

        



        if (enemyHealth.IsDead)
        {
            enemy.Stop(true);
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
    public void DetectedDistance()
    {
        DistanceBetweenTwo = Vector3.Distance(transform.position, player.position);


        if (DistanceBetweenTwo <= detect_distance)
        {
            isdetected = true;
        }//if the distance between player and enemy is lower than detect_distance, the player is detected
    }
    public void DetectCollider()
    {
        if (Physics.Linecast(transform.position, player.position, out linehit, LineMask))
        {
            collider_exist = true;
        }
        else
        {
            collider_exist = false;
        }
    }
    public void DetectStopDistance()
    {

        if (DistanceBetweenTwo <= distance)
        {



            ReachDestination = true;


        }
        else
        {
            ReachDestination = false;
        }
    }
    
    public void ForwardState()
    {

        if (collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            if(!SetDestinationOnce)
            {
                destination = player.transform.position + (player.transform.forward * DestinationDistance);
                SetDestination();
                SetDestinationOnce = true;
            }
            
        }
    }
    public void BackState()
    {
        if (collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            if (!SetDestinationOnce)
            {
                destination = player.transform.position + (player.transform.forward * -DestinationDistance);
                SetDestination();
                SetDestinationOnce = true;
            }
        }

    }
    public void RightState()
    {
        if (collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            if (!SetDestinationOnce)
            {
                destination = player.transform.position + (player.transform.right * DestinationDistance);
                SetDestination();
                SetDestinationOnce = true;
            }
            
        }

    }
    public void LeftState()
    {
        if (collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            if (!SetDestinationOnce)
            {
                destination = player.transform.position + (player.transform.right * -DestinationDistance);
                SetDestination();
                SetDestinationOnce = true;
            }
            
        }

    }

    public void selectState()
    {
        switch (EnemyDecision)
        {
            case 0:
                ForwardState();
                break;
            case 1:
                BackState();
                break;
            case 2:
                LeftState();
                break;
            case 3:
                RightState();
                break;

            default:
                ForwardState();
                break;
        }
    }
}
