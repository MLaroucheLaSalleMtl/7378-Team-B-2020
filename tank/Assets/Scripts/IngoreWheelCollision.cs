using UnityEngine;
using System.Collections;

public class IngoreWheelCollision : MonoBehaviour 
{
	void Start()
	{
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("OutSideWheel"),LayerMask.NameToLayer("InSideWheel"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("OutSideWheel"),LayerMask.NameToLayer("TankBody"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("InSideWheel"),LayerMask.NameToLayer("TankBody"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("TankTrack"),LayerMask.NameToLayer("TankBody"));
	}
}
