using System;
using System.Collections;
using System.Collections.Generic;
using TankBehaviour;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public bool ExplosionFlg = false;
    public List<string> APCollIgnoreTagArr;
    public List<string> ApCollTargetTagArr;
    private Action<GameObject> OnCollisionEnterCallBack;
    private GameObject Display;
    Rigidbody rigidbody;
    private int damage;
    Vector3 vel;
    // Start is called before the first frame update
    public void SetAPConfig(List<string> ignoreArr = null, List<string> targetArr = null,Action<GameObject> callBack=null)
    {
        if (ignoreArr != null)
            APCollIgnoreTagArr = ignoreArr;
        if (ApCollTargetTagArr != null)
            ApCollTargetTagArr = targetArr;
        if(callBack!=null)
            OnCollisionEnterCallBack=callBack;
    }

    void Start()
    {
        CalculateDamage();
        Display = GameObject.FindGameObjectWithTag("Displayer");
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = rigidbody.velocity;

    }

    private void OnCollisionEnter(Collision collision)
    {
        print(damage);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        //if (APCollIgnoreTagArr != null && APCollIgnoreTagArr.Count > 0)
        //{
        //    if (APCollIgnoreTagArr.Contains(collision.gameObject.tag)) return;
        //}
        //if (ApCollTargetTagArr.Contains(collision.gameObject.tag)) 
        //    if (OnCollisionEnterCallBack != null) OnCollisionEnterCallBack(collision.gameObject);
        if (ExplosionEffect && !ExplosionFlg)
        {
            ExplosionFlg = true;
            GameObject obj =
                (GameObject) Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation);
            Destroy(obj, 3);
            Destroy(this.gameObject, 0.3f);
        }

        Vector3 normal = collision.contacts[0].normal;
        var collisionAngle = Mathf.Abs(90 - (Vector3.Angle(vel, normal)));
        switch (collision.transform.tag)
        {
            case "LT":
                DoDamage(collision.gameObject);
                Destroy(this.gameObject);
                break;
            case "MT":
                if(this.tag == "HE")
                {
                    if (collisionAngle > 40)
                    {
                        DoDamage(collision.gameObject);
                        Destroy(this.gameObject);
                        break;
                    }
                    break;
                }
                else if(collisionAngle > 30)
                {
                    DoDamage(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                }
                else
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    Destroy(this.gameObject);
                    break;
                }
            case "HT":
                if (this.tag == "HE")
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    Destroy(this.gameObject);
                    break;
                }
                else if (collisionAngle > 55)
                {
                    DoDamage(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                }
                else
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    Destroy(this.gameObject);
                    break;
                }
            case "Player":
                switch (collision.gameObject.GetComponent<PlayerHealth>().type)
                {
                    case "Light":
                        collision.gameObject.GetComponent<PlayerHealth>().DoDamage(damage * 0.15f);
                        Destroy(this.gameObject);
                        break;
                    case "Medium":
                        if (collisionAngle > 30)
                        {
                            collision.gameObject.GetComponent<PlayerHealth>().DoDamage(damage * 0.15f);
                            Destroy(this.gameObject);
                            break;
                        }
                        else
                        {
                            Destroy(this.gameObject);
                            break;
                        }
                    case "Heavy":

                        if (collisionAngle > 55)
                        {
                            collision.gameObject.GetComponent<PlayerHealth>().DoDamage(damage * 0.15f);
                            Destroy(this.gameObject);
                            break;
                        }
                        else
                        {
                            Destroy(this.gameObject);
                            break;
                        }
                }
                break;


        }

    }
    private void DoDamage(GameObject go)
    {
            Display.GetComponent<DisplayDamage>().Pen(damage, go.transform);
            if (go.GetComponent<TurrentEnemyCtrl>())
            {
                TurrentEnemyCtrl ctrl = go.GetComponent<TurrentEnemyCtrl>();
                ctrl.OnTakeDamage(damage);
            }
            if (go.GetComponent<enemyHealth>())
            {
                enemyHealth ctrl = go.GetComponent<enemyHealth>();
                ctrl.DoDamage(damage);
            }
            if (go.GetComponent<EnemyHealth2>())
            {
                EnemyHealth2 ctrl = go.GetComponent<EnemyHealth2>();
                ctrl.DoDamage(damage);
            }
            if (go.GetComponent<EnemyTankAttributeCtrl>())
            {
                EnemyTankAttributeCtrl ctrl = go.GetComponent<EnemyTankAttributeCtrl>();
                ctrl.OnTakeDamage(damage);
            }

    }
    private void CalculateDamage()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        switch (this.tag)
        {
            case "AP":
                damage = player.GetComponentInChildren<PlayerWeaponController>().APdamage;
                break;
            case "HE":
                damage = player.GetComponentInChildren<PlayerWeaponController>().HEdamage;
                break;
        }
    }
}