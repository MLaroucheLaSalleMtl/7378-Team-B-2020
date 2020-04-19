using UnityEngine;
using Turrets;
using UnityEngine.UI;
namespace TurretDemo
{
    public class TurrentTester : MonoBehaviour
    {
        public TurretRotation[] turret;
        public GameObject ShootCam;
        public Transform ShootCamBase;
        public Vector3 targetPos;
        public Transform targetTransform;
        private bool lockTur;
        public static bool isAiming = false;
        Camera cam;
        [Space]
        public bool turretsIdle = false;

        void Start()
        {
            ShootCam = GameObject.FindGameObjectWithTag("ShootCamera");
            ShootCam.SetActive(false);
            ShootCamBase = GameObject.FindGameObjectWithTag("ShootCamBase").transform;
            turret[0] = GameObject.FindGameObjectWithTag("Turrent").GetComponent<TurretRotation>();
            cam = Camera.main;
            print("finished");
        }
        private void Update()
        {

            if (!isAiming)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    turretsIdle = !turretsIdle;

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
                if (Input.GetMouseButton(1))
                {
                    lockTur = true;
                }
                else
                {
                    lockTur = false;
                }
                ShootCamBase.LookAt(targetPos);

                foreach (TurretRotation tur in turret)
                {
                    if (lockTur)
                    {
                        tur.LockTur = lockTur;
                    }
                    else
                    {
                        tur.LockTur = false;
                    }
                    if (targetTransform == null)
                        tur.SetAimpoint(targetPos);
                    else
                        tur.SetAimpoint(targetTransform.position);

                    tur.SetIdle(turretsIdle);
                }
            }
            ActivateShoot();
        }

        public void ActivateShoot()
        {
            if(isAiming)
            {
                turret[0].runRotationsInFixed = true;
                ShootCam.SetActive(true);
            }
            else
            {
                turret[0].runRotationsInFixed = false;
                ShootCam.SetActive(false);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetPos, 1.0f);
        }

    }
}
