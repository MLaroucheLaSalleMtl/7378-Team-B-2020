﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth;
    public float CurrentHealth;
    public GameObject smoke;
    public bool onlyOnceSmoke = false;
    [Header("Death")]
    public bool IsDead = false;
    public bool OnlyOnce = false;
    public EndCondition EndCondition;
    [Header("Display Hp")]
    public Slider health_bar;
   

    // Start is called before the first frame update
    void Start()
    {
        if(transform.tag=="LT")
        {
            maxhealth = 1500;
        }
        else if(transform.tag=="MT")
        {
            maxhealth = 2000;
        }
        else
        {
            maxhealth = 2500;
        }
        CurrentHealth = maxhealth;
        health_bar.value = (CurrentHealth/maxhealth)*100;
        
        EndCondition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<EndCondition>();

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        health_bar.value = (CurrentHealth / maxhealth) * 100;


        if(CurrentHealth>=maxhealth)
        {
            health_bar.gameObject.SetActive(false);
        }
        else
        {
            health_bar.gameObject.SetActive(true);
        }


        if(CurrentHealth/maxhealth<0.3&&!onlyOnceSmoke)
        {

            Instantiate(smoke, transform.position, Quaternion.identity,transform);
            onlyOnceSmoke = true;
        }



        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    public void DoDamage(int damage)
    {

        CurrentHealth = CurrentHealth - damage;
        




    }
    public void Death()
    {
        IsDead = true;
       
        if (!OnlyOnce)
        {
            EndCondition.numberupdate();
            OnlyOnce = true;
        }
        
    }
}
