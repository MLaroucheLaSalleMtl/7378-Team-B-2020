using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPos;
    public GameObject Light;
    public GameObject Medium;
    public GameObject Heavy;
    public GameObject[] enemys;
    // Start is called before the first frame update
    private void Awake()
    {
        Spawn(Menu_Switch.tank_id);
    }
    void Start()
    {
        StartCoroutine(spawnenemy());
    }
    IEnumerator spawnenemy()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject enemy in enemys)
        {
            enemy.SetActive(true);
        }
    }
    private void Spawn(int id)
    {
        switch (id)
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
}
