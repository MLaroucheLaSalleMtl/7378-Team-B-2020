using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject Light;
    public GameObject Medium;
    public GameObject Heavy;
    private void Awake()
    {
        Spawn(Menu_Switch.tank_id);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Spawn(int id)
    {
        switch (id)
        {
            case 1:
                GameObject.Instantiate(Light, spawnPos.position, Quaternion.Euler(0, -90, 0));
                break;
            case 2:
                GameObject.Instantiate(Medium, spawnPos.position, Quaternion.Euler(0, -90, 0));
                break;
            case 3:
                GameObject.Instantiate(Heavy, spawnPos.position, Quaternion.Euler(0, -90, 0));
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
