using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_of_light : MonoBehaviour
{
    
    RaycastHit hit;
    public bool target_is_found = false;
    public bool alarm_play=false;
    public bool alarm_play_once = false;
    public script script;
    public AudioClip alarmaudio;
    
    
    [SerializeField] private AudioSource alarm;
    // Start is called before the first frame update
    void Start()
    {
        
        AudioSource alarm = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction1 = transform.TransformDirection(0.086f, 0, 1) * 8;
        Vector3 direction2 = transform.TransformDirection(0, 0.086f, 1) * 8;
        Vector3 direction3 = transform.TransformDirection(-0.086f, 0f, 1f) * 8;
        Vector3 direction4 = transform.TransformDirection(0, -0.086f, 1) * 8;
        // use four lines to detect the player
        //Debug.DrawRay(transform.position, direction1, Color.black);
        //Debug.DrawRay(transform.position, direction2, Color.black);
        //Debug.DrawRay(transform.position, direction3, Color.black);
        //Debug.DrawRay(transform.position, direction4, Color.black);
        if (Physics.Raycast(transform.position, direction1, out hit, 10))
        {
            Debug.Log(hit.transform.name + "hit");
            if(hit.transform.tag=="Player")
            {
                target_is_found = true;
                script.target_is_in_sight = true;
                alarm_play = true;
            }
        }
        if (Physics.Raycast(transform.position, direction2, out hit, 10))
        {
            Debug.Log(hit.transform.name + "hit");
            if (hit.transform.tag == "Player")
            {
                target_is_found = true;
                script.target_is_in_sight = true;
                alarm_play = true;
            }
        }
        if (Physics.Raycast(transform.position, direction3, out hit, 10))
        {
            Debug.Log(hit.transform.name + "hit");
            if (hit.transform.tag == "Player")
            {
                target_is_found = true;
                script.target_is_in_sight = true;
                alarm_play = true;
            }
        }
        if (Physics.Raycast(transform.position, direction4, out hit, 10))
        {
            Debug.Log(hit.transform.name + "hit");
            if (hit.transform.tag == "Player")
            {
                target_is_found = true;
                script.target_is_in_sight = true;
                alarm_play = true;
            }
        }


        if(alarm_play==true&&alarm_play_once==false)
        {
            alarm.Play();
            alarm_play_once = true;
            
        }
    }
}
