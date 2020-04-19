using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject Light;
    public GameObject Medium;
    public GameObject Heavy;
    // Start is called before the first frame update
    private void Awake()
    {
        Spawn(Menu_Switch.tank_id);

    }
    void Start()
    {

    }

    private void Spawn(int id)
    {
        switch(id)
        {
            case 1:
                GameObject.Instantiate(Light, spawnPos.position, Quaternion.identity);
                break;
            case 2:
                GameObject.Instantiate(Medium, spawnPos.position, Quaternion.identity);
                break;
            case 3:
                GameObject.Instantiate(Heavy, spawnPos.position, Quaternion.identity);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
