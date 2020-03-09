﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    public float maxhealth=100f;
    public float CurrentHealth;
    public Slider health_bar;
    public GameObject shell;
    public GameObject enemy;
    public float damage = 50f;
    
    public bool DamageOnce;
    public EndCondition EndCondition;



    private NavMeshAgent enemynav;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxhealth;
        health_bar.value = CurrentHealth;
        enemynav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {


        //Collider[] damagecolliders = Physics.OverlapSphere(enemy.transform.position, DamageRadius);
        //for (int i = 0; i < damagecolliders.Length; i++)
        //{
        //    Debug.Log("collider found" + damagecolliders[i]);
        //    Collider shellhit = damagecolliders[i];


        //    if (shellhit.tag == "Bullet")
        //    {
        //        Debug.Log("damaged");
        //        DamageOnce = true;
        //        DoDamage();
        //    }
        //}

        if (CurrentHealth <= 0)
        {
            enemynav.enabled = !enemynav.enabled;
            EndCondition.numberupdate();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.collider.tag);
    //    if (collision.collider.tag == "Bullet")
    //    {
    //        Debug.Log("damaged");
    //        DamageOnce = true;
    //        DoDamage();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag=="AP")
        {
            Debug.Log("damaged");
            DamageOnce = true;
            DoDamage();
        }
    }
    public void DoDamage()
    {
        if(DamageOnce)
        {
            CurrentHealth = CurrentHealth - damage;
            DamageOnce = false;
            health_bar.value = CurrentHealth;
        }
        
        
    }
}
