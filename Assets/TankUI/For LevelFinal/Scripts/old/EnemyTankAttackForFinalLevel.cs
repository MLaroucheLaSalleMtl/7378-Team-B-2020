using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankAttackForFinalLevel : MonoBehaviour
{
    #region Properties

    [SerializeField] private Transform bulletSpawnTran;
    [SerializeField] private Transform turretTran;
    [SerializeField] private GameObject bulletPfb;
    [SerializeField] private int maxAIBulletCount = 3;
    private EnemyTankMoveMentForFinalLevel enmeyMoveCtrl;
    private Transform playerTran;
    private bool isfiring = false;
    private float bulletMoveSpeed = 50;
    private Vector3 direction;
    private Quaternion rotation;
    private float rotationspeed = 3f;
    private RaycastHit hit;
    private Vector3 playerRigidBodyVelocity;
    private Rigidbody playerRb;
    private float bulletMoveTime;
    private float fireWaitTime = 10f;
    private float time;
    private bool isFiring = false;
    public bool IsFring
    {
        get => isFiring;
    }
    #endregion

    #region Mono APIs

    private void Awake()
    {
        enmeyMoveCtrl = GetComponent<EnemyTankMoveMentForFinalLevel>();
        playerTran = GameObject.FindWithTag("Player").transform;
        playerRb = playerTran.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (enmeyMoveCtrl.IsDead)
        {
            return;
        }
        CalcBulletMoveTime(enmeyMoveCtrl.DistToEnemy, bulletMoveSpeed);
        if (enmeyMoveCtrl.IsDetected == true)
        {
            isfiring = true;
        }

        if (isfiring)
        {
            DoAimAI();
            print("DoAimAI");
        }
    }

    #endregion

    #region State Functions
    public void DoAimAI()
    {
        playerRigidBodyVelocity = playerRb.velocity;
        direction = playerTran.position - turretTran.position;
        rotation = Quaternion.LookRotation((direction + playerRigidBodyVelocity) * bulletMoveTime);
        turretTran.rotation = Quaternion.Lerp(turretTran.rotation, rotation, Time.deltaTime * rotationspeed);
        time = time + Time.deltaTime;
        if (time >= fireWaitTime)
        {
            DoAttackAI();
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
    }

    private void DoAttackAI()
    {
        int randomVal = UnityEngine.Random.Range(0, 2);
        switch (randomVal)
        {
            case 0:
                AttackAINormalFire();
                time = 0f;
                break;
            case 1:
                StartCoroutine(AttackAIContinueFire());
                break;
        }
    }
    public void AttackAINormalFire()
    {
        GameObject bullet = Instantiate(bulletPfb, bulletSpawnTran.position, bulletSpawnTran.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletMoveSpeed;
    }

    IEnumerator AttackAIContinueFire()
    {
        for (int i = 0; i < maxAIBulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPfb, bulletSpawnTran.position, bulletSpawnTran.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletMoveSpeed;
            yield return 1f;
        }

        time = 0f;
    }
    public void CalcBulletMoveTime(float distance, float bulletSpeed)
    {
        bulletMoveTime = distance / bulletSpeed;
    }

    #endregion
}