using UnityEngine;
using UnityEngine.UI;
using TurretDemo;
namespace Turrets
{
    public class TurretRotation : MonoBehaviour
    {
        public bool runRotationsInFixed = false;

        [Header("Objects")]
        public Transform turretBase;
        public Transform turretBarrels;

        [Header("Rotation Limits")]
        [Tooltip("Turn rate of the turret's base and barrels in degrees per second.")]
        public float turnRate = 30.0f;
        public bool limitTraverse = false;
        [Range(0.0f, 180.0f)]
        public float leftTraverse = 60.0f;
        [Range(0.0f, 180.0f)]
        public float rightTraverse = 60.0f;
        [Range(0.0f, 90.0f)]
        public float elevation = 60.0f;
        [Range(0.0f, 90.0f)]
        public float depression = 5.0f;

        public bool showArcs = false;
        public bool showDebugRay = true;

        private Vector3 aimPoint;
        public Transform emptyPoint;
        public Image reticle;
        public Camera ShootCam;
        Camera MainCam;
        private bool aiming = false;
        private bool atRest = false;
        private bool lockTur = false;

        /// <summary>
        /// Turret is no longer aiming at anything, returns to resting position, and stops rotating.
        /// </summary>
        public bool Idle { get { return !aiming; } }

        /// <summary>
        /// Turret is idle and in a resting position.
        /// </summary>
        public bool AtRest { get { return atRest; } }

        public bool LockTur { get => lockTur; set => lockTur = value; }

        private void Start()
        {
            MainCam = Camera.main;
            //ShootCam = GameObject.FindGameObjectWithTag("ShootCamera").GetComponent<Camera>();
            if (aiming == false)
                aimPoint = transform.TransformPoint(Vector3.forward * 100.0f);
        }

        private void Update()
        {
            Camera cam;
            switch (TurrentTester.isAiming)
            {
                case true:
                    cam = ShootCam;
                    break;
                case false:
                    cam = Camera.main;
                    break;
                default:
                    cam = Camera.main;
                    break;
            }

            RaycastHit hit;
            if (Physics.Raycast(turretBarrels.transform.position, turretBarrels.transform.forward * 1000, out hit))
            {
                Vector2 screenPos = cam.WorldToScreenPoint(hit.point);
                reticle.rectTransform.position = screenPos;
            }
            else
            {
                Vector2 screenPos = cam.WorldToScreenPoint(emptyPoint.position);
                reticle.rectTransform.position = screenPos;
            }
            if (!runRotationsInFixed && !lockTur)
            {
                RotateTurret();
            }

            if (showDebugRay)
                DrawDebugRays();
        }

        private void FixedUpdate()
        {
            if (runRotationsInFixed && !lockTur)
            {
                RotateTurret();
            }
        }

        /// <summary>
        /// Give the turret a position to aim at. If not idle, it will rotate to aim at this point.
        /// </summary>
        public void SetAimpoint(Vector3 position)
        {
            aiming = true;
            aimPoint = position;
        }

        /// <summary>
        /// When idle, turret returns to resting position, will not track an aimpoint, and rotations stop updating.
        /// </summary>
        public void SetIdle(bool idle)
        {
            aiming = !idle;

            if (aiming)
                atRest = false;
        }

        /// <summary>
        /// Attempts to automatically assign the turretBase and turretBarrels transforms. Will search for a transform
        /// named "Base" for turretBase and a transform named "Barrels" for the turretBarrels.
        /// </summary>
        public void AutoPopulateBaseAndBarrels()
        {
            // Don't allow this while ingame.
            if (!Application.isPlaying)
            {
                turretBase = transform.Find("Base");
                if (turretBase != null)
                    turretBarrels = turretBase.Find("Barrels");
            }
            else
            {
                Debug.LogWarning(name + ": Turret cannot auto-populate transforms while game is playing.");
            }
        }

        /// <summary>
        /// Sets the turretBase and turretBarrels transforms to null.
        /// </summary>
        public void ClearTransforms()
        {
            // Don't allow this while ingame.
            if (!Application.isPlaying)
            {
                turretBase = null;
                turretBarrels = null;
            }
            else
            {
                Debug.LogWarning(name + ": Turret cannot clear transforms while game is playing.");
            }
        }

        private void RotateTurret()
        {
            if (aiming)
            {
                RotateBase();
                RotateBarrels();
            }
            else if (!atRest)
            {
                atRest = RotateToIdle();
            }
        }

        private void RotateBase()
        {
            if (turretBase != null)
            {
                Vector3 localTargetPos = transform.InverseTransformPoint(aimPoint);//旋转来自于父物体

                localTargetPos.y = 0.0f;

                Vector3 clampedLocalVec2Target = localTargetPos;//左右旋转限制
                if (limitTraverse)
                {
                    if (localTargetPos.x >= 0.0f)
                        clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * rightTraverse, float.MaxValue);
                    else
                        clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * leftTraverse, float.MaxValue);
                }

                // 创建本地旋转
                Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
                Quaternion newRotation = Quaternion.RotateTowards(turretBase.localRotation, rotationGoal, turnRate * Time.deltaTime);

                // 把旋转给到物体上
                turretBase.localRotation = newRotation;
            }
        }

        private void RotateBarrels()
        {

            if (turretBase != null && turretBarrels != null)
            {
                Vector3 localTargetPos = turretBarrels.parent.InverseTransformPoint(aimPoint);
                localTargetPos.x = 0.0f;

                Vector3 clampedLocalVec2Target = localTargetPos;
                if (localTargetPos.y >= 0.0f)
                    clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * elevation, float.MaxValue);
                else
                    clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * depression, float.MaxValue);
                Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
                Quaternion newRotation = Quaternion.RotateTowards(turretBarrels.localRotation, rotationGoal, 2.0f * turnRate * Time.deltaTime);

                turretBarrels.localRotation = newRotation;
            }
        }

        private bool RotateToIdle()
        {
            bool baseFinished = false;
            bool barrelsFinished = false;

            if (turretBase != null)
            {
                Quaternion newRotation = Quaternion.RotateTowards(turretBase.localRotation, Quaternion.identity, turnRate * Time.deltaTime);
                turretBase.localRotation = newRotation;

                if (turretBase.localRotation == Quaternion.identity)
                    baseFinished = true;
            }

            if (turretBarrels != null)
            {
                Quaternion newRotation = Quaternion.RotateTowards(turretBarrels.localRotation, Quaternion.identity, 2.0f * turnRate * Time.deltaTime);
                turretBarrels.localRotation = newRotation;

                if (turretBarrels.localRotation == Quaternion.identity)
                    barrelsFinished = true;
            }

            return (baseFinished && barrelsFinished);
        }

        private void DrawDebugRays()
        {
            if (turretBarrels != null)
                Debug.DrawRay(turretBarrels.position, turretBarrels.forward * 100.0f);
            else if (turretBase != null)
                Debug.DrawRay(turretBase.position, turretBase.forward * 100.0f);
        }
    }
}
