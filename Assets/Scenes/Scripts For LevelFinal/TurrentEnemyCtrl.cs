using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentEnemyCtrl : MonoBehaviour
{
    [SerializeField] private Transform currentTarget=null;
    [SerializeField] private float findTargetRadius=20f;
    [SerializeField] private string mainPlayerTag = "TankBody";
    [SerializeField] private float rotateSpeed=1f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private bool isAimToPlayer = false;

    [SerializeField] private GameObject[] shootPosArr=null;
    [SerializeField] private GameObject bullet=null;
    [SerializeField] private float bulletSpeed = 20f;

    private void Update()
    {
        TryFindTarget();
        TryRotateToTarget();
        TryAttackPlayer();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,findTargetRadius);
    }
    
    private void TryFindTarget()
    {
           Collider[] coll= Physics.OverlapSphere(transform.position, findTargetRadius, 1<<10);
           if (coll != null && coll.Length > 0)
           {
               currentTarget = coll[0].transform;
               
           }
           else
           {
               currentTarget = null;
           }
  
    }

    private void TryRotateToTarget()
    {
        if (currentTarget != null)
        {
            Quaternion targetRot = Quaternion.LookRotation(currentTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRot,rotateSpeed*Time.deltaTime);
         //   print(Quaternion.Angle(targetRot,transform.rotation));
         if (Quaternion.Angle(targetRot, transform.rotation) < 0.5f)
         {
             isAimToPlayer = true;
         }
         else
         {
             isAimToPlayer = false;
         }
        }
        else
        {
            isAimToPlayer = false;
        }
    }
    private void TryAttackPlayer()
    {
        if (isAimToPlayer&&Time.time>=nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackDelay;
        }
    }

    private void Attack()
    {
        Rigidbody tempRb;
        foreach (var posObj in shootPosArr)
        {
          GameObject obj=  Instantiate(bullet, posObj.transform.position, posObj.transform.rotation);
          obj.GetComponent<WeaponDamage>().SetAPConfig(new List<string>(){"AP"});
          tempRb=obj.GetComponent<Rigidbody>();
          tempRb.velocity = transform.forward * bulletSpeed;
        }
    }
}
