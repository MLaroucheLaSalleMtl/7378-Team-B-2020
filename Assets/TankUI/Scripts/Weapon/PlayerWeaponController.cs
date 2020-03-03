using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject playerShootPosition;
    public float shellSpeed = 250;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            this.gameObject.GetComponent<AudioSource>().Play();
            GameObject shell = GameObject.Instantiate(projectile, playerShootPosition.transform.position, playerShootPosition.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * shellSpeed;
        }
	}
}
