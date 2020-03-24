using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTest : MonoBehaviour
{
    public GameObject test;
    public float distance = 3f;
    public bool onlyonce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!onlyonce)
        {
            Instantiate(test, transform.position + (transform.right * distance), transform.rotation);
            onlyonce = true;
        }
    }
}
