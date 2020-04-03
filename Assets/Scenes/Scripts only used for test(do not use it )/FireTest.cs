using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction1 = transform.TransformDirection(0.5f, 0, 1f) * 1000;
        Debug.DrawRay(transform.position, direction1, Color.black);
    }
}
