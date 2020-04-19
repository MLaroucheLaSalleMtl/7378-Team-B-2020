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
    public static int tank_id = 1;
    public static bool Tutorial_finished;
    public GameObject TutorialButton;

    public Slider Damage;
    public Slider Armor;
    public Slider Mobility;
    public Slider ReloadSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn_Light()
    {
        tank_id = 1;
        Destroy(Selected_Vehicle);
        //Quaternion bcrt = new Quaternion(spawner.transform.rotation.x, -spawner.transform.rotation.y, spawner.transform.rotation.z,spawner.transform.rotation.w);
        tankname.text = "Bat.Chat 25T";
        Selected_Vehicle = GameObject.Instantiate(BC25T, spawner.transform.position, spawner.transform.rotation);
        Damage.value = 0.3f;
        Armor.value = 0.2f;
        Mobility.value = 0.8f;
        ReloadSpeed.value = 0.6f;
    }
    public void Spawn_Medium()
    {
        tank_id = 2;
        Destroy(Selected_Vehicle);
        tankname.text = "121";
        Selected_Vehicle = GameObject.Instantiate(Abram, spawner.transform.position, spawner.transform.rotation);
        Damage.value = 0.44f;
        Armor.value = 0.5f;
        Mobility.value = 0.6f;
        ReloadSpeed.value = 0.5f;
    }
    public void Spawn_Heavy()
    {
        tank_id = 3;
        Destroy(Selected_Vehicle);
        tankname.text = "T110E5";
        Selected_Vehicle = GameObject.Instantiate(IS4, spawner.transform.position, spawner.transform.rotation);
        Damage.value = 0.6f;
        Armor.value = 0.9f;
        Mobility.value = 0.35f;
        ReloadSpeed.value = 0.45f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Tutorial_finished)
        {
            TutorialButton.SetActive(true);
        }
    }
}
