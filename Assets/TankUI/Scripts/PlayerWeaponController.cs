using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectile2;
    public GameObject playerShootPosition;
    public float shellSpeed = 250;
    public static bool fire = false;
    public static bool canFire = false;
    public static bool fired = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            if (Input.GetMouseButtonDown(0) || fire)
            {
                switch (WeaponHandler.currentShell)
                {
                    case 1:
                        WeaponHandler.AP--;
                        GameObject AP = GameObject.Instantiate(projectile, playerShootPosition.transform.position, playerShootPosition.transform.rotation);
                        AP.GetComponent<Rigidbody>().velocity = AP.transform.forward * shellSpeed;
                        break;
                    case 2:
                        WeaponHandler.HE--;
                        GameObject HE = GameObject.Instantiate(projectile2, playerShootPosition.transform.position, playerShootPosition.transform.rotation);
                        HE.GetComponent<Rigidbody>().velocity = HE.transform.forward * shellSpeed;
                        break;
                }
                //this.gameObject.GetComponent<AudioSource>().Play();

                canFire = false;
                fired = true;
                WeaponHandler.canCount = true;
                WeaponHandler.doOnce = false;
                WeaponHandler.reload = 5.0f;
                fire = false;
            }
        }
    }
}