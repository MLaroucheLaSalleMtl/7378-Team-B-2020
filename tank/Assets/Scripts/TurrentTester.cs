using UnityEngine;
using Turrets;
using UnityEngine.UI;
namespace TurretDemo
{
    public class TurrentTester : MonoBehaviour
    {
        Camera cam;
        public TurrentRotation[] turret;
        public Vector3 targetPos;
        public Transform targetTransform;

        [Space]
        public bool turretsIdle = false;

        private void Start()
        {
            cam = GetComponent<Camera>();
        }
        private void Update()
        {
            // Toggle turret idle.
            if (Input.GetKeyDown(KeyCode.E))
                turretsIdle = !turretsIdle;

            Ray ray = cam.ScreenPointToRay(new Vector3(960, 583, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                targetPos = hit.point;
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);
                targetPos = ray.direction * 1000;
                Debug.Log("Did not Hit");
            }


            // When a transform is assigned, pass that to the turret. If not,
            // just pass in whatever this is looking at.
            //targetPos = transform.TransformPoint(Vector3.forward * 200.0f);



            //print(ray.direction);
            foreach (TurrentRotation tur in turret)
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
