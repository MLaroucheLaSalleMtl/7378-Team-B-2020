using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeRadius = 0.2f;
    
    public float timer;
    public float waittime;
    public bool hit = false;
    public Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit==true)
        {
            timer += Time.deltaTime;
            if (timer <= waittime)
            {

                transform.localPosition = Random.insideUnitSphere * ShakeRadius+transform.position;
            }
            if (timer > waittime)
            {

                timer = 0;
                hit = false;
                transform.position = origin;
            }
        }
        
        
    }
}
