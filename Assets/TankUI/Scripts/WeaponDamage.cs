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
        Display = GameObject.FindGameObjectWithTag("Displayer");
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 vel = rigidbody.velocity;
        var orthogonalVector = collision.contacts[0].point - transform.position;
        var collisionAngle = Vector3.Angle(orthogonalVector, rigidbody.velocity);
        print(collisionAngle);
        switch (collision.transform.tag)
        {
            case "LT":
                Display.GetComponent<DisplayDamage>().Pen(300, collision.transform);
                collision.gameObject.GetComponent<enemyHealth>().DoDamage(220);
                break;
            case "MT":
                if(PenetrationChecking(vel, normal, 25))
                {
                    Display.GetComponent<DisplayDamage>().Pen(300, collision.transform);
                    collision.gameObject.GetComponent<enemyHealth>().DoDamage(220);
                    break;
                }
                else
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    break;
                }
            case "HT":
                if (PenetrationChecking(vel, normal, 40))
                {
                    Display.GetComponent<DisplayDamage>().Pen(300, collision.transform);
                    collision.gameObject.GetComponent<enemyHealth>().DoDamage(220);
                    break;
                }
                else
                {
                    Display.GetComponent<DisplayDamage>().Ricochet(collision.transform);
                    break;
                }


        }

    }
    private bool PenetrationChecking(Vector3 vel, Vector3 normal, float maxAngle)
    {
        // measure angle
        //print(Vector3.Angle(vel, normal));
        if (Vector3.Angle(vel, -normal) > maxAngle)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}