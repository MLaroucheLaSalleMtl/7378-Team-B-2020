using System.Collections;
using System.Collections.Generic;
using TankBehaviour;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectile2;
    public GameObject playerShootPosition;
    public WeaponHandler wp;
    public float shellSpeed = 250;
    public int APdamage;
    public int HEdamage;
    public float MaxReload;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        wp = GameObject.Find("Weaponmanager").GetComponent<WeaponHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wp.canFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (wp.currentShell)
                {
                    case 1:
                        wp.AP--;
                        GameObject AP = GameObject.Instantiate(projectile, playerShootPosition.transform.position, playerShootPosition.transform.rotation);
                        AP.GetComponent<Rigidbody>().velocity = AP.transform.forward * shellSpeed;
                        AP.GetComponent<WeaponDamage>().SetAPConfig(new List<string>() { "AP" }, new List<string>() { "Enemy" },
                            (go) =>
                            {
                                if (go.GetComponent<TurrentEnemyCtrl>())
                                {
                                    TurrentEnemyCtrl ctrl = go.GetComponent<TurrentEnemyCtrl>();
                                    ctrl.OnTakeDamage(5);
                                }
                                if (go.GetComponent<enemyHealth>())
                                {
                                    enemyHealth ctrl = go.GetComponent<enemyHealth>();
                                    ctrl.DoDamage(50);
                                }
                                if (go.GetComponent<EnemyTankAttributeCtrl>())
                                {
                                    EnemyTankAttributeCtrl ctrl = go.GetComponent<EnemyTankAttributeCtrl>();
                                    ctrl.OnTakeDamage(50);
                                }
                            });
                        audio.Play();
                        break;
                    case 2:
                        wp.HE--;
                        GameObject HE = GameObject.Instantiate(projectile2, playerShootPosition.transform.position, playerShootPosition.transform.rotation);
                        HE.GetComponent<Rigidbody>().velocity = HE.transform.forward * shellSpeed;
                        audio.Play();
                        break;
                }
                //this.gameObject.GetComponent<AudioSource>().Play();
                wp.resetTimer();
            }
        }
    }
}