using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemy_movement : MonoBehaviour
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
    [Header("Death")]
    public enemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        StateTime = Random.Range(20, 40);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        enemyHealth = transform.GetComponent<enemyHealth>();
        enemy = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        DetectedDistance();
        DetectCollider();
        if (!OnlyRunOneTime)
        {
            Firsttimer += Time.deltaTime;
            if (Firsttimer > StateTime)
            {
                OnlyRunOneTime = true;
            }
            else
            {
                NormalState();

            }
        }
        else
        {
            if(isdetected)
            {
                timer += Time.deltaTime;
                if (timer > StateTime)
                {
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
            
        }



        if(enemyHealth.IsDead)
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
    public void NormalState()
    {
        DetectStopDistance();
        if (isdetected && ReachDestination && !collider_exist)
        {

            destination = transform.position;
            SetDestination();
        }
        else if (isdetected && ReachDestination && collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else if (isdetected && !ReachDestination)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            destination = transform.position;
            SetDestination();
        }

        //if ((ReachDestination == false || collider_exist == true) && isdetected == true)
        //{

        //    timer += Time.deltaTime;
        //    if (timer > waittime)
        //    {
        //        destination = player.transform.position;
        //        SetDestination();
        //    }

        //}
        //else if (ReachDestination == true && collider_exist == false)
        //{

        //    destination = transform.position;
        //    SetDestination();
        //}


    }//in this state, enemy will follow player, and keep a distance with player
    public void ForwardState()
    {

        if (collider_exist)
        {
            destination = player.transform.position;
            SetDestination();
        }
        else
        {
            destination = player.transform.position + (player.transform.forward * DestinationDistance);
            SetDestination();
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
            destination = player.transform.position + (player.transform.forward * -DestinationDistance);
            SetDestination();
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
            destination = player.transform.position + (player.transform.right * DestinationDistance);
            SetDestination();
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
            destination = player.transform.position + (player.transform.right * -DestinationDistance);
            SetDestination();
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
                NormalState();
                break;
        }
    }
    //public Transform player;
    //NavMeshAgent enemy;
    //Vector3 destination;
    //public float distance=50;//the enemy will get close to the player and stop "distance" away from player
    //RaycastHit hit;
    //RaycastHit linehit;
    //public LayerMask LineMask;
    //public LayerMask layerMask;
    //public bool ReachDestination = false;
    //float timer = 0f;
    //public float waittime = 3f;
    //public bool collider_exist = false;
    //public float DistanceBetweenTwo;

    //public bool isdetected = false;
    //public float detect_distance = 50;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    enemy = this.GetComponent<NavMeshAgent>();

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    DistanceBetweenTwo= Vector3.Distance(transform.position, player.position);


    //    if(DistanceBetweenTwo<=detect_distance)
    //    {
    //        isdetected = true;
    //    }






    //    if((ReachDestination==false||collider_exist==true)&&isdetected==true)
    //    {

    //        timer += Time.deltaTime;
    //        if (timer > waittime)
    //        {
    //            destination = player.transform.position;
    //            SetDestination();
    //        }

    //    }
    //    else if(ReachDestination==true&&collider_exist==false)
    //    {

    //        destination = transform.position;
    //        SetDestination();
    //    }

    //    if (DistanceBetweenTwo<=distance)
    //    {

    //            timer = 0f;

    //            ReachDestination = true;


    //    }
    //    else
    //    {
    //        ReachDestination = false;
    //    }


    //    if(Physics.Linecast(transform.position,player.position,out linehit,LineMask))
    //    {
    //        collider_exist = true;
    //    }
    //    else
    //    {
    //        collider_exist = false;
    //    }


    //}
    //public void SetDestination()
    //{

    //    enemy.SetDestination(destination);
    //}
    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}
