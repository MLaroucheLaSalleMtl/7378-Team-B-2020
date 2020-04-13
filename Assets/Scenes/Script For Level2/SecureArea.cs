using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecureArea : MonoBehaviour
{

    public float timer = 0;
    public float waittime = 0.1f;
    public Slider SecureBar;
    public bool InArea = false;
    public bool EnemyIn = false;
    public bool Secured = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(SecureBar.value>=100f)
        {
            Secured = true;
        }
        else
        {
            Secured = false;
        }



        if(InArea&&!EnemyIn)
        {
            timer += Time.deltaTime;
            if (timer >= waittime)
            {
                SecureBar.value += 0.5f;

            }
            if (timer > waittime)
            {
                timer = 0f;
            }



        }
        else if(!InArea&&SecureBar.value>0)
        {
            timer += Time.deltaTime;
            if (timer >= waittime)
            {
                SecureBar.value -= 1f;

            }
            if (timer > waittime)
            {
                timer = 0f;
            }
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            InArea = true;
        }
        


        if(other.tag=="Enemy")
        {
            EnemyIn = true;
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InArea = false;
        }
        

        if (other.tag == "Enemy")
        {
            EnemyIn = false;
        }
        
    }
}
