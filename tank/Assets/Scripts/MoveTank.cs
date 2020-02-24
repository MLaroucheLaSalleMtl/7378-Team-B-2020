using UnityEngine;
using System.Collections;

public class MoveTank : MonoBehaviour
{
    public Rigidbody FrontLeft;
    public Rigidbody RearLeft;
    public Rigidbody FrontRight;
    public Rigidbody RearRight;
    public Transform hull;
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
            wheelTorque = 150000;
            hull.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1) * 30000);
            //         FrontLeft.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Force);
            //RearLeft.AddRelativeTorque(Vector3.right*wheelTorque, ForceMode.Force);
            //FrontRight.AddRelativeTorque(Vector3.right*wheelTorque, ForceMode.Force);
            //RearRight.AddRelativeTorque(Vector3.right*wheelTorque, ForceMode.Force);
            //LeftRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);
            //RightRoll.AddRelativeTorque(Vector3.right*wheelTorque,ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.S))
        {
            hull.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -50000);
            //FrontLeft.AddRelativeTorque(Vector3.right * -wheelTorque, ForceMode.Force);
            //RearLeft.AddRelativeTorque(Vector3.right * -wheelTorque, ForceMode.Force);
            //FrontRight.AddRelativeTorque(Vector3.right * -wheelTorque, ForceMode.Force);
            //RearRight.AddRelativeTorque(Vector3.right * -wheelTorque, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            hull.transform.Rotate(0, -25 * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hull.transform.Rotate(0, 25 * Time.deltaTime, 0, Space.Self);
        }
    }
}
