using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealth : MonoBehaviour
{
    [Header("Health")]
    private float maxhealth = 100f;
    public float CurrentHealth;
    [Header("Death")]
    public bool IsDead = false;
    public bool OnlyOnce = false;
    public EndCondition EndCondition;
    [Header("Display Hp")]
    public Slider health_bar;






    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxhealth;
        health_bar.value = CurrentHealth;


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
        Debug.Log("dead");
        if (!OnlyOnce)
        {
            EndCondition.numberupdate();
            OnlyOnce = true;
        }
        
    }
}
