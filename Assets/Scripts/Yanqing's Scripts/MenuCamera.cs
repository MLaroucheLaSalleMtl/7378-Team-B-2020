using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
    private const float YLimit_Min = 0f;
    private const float YLimit_Max = 89.0f;

    public Transform LookAt;
    public Transform camTransform;

    public bool IsAiming;

    private Camera cam;

    private float distance = 15.0f;
    private float current_distance;
    private float m_distance;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    private bool collided = false;

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
        //if (this.transform.position.y <= LookAt.position.y - 3)
        //{
        //    this.transform.position = new Vector3(this.transform.position.x, LookAt.position.y - 1, this.transform.position.z);
        //}
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

            currentY = Mathf.Clamp(currentY, YLimit_Min, YLimit_Max);//limit top and bottom
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        //if (!collided)
        //{
        //    m_distance = distance;//temp deprecated
        //}
        //int layerMask = 1 << 16;
        //RaycastHit hit;
        //if (Physics.Linecast(LookAt.position, this.transform.position, out hit, layerMask))
        //{
        //    collided = true;
        //    string name = hit.collider.gameObject.tag;
        //    print(name);
        //    if (name != "MainCamera")
        //    {
        //        //如果射线碰撞的不是相机，那么就取得射线碰撞点到玩家的距离
        //        current_distance = Vector3.Distance(hit.point, LookAt.position);
        //        //如果射线碰撞点小于玩家与相机本来的距离，就说明角色身后是有东西，为了避免穿墙，就把相机拉近
        //    }
        //}
        //else
        //{
        //    print("no");
        //    collided = false;
        //}
        //if (collided)
        //{
        //    distance = current_distance - 0.5f;// Mathf.Lerp(distance, current_distance - 0.5f, Time.deltaTime * 5);
        //}
        //else
        //{
        //    distance = Mathf.Lerp(distance, m_distance, Time.deltaTime * 5);
        //}

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance += 1;
        } 
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance -= 1;
        }
    }

}
