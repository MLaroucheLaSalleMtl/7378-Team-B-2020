using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth2 : MonoBehaviour
{
    [Header("Health")]
    private float maxhealth = 100f;
    public float CurrentHealth;
    [Header("Death")]
    public bool IsDead = false;
    public bool OnlyOnce = false;
   
    [Header("Display Hp")]
    public Slider health_bar;
    enemyfire enemyfire;





    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxhealth;
        health_bar.value = CurrentHealth;
        enemyfire = gameObject.GetComponent<enemyfire>();
        
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        health_bar.value = CurrentHealth;




        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    public void DoDamage(float damage)
    {

        CurrentHealth = CurrentHealth - damage;





    }
    public void Death()
    {
        IsDead = true;
        enemyfire.enabled = false;
       
       

    }
}
