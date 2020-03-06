using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Switch : MonoBehaviour
{
    public GameObject IS4;
    public GameObject BC25T;
    public GameObject Abram;
    public Transform spawner;
    private GameObject Selected_Vehicle;
    public Text tankname;
    // Start is called before the first frame update
    void Start()
    {
        Spawn_Light();
    }

    public void Spawn_Light()
    {
        Destroy(Selected_Vehicle);
        //Quaternion bcrt = new Quaternion(spawner.transform.rotation.x, -spawner.transform.rotation.y, spawner.transform.rotation.z,spawner.transform.rotation.w);
        tankname.text = "Bat.Chat 25T";
        Selected_Vehicle = GameObject.Instantiate(BC25T, spawner.transform.position, spawner.transform.rotation);
    }
    public void Spawn_Medium()
    {
        Destroy(Selected_Vehicle);
        tankname.text = "Abrams";
        Selected_Vehicle = GameObject.Instantiate(Abram, spawner.transform.position, spawner.transform.rotation);
    }
    public void Spawn_Heavy()
    {
        Destroy(Selected_Vehicle);
        tankname.text = "IS-4";
        Selected_Vehicle = GameObject.Instantiate(IS4, spawner.transform.position, spawner.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
