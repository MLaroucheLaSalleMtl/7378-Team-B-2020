﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsTankFire : MonoBehaviour
{
    public InsTankMovement InsTankMovement;
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
    public float FireWaitTime = 4f;
    public float time;
    public bool Fired = false;


    public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj= GameObject.FindGameObjectWithTag("Player");
        player = PlayerObj.transform;
        PlayerRigi = player.GetComponent<Rigidbody>();
        AudioSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime(InsTankMovement.DistanceBetweenTwo, speed);




        Debug.DrawRay(generator.position, generator.forward, Color.red);
        if (InsTankMovement.isdetected == true)
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


        Debug.Log("fire");
        AudioSource.Play();
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
        if (time >= FireWaitTime&& !InsTankMovement.collider_exist)
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
