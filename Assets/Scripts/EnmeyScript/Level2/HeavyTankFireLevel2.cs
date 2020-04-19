﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTankFireLevel2 : MonoBehaviour
{
    public HeavyTankMoveLevel2 HeavyTankMoveLevel2;
    private AudioSource AudioSource;

    public Transform generator;
    public Transform turret;
    public bool isfiring = false;
    public Transform player;
    public Transform fire_position;
    public GameObject prefab;
    private float speed = 50;

    public Vector3 direction;
    public Quaternion rotation;
    public float rotationspeed = 0.01f;
    
    RaycastHit hit;
    [Header("predictive Shooting")]
    public Vector3 VelocityofPlayer;
    public Rigidbody PlayerRigi;
    public float PredictiveTime;
    public float FireWaitTime = 10f;
    public float time;
    public bool Fired = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerRigi = player.GetComponent<Rigidbody>();
        HeavyTankMoveLevel2 = this.GetComponent<HeavyTankMoveLevel2>();
        AudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime(HeavyTankMoveLevel2.DistanceBetweenTwo, speed);




        Debug.DrawRay(generator.position, generator.forward, Color.red);
        if (HeavyTankMoveLevel2.isdetected == true)
        {
            isfiring = true;
        }


        if (isfiring)
        {
            TurretRotation();

        }
    }

    public void fire()
    {

        AudioSource.Play();
        Debug.Log("fire");
        //this.gameObject.GetComponent<AudioSource>().Play();
        GameObject shell = Instantiate(prefab, fire_position.position, fire_position.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * speed;




    }
    public void TurretRotation()
    {
        VelocityofPlayer = PlayerRigi.velocity;
        direction = player.position - turret.position;
        rotation = Quaternion.LookRotation((direction + VelocityofPlayer) * PredictiveTime);
        turret.rotation = Quaternion.Lerp(turret.rotation, rotation, Time.deltaTime * rotationspeed);
        time = time + Time.deltaTime;
        if (time >= FireWaitTime&&!HeavyTankMoveLevel2.collider_exist)
        {
            
            
                fire();
                Fired = true;
                time = 0f;
            
            
        }
        else
        {
            Fired = false;
        }

    }


    public void CalculateTime(float distance, float speedofshell)
    {
        PredictiveTime = distance / speedofshell;

    }
}
