using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkInstantiate : MonoBehaviour
{
    
    public GameObject LightTank;
    
    public float time;
    public float InstantiateTime = 5f;
    
    private SecureArea SecureArea;
    // Start is called before the first frame update
    void Start()
    {
        SecureArea = GameObject.FindGameObjectWithTag("SecureArea").GetComponent<SecureArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SecureArea.Secured&&SecureArea.InArea)
        {
            time = time + Time.deltaTime;
            if (time >= InstantiateTime)
            {


                Instantiate(LightTank, transform.position, transform.rotation);


                time = 0f;

            }
        }
        
        
    }
}
