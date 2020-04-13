using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HeavyTankMoveLevel2 : MonoBehaviour
{
    public bool isdetected = false;
    public float Detect = 50f;
    public float DistanceBetweenTwo;
    public Transform player;
    public Vector3 destination;
    public Transform RetreatDestination;
    public Transform NormalDestination;
    NavMeshAgent enemy;
    public GameManagerLelvel2 GameManagerLelvel2;
    Rigidbody enemyrigi;
    Vector3 velocity = Vector3.zero;


    RaycastHit linehit;
    public bool collider_exist = false;
    public LayerMask LineMask;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = this.GetComponent<NavMeshAgent>();
        enemyrigi = this.GetComponent<Rigidbody>();
        GameManagerLelvel2 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameManagerLelvel2>();
        NormalDestination = GameObject.FindGameObjectWithTag("ND").transform;
        RetreatDestination = GameObject.FindGameObjectWithTag("RD").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DetectedDistance();
        DetectCollider();
        if(GameManagerLelvel2.Retreat)
        {
            enemy.Resume();
            destination = RetreatDestination.position;
            SetDestination();
        }
        else
        {
            destination = NormalDestination.position;
            SetDestination();
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
            collider_exist = true;
        }
        else
        {
            collider_exist = false;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ND")
        {

            
            enemyrigi.velocity = Vector3.zero;
            enemyrigi.angularVelocity = Vector3.zero;
        }
    }
}
