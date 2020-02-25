using System.Collections;
using UnityEngine;

namespace ChobiAssets.PTM
{
	
	public class AI_Share_Target_CS : MonoBehaviour
	{
        /*
		 * This script is automatically attached to "AI_Core" object in the AI tank by the "AI_CS" script, only when the "Commander" is set in the "AI_Settings_CS" script.
		 * This script works in combination with "AI_CS" in this tank, and "Aiming_Control_CS" in the commander.
		 * When the commander locks on the target, this tank also tries to attack the same target. This tank will start chasing the target when the target is captured by the main camera.
		 * When the commander is not locking on any target, this tank tries to find the enemy by itself following the commander. When the enemy is detected, this tank will start chasing and attacking it.
         */


        public AI_CS AI_Script; // Set by "AI_CS".

		Aiming_Control_CS commanderAimingScript;
        Transform initialFollowTarget;


		void Start()
		{
			Initialize();
		}


        void Initialize()
        {
            // Get "Aiming_Control_CS" in the commander.
            commanderAimingScript = AI_Script.Settings_Script.Commander.GetComponentInChildren<Aiming_Control_CS>();
            if (commanderAimingScript == null)
            {
                AI_Script.Is_Sharing_Target = false;
                Destroy(this);
            }

            // Store the initial follow target.
            initialFollowTarget = AI_Script.Settings_Script.Follow_Target;
        }


        void Update()
        {
            // Check the commander exists.
            if (AI_Script.Settings_Script.Commander == null)
            { // The commander might have been removed from the scene.
                // Stop sharing the target.
                AI_Script.Is_Sharing_Target = false;
                Destroy(this);
                return;
            }

            // Check the commander is living.
            if (commanderAimingScript == null)
            {
                if (AI_Script.Settings_Script.Commander.root.tag == "Finish")
                { // The commander has been destroyed.
                    // Stop sharing the target.
                    AI_Script.Is_Sharing_Target = false;
                    return;
                }
                else
                { // The commander has been respawned.
                    // Get "Aiming_Control_CS" again.
                    commanderAimingScript = AI_Script.Settings_Script.Commander.GetComponentInChildren<Aiming_Control_CS>();
                }
            }

            // Sharing Process
            if (commanderAimingScript.Target_Transform)
            { // The commander is locking on any target.
                if (AI_Script.Is_Sharing_Target)
                { // Now sharing
                    // Check the both tanks have the same target.
                    if (AI_Script.Target_Transform == commanderAimingScript.Target_Transform)
                    { // The both tanks have the same target.
                        return;
                    }
                }
                // Not sharing now, or the both tanks have different target.
                Share_Target();
            }
            else
            { // The commander is not locking on any target.
                if (AI_Script.Is_Sharing_Target)
                { // Now sharing
                    // Stop sharing the target.
                    AI_Script.Is_Sharing_Target = false;
                    // Reset the follow target.
                    AI_Script.Settings_Script.Follow_Target = initialFollowTarget;
                    AI_Script.Reset_Settings();
                }
            }

            if (AI_Script.Settings_Script.Follow_Target == null)
            {
                print("Call / " + AI_Script.Is_Sharing_Target + " /" + commanderAimingScript.Target_Transform);
            }
        }


        void Share_Target()
        {
            // Get the target information.
            AI_Headquaters_Helper_CS targetAIHelperScript = commanderAimingScript.Target_Transform.GetComponentInParent<AI_Headquaters_Helper_CS>();
            if (targetAIHelperScript == null)
            {
                return;
            }

            // Check the target is visible by the camera.
            if (Check_Visibility(targetAIHelperScript) == false)
            { // The target is not visible.
              // Stop sharing the target.
                AI_Script.Is_Sharing_Target = false;
                return;
            }
            // The target is visible.

            // Start sharing the target.
            AI_Script.Is_Sharing_Target = true;
            // Assign the target to the follow target. >> The tank will follow the target.
            AI_Script.Settings_Script.Follow_Target = targetAIHelperScript.transform;
            AI_Script.Reset_Settings();
            // Send the target information to the "AI_CS".
            AI_Script.Set_Target(targetAIHelperScript);
        }


        bool Check_Visibility(AI_Headquaters_Helper_CS targetAIHelperScript)
        {
            RaycastHit raycastHit;
            Vector3 tempTargetPosition = targetAIHelperScript.Body_Transform.position + (targetAIHelperScript.Body_Transform.up * targetAIHelperScript.Visibility_Upper_Offset);
            if (Physics.Linecast(Camera.main.transform.position, tempTargetPosition, out raycastHit, Layer_Settings_CS.Layer_Mask))
            {
                if (raycastHit.transform.root == targetAIHelperScript.Body_Transform.root)
                { // The ray hits the target.
                    return true;
                }
                else
                { // The ray hits other object.
                    return false;
                }
            }
            else
            { // The ray does not hit anything. >> There is no obstacle between the camera and the target.
                return true;
            }
        }

	}

}