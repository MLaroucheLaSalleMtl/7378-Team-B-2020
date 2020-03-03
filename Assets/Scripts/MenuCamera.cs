using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
    private const float YLimit_Min = -50.0f;
    private const float YLimit_Max = 89.0f;

    public Transform LookAt;
    public Transform camTransform;


    public bool IsAiming;

    private Camera cam;

    private float distance = 15.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

        camTransform = transform;
        cam = Camera.main;
        currentX = cam.transform.localEulerAngles.y;
        currentY = cam.transform.localEulerAngles.x;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = LookAt.position + rotation * dir;
        camTransform.LookAt(LookAt.position);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                distance += 1;
            }
            //Zoom in  
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                distance -= 1;
            }
            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, YLimit_Min, YLimit_Max);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
    }

}
