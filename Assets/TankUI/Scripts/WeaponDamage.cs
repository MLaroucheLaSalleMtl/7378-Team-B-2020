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
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (APCollIgnoreTagArr != null && APCollIgnoreTagArr.Count > 0)
        {
            if (APCollIgnoreTagArr.Contains(collision.gameObject.tag)) return;
        }
        if (OnCollisionEnterCallBack != null) OnCollisionEnterCallBack(collision.gameObject);
        if (ExplosionEffect && !ExplosionFlg)
        {
            print(collision.gameObject.name);
            ExplosionFlg = true;
            GameObject obj =
                (GameObject) Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation);
            Destroy(obj, 3);
            Destroy(this.gameObject, 0.3f);
        }
    }
}