using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float maxhealth = 100f;
    public float CurrentHealth;
   
    public GameObject shell;
    public GameObject enemy;
    public float damage = 50f;

    public bool DamageOnce;
    public EndCondition EndCondition;



   
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxhealth;
        
        
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoDamage()
    {
        if (DamageOnce)
        {
            CurrentHealth = CurrentHealth - damage;
            DamageOnce = false;
            
        }


    }
}
