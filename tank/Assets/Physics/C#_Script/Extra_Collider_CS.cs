using UnityEngine;
using System.Collections;

namespace ChobiAssets.PTM
{

    public class Extra_Collider_CS : MonoBehaviour
    {
        /*
		 * This script is attached to the "Extra_Collier" in the tank.
		 * This script only sets the Layer of this gameobject.
		*/


        void Start()
        {
            gameObject.layer = Layer_Settings_CS.Extra_Collider_Layer;

            Destroy(this);
        }

    }

}