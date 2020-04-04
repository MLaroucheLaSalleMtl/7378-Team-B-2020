using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireTest : MonoBehaviour
{

    public float WaitTime = 2f;
    public float time;
    public bool fire = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time>=WaitTime)
        {
            fire = true;
            time = 0f;
        }
        else
        {
            fire = false;
        }
    }
}
