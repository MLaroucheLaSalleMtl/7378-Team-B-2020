using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkInstantiate : MonoBehaviour
{
    public GameObject LightTank;
    public GameObject HeavyTank;
    public float time;
    public float InstantiateTime = 5f;
    public float decision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        time = time + Time.deltaTime;
        if(time>=InstantiateTime)
        {
            decision = Random.Range(0, 99);
            if(decision<=49)
            {
                Instantiate(LightTank, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(HeavyTank, transform.position, transform.rotation);
            }
            time = 0f;
            
        }
        
    }
}
