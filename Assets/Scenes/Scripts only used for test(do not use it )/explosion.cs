using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public GameObject explode;
    public GameObject self;
    public float speed = 10;
    public float timer = 0;
    public float waittime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

     
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explode, self.transform);

        timer += Time.deltaTime;
        if (timer >= waittime)
        {

            DestroyObject(self);
        }
        if (timer > waittime)
        {
            timer = 0f;
        }
    }

}


