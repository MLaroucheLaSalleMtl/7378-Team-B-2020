using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject AP;
    public GameObject HE;
    public Transform position;
    public float speed = 250f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject shell = Instantiate(AP, position.position, position.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;
        }
    }
}
