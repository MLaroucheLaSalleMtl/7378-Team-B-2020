using System;
using System.Collections;
using System.Collections.Generic;
using TurretDemo;
using Turrets;
using UnityEngine;
using UnityEngine.UI;

public class TankCamera : MonoBehaviour
{
    public static TankCamera Ins;
    private const float YLimit_Min = -50.0f;
    private const float YLimit_Max = 89.0f;
    private static float Shoot_Y_Min;
    private static float Shoot_Y_Max;

    public Transform LookAt;
    public Transform TPS_camTransform;
    public Transform ShootCam;
    public Transform ShootCamBase;

    private TurrentTester tr;

    public bool IsAiming;
    private bool DoOnce;

    private Camera cam;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        Ins = this;
    }

    void Start()
    {
        ShootCamBase = GameObject.FindGameObjectWithTag("ShootCamBase").transform;
        LookAt = GameObject.FindGameObjectWithTag("CameraPivot").transform;
        LockCursor();
        cam = Camera.main;
        Shoot_Y_Min = -GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TurretRotation>().elevation;
        Shoot_Y_Max = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TurretRotation>().depression;
        TPS_camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    public void  LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void clampCamera()
    {

    }
    private void LateUpdate()
    {

    }

    private void aimCam()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || distance == 5 )
        {       
            switch(TurrentTester.isAiming)
            {
                case false:
                    TurrentTester.isAiming = true;
                    distance = 4;
                    break;
                case true:
                    TurrentTester.isAiming = false;
                    distance = 6;
                    break;
            }
        }
        if(!TurrentTester.isAiming)
        {
            RotateTPS();
        }
        else
        {
            RotateShoot();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance += 1;
        }
        //Zoom in  
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance -= 1;
        }
        if(distance >= 20)
        {
            distance = 20;
        }
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, YLimit_Min, YLimit_Max);
    }
    private void RotateTPS()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        TPS_camTransform.position = LookAt.position + rotation * dir;
        if (TPS_camTransform.position.y <= LookAt.position.y - 3)
        {
            TPS_camTransform.position = new Vector3(TPS_camTransform.position.x, LookAt.position.y - 3, TPS_camTransform.position.z);
        }
        TPS_camTransform.LookAt(LookAt.position);
    }

    private void RotateShoot()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        ShootCamBase.transform.rotation = rotation;
        currentY = Mathf.Clamp(currentY, Shoot_Y_Min, Shoot_Y_Max);
    }
   
}
