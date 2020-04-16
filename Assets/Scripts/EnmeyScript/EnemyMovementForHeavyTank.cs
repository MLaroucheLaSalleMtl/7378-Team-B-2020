using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovementForHeavyTank : MonoBehaviour
{
    [Header("Navigation elements")]

    public Transform player;
    NavMeshAgent enemy;
    Vector3 destination;
    public float distance = 50;//the enemy will get close to the player and stop "distance" away from player
    RaycastHit hit;
    RaycastHit linehit;
    public LayerMask LineMask;

    
    public float timer = 0f;
    public float waittime = 3f;
    public bool collider_exist = false;
    public float DistanceBetweenTwo;

    public bool isdetected = false;
    public float detect_distance = 50;
    [Header("Heavy Tank")]
    public bool HeavyTankOnlyOnce = false;
    private float DestinationGoal = 40f;
    public EnemyFireForHeavyTank EnemyFireForHeavyTank;
    public float decision;
    public enemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyFireForHeavyTank = this.GetComponent<EnemyFireForHeavyTank>();
        enemyHealth = transform.GetComponent<enemyHealth>();

        enemy = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {


        if (EnemyFireForHeavyTank.Fired)
        {
            HeavyTankOnlyOnce = false;
        }
        else
        {
            HeavyTankOnlyOnce = true;
        }
        DetectedDistance();
        DetectCollider();

        heavy_tankstate();




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
    









    public void heavy_tankstate()
    {

        if (isdetected)
        {
            if (collider_exist)
            {
                destination = player.transform.position;
            }
            else
            {
                if (!HeavyTankOnlyOnce)
                {
                    decision = Random.Range(0, 99);
                    if (decision < 49)
                    {
                        destination = player.transform.position + (player.transform.right * DestinationGoal) + (player.transform.forward * DestinationGoal);
                    }
                    else
                    {
                        destination = player.transform.position + (player.transform.right * -DestinationGoal) + (player.transform.forward * DestinationGoal);
                    }

                    SetDestination();
                    HeavyTankOnlyOnce = true;
                }

            }
        }
        else
        {
            destination = transform.position;
            SetDestination();
        }
    }
}
