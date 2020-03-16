using UnityEngine;
using Turrets;
using UnityEngine.UI;
namespace TurretDemo
{
    public class TurrentTester : MonoBehaviour
    {
        public TurretRotation[] turret;
        public Vector3 targetPos;
        public Transform targetTransform;
        Camera cam;
        [Space]
        public bool turretsIdle = false;

        void Start()
        {
            cam = GetComponent<Camera>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                turretsIdle = !turretsIdle;

            Ray ray = cam.ScreenPointToRay(new Vector3(960, 583, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.yellow);
                targetPos = hit.point;
                //Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.direction * 1000, Color.white);
                targetPos = ray.direction * 1000;
                //Debug.Log("did not hit");
            }

            foreach (TurretRotation tur in turret)
            {
                if (targetTransform == null)
                    tur.SetAimpoint(targetPos);
                else
                    tur.SetAimpoint(targetTransform.position);

                tur.SetIdle(turretsIdle);
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetPos, 1.0f);
        }

    }
}
