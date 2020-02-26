using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSTR : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(960, 583, 0));
        Debug.DrawRay(ray.origin, ray.direction * 200, Color.yellow);
    }
}
