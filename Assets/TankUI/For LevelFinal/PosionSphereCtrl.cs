using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionSphereCtrl : MonoBehaviour
{
    public int endTime = 300;
    public float reduceDelta = 0f;
    public Vector3 reduceDeltaVec=Vector3.zero;

    public void Start()
    {
        reduceDelta = transform.localScale.x / endTime;
        reduceDeltaVec=new Vector3(reduceDelta,reduceDelta,reduceDelta);
    }

    private void Update()
    {
        transform.localScale-=reduceDeltaVec*Time.deltaTime;
    }
}
