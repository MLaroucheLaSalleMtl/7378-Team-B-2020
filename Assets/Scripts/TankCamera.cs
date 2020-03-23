using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankCamera : MonoBehaviour
{
    private const float YLimit_Min = -50.0f;
    private const float YLimit_Max = 89.0f;

    public Transform LookAt;
    public Transform camTransform;


    public bool IsAiming;

    private Camera cam;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camTransform = transform;
        cam = Camera.main;
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

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = LookAt.position + rotation * dir;
        if (transform.position.y <= LookAt.position.y - 3)
        {
            transform.position = new Vector3(transform.position.x, LookAt.position.y - 3, transform.position.z);
        }
        camTransform.LookAt(LookAt.position);
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
   
}
