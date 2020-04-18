using System;
using System.Collections;
using System.Collections.Generic;
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
                Display.GetComponent<DisplayDamage>().Pen(damage, collision.transform);
                //collision.gameObject.GetComponent<enemyHealth>().DoDamage(damage);
                Destroy(this.gameObject);
                break;
            case "MT":
                if(collisionAngle > 30)
                {
                    Display.GetComponent<DisplayDamage>().Pen(damage, collision.transform);
                    //collision.gameObject.GetComponent<enemyHealth>().DoDamage(damage);
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
                print(collisionAngle);
                if (collisionAngle > 50)
                {
                    Display.GetComponent<DisplayDamage>().Pen(damage, collision.transform);
                    //collision.gameObject.GetComponent<enemyHealth>().DoDamage(damage);
                    Destroy(this.gameObject);
                    break;
                }
                else
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    Destroy(this.gameObject);
                    break;
                }


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