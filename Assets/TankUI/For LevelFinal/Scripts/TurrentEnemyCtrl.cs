using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Slider hpBar;
    [SerializeField] private float Hp = 100;
    [SerializeField] private float apDamage=10f;
    private bool isDead = false;

    private void Update()
    {
        if (Hp <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                ModuleRoot.Ins.ModuleData.AddScore();
                ModuleRoot.Ins.UIModule.ShowWnd(true, WndType.WND_OVER);
            }
        }
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
           Collider[] coll= Physics.OverlapSphere(transform.position, findTargetRadius, 1<<19);
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
          obj.GetComponent<WeaponDamage>().SetAPConfig(new List<string>(){"AP"},new List<string>(){"Player"},BulletAction);
          tempRb=obj.GetComponent<Rigidbody>();
          tempRb.velocity = transform.forward * bulletSpeed;
        }
    }

    public void BulletAction(GameObject go)
    {
        go.GetComponent<PlayerHealth>().DoDamage(apDamage);
    }
    public void OnTakeDamage(float dmg)
    {
        float val = Hp - dmg;
        Hp = val <= 0 ? 0 : val;
        SetHpUI(Hp/100);
    }

    public void SetHpUI(float HpRatio)
    {
        hpBar.value = HpRatio;
    }
}
