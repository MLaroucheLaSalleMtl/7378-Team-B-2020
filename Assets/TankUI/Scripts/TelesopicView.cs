using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelesopicView : MonoBehaviour
{
    public float zoomLevel = 2.0f;
    public float zoomInSpeed = 5.0f;
    public float zoomOutSpeed = 100.0f;

    private float initFOV;
    public GameObject obj;
    void Start()
    {
        //获取当前摄像机的视野范围 unity默认值60
        initFOV = Camera.main.fieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomInView();
            //激活ui窗口
            obj.SetActive(true);
        }
        else
        {
            ZoomOutView();
            //失活ui窗口
            obj.SetActive(false);
        }
        print("1: " + (Camera.main.fieldOfView - zoomInSpeed));
        print("2: " + initFOV / zoomLevel);
    }

    //放大摄像机的视野区域
    void ZoomInView()
    {
        if (Mathf.Abs(Camera.main.fieldOfView - (initFOV / zoomLevel)) < 0f)
        {
            Camera.main.fieldOfView = initFOV / zoomLevel;
        }
        else if (Camera.main.fieldOfView >= (initFOV / zoomLevel))
        {
            Camera.main.fieldOfView -= (Time.deltaTime * zoomInSpeed);
        }
    }

    //缩小摄像机的视野区域
    void ZoomOutView()
    {
        if (Mathf.Abs(Camera.main.fieldOfView - initFOV) < 0f)
        {
            Camera.main.fieldOfView = initFOV;
        }
        else if (Camera.main.fieldOfView + (Time.deltaTime * zoomOutSpeed) <= initFOV)
        {
            Camera.main.fieldOfView += (Time.deltaTime * zoomOutSpeed);
        }
    }
}
