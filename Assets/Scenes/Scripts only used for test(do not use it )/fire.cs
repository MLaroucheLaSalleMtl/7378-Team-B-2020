using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;
    public float speed = 10;
    public float timer=0;
    public float waittime = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waittime)
        {
            Debug.Log("fire");
            GameObject shell = Instantiate(prefab, position.position, position.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;

        }
        if (timer > waittime)
        {
            timer = 0f;
        }
       
    }
}
