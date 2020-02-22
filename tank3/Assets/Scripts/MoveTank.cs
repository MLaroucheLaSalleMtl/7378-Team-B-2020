using UnityEngine;
using System.Collections;

public class MoveTank : MonoBehaviour 
{
	public Rigidbody FrontLeft;
	public Rigidbody RearLeft;
	public Rigidbody FrontRight;
	public Rigidbody RearRight;
    public Rigidbody hull;
//
//	public Rigidbody LeftRoll;
//	public Rigidbody RightRoll;
//
	private int wheelTorque;

	private int fLDir;
	private int fRDir;
	private int rLDir;
	private int rRDir;

	void Update()
	{

	}

	void FixedUpdate()
	{

        if (Input.GetKey(KeyCode.W))
		{
            wheelTorque = -10000;
            /*FrontLeft.AddForce(-Vector3.forward);
            RearLeft.AddForce(-Vector3.forward);
            FrontRight.AddForce(-Vector3.forward);
            RearRight.AddForce(-Vector3.forward);
            hull.AddForce(Vector3.forward * -10000);*/
            FrontLeft.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
			RearLeft.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
			FrontRight.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
			RearRight.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
            //			LeftRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
            //			RightRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
        }		
	}
}
