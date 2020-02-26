using UnityEngine;
using Turrets;
using UnityEngine.UI;
namespace TurretDemo
{
    public class TurrentTester : MonoBehaviour
    {
        public TurrentRotation[] turret;
        public Vector3 targetPos;
        public Transform targetTransform;
        //this is just for testing purpose

        [Space]
        public bool turretsIdle = false;

        private void Update()
        {
            // Toggle turret idle.
            if (Input.GetKeyDown(KeyCode.E))
                turretsIdle = !turretsIdle;

            // When a transform is assigned, pass that to the turret. If not,
            // just pass in whatever this is looking at.
            targetPos = transform.TransformPoint(Vector3.forward * 200.0f);
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
