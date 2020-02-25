using System.Collections;
using UnityEngine;

namespace ChobiAssets.PTM
{
	
	public class Suspension_Arm_Control_CS : MonoBehaviour
	{

		public Transform This_Transform;
		public Transform Target_Transform;

		float offsetHeight;
		Vector3 difference;
		Vector3 currentAngles;


		void Start()
		{
			if (This_Transform == null) {
				This_Transform = transform;
			}

			if (Target_Transform == null) {
				Destroy(this);
			}

			offsetHeight = Target_Transform.localPosition.y - This_Transform.localPosition.y;
		}


		void Update()
		{
			difference = Target_Transform.localPosition - This_Transform.localPosition;
			difference.y -= offsetHeight;
			currentAngles.z = Mathf.Atan(difference.y / difference.x) * Mathf.Rad2Deg;
			This_Transform.localEulerAngles = currentAngles;
		}


	}

}