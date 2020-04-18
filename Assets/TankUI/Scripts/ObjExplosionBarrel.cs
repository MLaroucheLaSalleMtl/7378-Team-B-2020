using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjExplosionBarrel : MonoBehaviour
{
    public GameObject Particle;
    public LayerMask ShellMask, EnemyMask;
    public AudioClip SE;
    public int Dmg=50;
    public int Radius = 50;
    

    private void OnCollisionEnter(Collision other)
    {
        int layerMask = 1 << other.gameObject.layer;
        if ((ShellMask.value & layerMask) > 0)
        {
            Instantiate(Particle, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(SE,Camera.main.transform.position);
            Collider[] colls = Physics.OverlapSphere(transform.position, Radius);
            foreach (var VARIABLE in colls)
            {
                int mask = 1 << VARIABLE.gameObject.layer;
                if ((EnemyMask.value & mask) > 0)
                {
                    if (VARIABLE.GetComponent<enemyHealth>())
                    {
                        enemyHealth health = VARIABLE.GetComponent<enemyHealth>();
                        health.DoDamage(Dmg);
                    }
                    if (VARIABLE.GetComponent<EnemyHealth2>())
                    {
                        EnemyHealth2 health = VARIABLE.GetComponent<EnemyHealth2>();
                        health.DoDamage(Dmg);
                    }
                    if (VARIABLE.GetComponent<EnemyHealthAttributeCtrl>())
                    {
                        EnemyHealthAttributeCtrl health = VARIABLE.GetComponent<EnemyHealthAttributeCtrl>();
                        health.DoDamage(Dmg);
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}
