﻿using UnityEngine;
using UnityEngine.UI;

namespace Turrets
{
    public class TurrentRotation : MonoBehaviour
    {
        [Tooltip("Should turret rotate in the FixedUpdate rather than Update?")]
        public bool runRotationsInFixed = false;

        [Header("Objects")]
        [Tooltip("Transform used to provide the horizontal rotation of the turret.")]
        public Transform turretBase;
        [Tooltip("Transform used to provide the vertical rotation of the barrels. Must be a child of the TurretBase.")]
        public Transform turretBarrels;

        [Header("Rotation Limits")]
        [Tooltip("Turn rate of the turret's base and barrels in degrees per second.")]
        public float turnRate = 30.0f;
        [Tooltip("When true, turret rotates according to left/right traverse limits. When false, turret can rotate freely.")]
        public bool limitTraverse = false;
        [Tooltip("When traverse is limited, how many degrees to the left the turret can turn.")]
        [Range(0.0f, 180.0f)]
        public float leftTraverse = 60.0f;
        [Tooltip("When traverse is limited, how many degrees to the right the turret can turn.")]
        [Range(0.0f, 180.0f)]
        public float rightTraverse = 60.0f;
        [Tooltip("How far up the barrel(s) can rotate.")]
        [Range(0.0f, 90.0f)]
        public float elevation = 60.0f;
        [Tooltip("How far down the barrel(s) can rotate.")]
        [Range(0.0f, 90.0f)]
        public float depression = 5.0f;

        [Header("Utilities")]
        [Tooltip("Show the arcs that the turret can aim through.\n\nRed: Left/Right Traverse\nGreen: Elevation\nBlue: Depression")]
        public bool showArcs = false;
        [Tooltip("When game is running in editor, draws a debug ray to show where the turret is aiming.")]
        public bool showDebugRay = true;

        private Vector3 aimPoint;

        private bool aiming = false;
        private bool atRest = false;

        /// <summary>
        /// Turret is no longer aiming at anything, returns to resting position, and stops rotating.
        /// </summary>
        public bool Idle { get { return !aiming; } }

        /// <summary>
        /// Turret is idle and in a resting position.
        /// </summary>
        public bool AtRest { get { return atRest; } }

        private void Start()
        {
            if (aiming == false)
                aimPoint = transform.TransformPoint(Vector3.forward * 100.0f);
            print(Vector3.forward * 100f);
        }

        private void Update()
        {
            if (!runRotationsInFixed)
            {
                RotateTurret();
            }

            if (showDebugRay)
                DrawDebugRays();

        }

        private void FixedUpdate()
        {
            if (runRotationsInFixed)
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

        //炮塔默认状态

        public void SetIdle(bool idle)
        {
            aiming = !idle;

            if (aiming)
                atRest = false;
        }

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
        //归零
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
                //瞄准坐标来自父物体
                Vector3 localTargetPos = transform.InverseTransformPoint(aimPoint);
                localTargetPos.y = 0.0f;

                //限制炮塔左右转动
                Vector3 clampedLocalVec2Target = localTargetPos;
                if (limitTraverse)
                {
                    if (localTargetPos.x >= 0.0f)
                        clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * rightTraverse, float.MaxValue);
                    else
                        clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * leftTraverse, float.MaxValue);
                }

                //创建本地旋转
                Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
                Quaternion newRotation = Quaternion.RotateTowards(turretBase.localRotation, rotationGoal, turnRate * Time.deltaTime);

                //把旋转赋值给炮塔底座
                turretBase.localRotation = newRotation;
            }
        }

        private void RotateBarrels()
        {

            if (turretBase != null && turretBarrels != null)
            {
                Vector3 localTargetPos = turretBase.InverseTransformPoint(aimPoint);
                localTargetPos.x = 0.0f;

                //俯仰角
                Vector3 clampedLocalVec2Target = localTargetPos;
                if (localTargetPos.y >= 0.0f)
                    clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * elevation, float.MaxValue);
                else
                    clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * depression, float.MaxValue);

                //创建本地旋转
                Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
                Quaternion newRotation = Quaternion.RotateTowards(turretBarrels.localRotation, rotationGoal, 2.0f * turnRate * Time.deltaTime);

                //把旋转赋值给炮塔底座
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
        Vector3 UIToWorldMapDistance(Image ui)//瞄准星的3维坐标，z表示此元件在屏幕上的位置到世界地图上的距离
        {
            float cameraHeight = Camera.main.transform.position.y;//摄像机到世界的距离
            Vector3 screenPos = ui.rectTransform.transform.position;

            bool highThanCenter = screenPos.y > Screen.height * 0.5f;
            float ratio = Mathf.Abs((screenPos.y - Screen.height * 0.5f) / (Screen.height * 0.5f));//算出ui在屏幕上的比例关系
            float centerLineLength = cameraHeight / Mathf.Cos(Mathf.Deg2Rad * (90 - Camera.main.transform.eulerAngles.x));
            float bottomLength = centerLineLength * Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView * 0.5f);
            float acturalLength = bottomLength * ratio;
            float requireAngle = Mathf.Atan(acturalLength / centerLineLength);
            float bevelLength = cameraHeight / Mathf.Cos(Mathf.Deg2Rad * (90 - Camera.main.transform.eulerAngles.x) + (highThanCenter ? requireAngle : -requireAngle));
            float finalLength = bevelLength * Mathf.Cos(requireAngle);//瞄准星在屏幕上的位置到世界地图上的距离

            screenPos.z = finalLength;
            return screenPos;
        }
    }

}
