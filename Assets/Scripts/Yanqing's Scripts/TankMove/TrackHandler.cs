using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour
{
    public Transform[] LeftWheels;
    public Transform[] RightWheels;
    public Transform[] LeftBones;
    public Transform[] RightBones;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = LeftWheels[0].position.y - LeftBones[0].position.y;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < LeftWheels.Length; i++)
        {
            LeftBones[i].position  = new Vector3(LeftBones[i].position.x, LeftWheels[i].position.y - offset, LeftWheels[i].position.z);
            RightBones[i].position = new Vector3(RightBones[i].position.x, RightWheels[i].position.y - offset, RightWheels[i].position.z);
        }
    }
}
