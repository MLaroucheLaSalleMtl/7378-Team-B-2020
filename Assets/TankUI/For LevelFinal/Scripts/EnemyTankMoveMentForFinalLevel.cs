using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyTankMoveMentForFinalLevel : MonoBehaviour
{
    #region Properties

    [SerializeField] private LayerMask lineLayerMask;
    private EnemyHealthAttributeCtrl healthCtrl;
    private Transform playerTran;
    private NavMeshAgent navAgent;
    private Vector3 destination;
    private float distance = 50;
    private RaycastHit hit;
    private RaycastHit linehit;
    private bool existEnemyCollider = false;
    private float distToEnemy;
    private bool isdetected = false;
    private float detectCheckVal = 50;
    private bool isCantMove = false;
    private float destinationGoal = 40f;
    private EnemyTankAttackForFinalLevel enemyFireCtrl;
    private float moveRandomVal;
    private bool isDead = false;

    public float DistToEnemy=> distToEnemy;
    public bool IsDetected => isdetected;
    public bool IsDead => isDead;
    #endregion

    #region Mono APIs

    private void Awake()
    {
        playerTran = GameObject.FindWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        enemyFireCtrl = GetComponent<EnemyTankAttackForFinalLevel>();
        healthCtrl = GetComponent<EnemyHealthAttributeCtrl>();
    }

    void Update()
    {
        if (healthCtrl.IsDead)
        {
            isDead = true;
            return;
        }
        if (enemyFireCtrl.IsFring)
        {
            isCantMove = true;
        }
        else
        {
            isCantMove = false;
        }

        CheckDistToPlayer();
        CheckPlayerCollider();
        DOMoveAI();
    }

    #endregion

    #region MovementAI

    public void SetDestination()
    {
        navAgent.SetDestination(destination);
    }

    public void CheckDistToPlayer()
    {
        distToEnemy = Vector3.Distance(transform.position, playerTran.position);


        if (distToEnemy <= detectCheckVal)
        {
            isdetected = true;
        }
    }

    public void CheckPlayerCollider()
    {
        if (Physics.Linecast(transform.position, playerTran.position, out linehit, lineLayerMask))
        {
            existEnemyCollider = true;
        }
        else
        {
            existEnemyCollider = false;
        }
    }

    public void DOMoveAI()
    {
        if (isdetected)
        {
            if (existEnemyCollider)
            {
                destination = playerTran.transform.position;
            }
            else
            {
                if (!isCantMove)
                {
                    moveRandomVal = Random.Range(0, 99);
                    if (moveRandomVal < 49)
                    {
                        destination = playerTran.transform.position + (playerTran.transform.right * destinationGoal) +
                                      (playerTran.transform.forward * destinationGoal);
                    }
                    else
                    {
                        destination = playerTran.transform.position + (playerTran.transform.right * -destinationGoal) +
                                      (playerTran.transform.forward * destinationGoal);
                    }

                    SetDestination();
                    isCantMove = true;
                }
            }
        }
    }

    #endregion
}