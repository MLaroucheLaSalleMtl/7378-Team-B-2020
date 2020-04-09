using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionSphereCtrl : MonoBehaviour
{
    public int endTime = 300;
    public float reduceDelta = 0f;
    public Vector3 reduceDeltaVec=Vector3.zero;
    public string playerTag = "Player";
    public string playerLayer = "TankBody";
    public bool isPlayerExist = false;
    public PlayerHealth playerHealth = null;
    public float playerHpDeduceVal = 2f;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag(playerTag).GetComponent<PlayerHealth>();
    }

    public void Start()
    {
        reduceDelta = transform.localScale.x / endTime;
        reduceDeltaVec=new Vector3(reduceDelta,reduceDelta,reduceDelta);
        
    }

    private void Update()
    {
        transform.localScale-=reduceDeltaVec*Time.deltaTime;
        if (!isPlayerExist)
            ReducePlayerHp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(playerTag, StringComparison.CurrentCultureIgnoreCase))
        {
            isPlayerExist = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(playerTag, StringComparison.CurrentCultureIgnoreCase))
        {
            isPlayerExist = false;
        }
    }

    private void ReducePlayerHp()
    {
        playerHealth.DoDamage(playerHpDeduceVal*Time.deltaTime);
    }
}
