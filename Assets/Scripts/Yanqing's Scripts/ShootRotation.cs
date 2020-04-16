using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turrets;

public class ShootRotation : MonoBehaviour
{
    public TurretRotation turret;
    private Vector3 targetPos;
    private bool lockTur;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        turret = GameObject.FindGameObjectWithTag("Turrent").GetComponent<TurretRotation>();
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(960, 583, 0));
        LayerMask layerMask = 1 << 16;
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            targetPos = hit.point;
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.GetPoint(1000), Color.white);
            targetPos = ray.GetPoint(1000);
            //Debug.Log("did not hit");
        }

        if (lockTur)
        {
            turret.LockTur = lockTur;
        }
        else
        {
            turret.LockTur = false;
        }
        turret.SetAimpoint(targetPos);
    }
}
