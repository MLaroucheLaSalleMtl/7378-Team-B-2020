using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChobiAssets.PTM
{


    public class Utility_Add_Extra_Collider_CS : MonoBehaviour
    {

        [SerializeField] float radiusOffset = 0.0f;


        [ContextMenu("Add Extra Collider")]
        void Add_Extra_Collide ()
        {
            var childTransforms = GetComponentsInChildren<Transform>();
            foreach(Transform childTransform in childTransforms)
            {
                if (childTransform.gameObject.layer != Layer_Settings_CS.Wheels_Layer)
                { // The child is not a wheel.
                    continue;
                }

                if (childTransform.childCount > 0)
                { // The child has already Extra_Collider.
                    continue;
                }

                GameObject newObject = new GameObject();
                newObject.name = "Extra_Collider";
                newObject.layer = Layer_Settings_CS.Extra_Collider_Layer;
                newObject.transform.parent = childTransform;
                newObject.transform.localPosition = Vector3.zero;
                var sphereCollider = newObject.AddComponent<SphereCollider>();
                sphereCollider.radius = childTransform.GetComponent<SphereCollider>().radius + radiusOffset;
                newObject.AddComponent<Extra_Collider_CS>();
            }

        }


    }

}
