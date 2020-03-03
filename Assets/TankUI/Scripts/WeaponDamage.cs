using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject ExplosionEffect;
    private bool ExplosionFlg = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (ExplosionEffect && !ExplosionFlg)
        {
            ExplosionFlg = true;
            GameObject obj = (GameObject)Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation);
            Destroy(obj, 3);
            Destroy(this.gameObject, 0.3f);
        }
	}
}
