using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    
    public Trigger_of_light trigger_Of_Light;
    public float y =0;
    public GameObject player;
    public bool target_is_in_sight = true;
    public int max_distance = 8;
    Transform original_position;
    public float my=0;
    public bool nocollider=true;
    public GameObject light_move;
    public LayerMask tower;
    public bool Instantiate_only_once = false;
    public bool turnright = false;
    public bool turnleft = false;
    public bool finish_searching = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.black);
        print(transform.localRotation.x);
        if (trigger_Of_Light.target_is_found==false&&nocollider==true)//if player is not touched,rotate
        {
            spotlight_rotation();
        }
        else if(trigger_Of_Light.target_is_found == true&&target_is_in_sight==true)//if it is touched , the light follows player
        {
            
            
            
                transform.LookAt(player.transform);//when the light collide with player, the light follows player
            
            


            if (Vector3.Distance(transform.position,player.transform.position)>max_distance)//if player goes too far away, it went back to the rotation
            {
                
                target_is_in_sight = false;
                
                
                trigger_Of_Light.target_is_found = false;
            }
             if (Physics.Linecast(transform.position, player.transform.position, tower))//if player hide behind the wall
            {
                nocollider = false;
                original_position = player.transform;
                target_is_in_sight = false;
                
                
                
            }
             else if(!Physics.Linecast(transform.position, player.transform.position, tower))
            {
                nocollider = true;
            }
        }
        else if(nocollider==false&&target_is_in_sight==false&&trigger_Of_Light.target_is_found==true)
        {
            //transform.LookAt(light_move.transform);
            

            light_move.transform.parent = null;
            StartCoroutine(wait());
            
            //if (my<5)
            //{
            //    Vector3 newPos = new Vector3(light_move.transform.position.x, light_move.transform.position.y, light_move.transform.position.z + 0.1f);
            //    light_move.transform.position = newPos;
            //    my = my + 0.1f;
            //}

            //do
            //{
            //    my = my - 0.2f;
            //    Vector3 newPos = new Vector3(light_move.transform.position.x, light_move.transform.position.y, light_move.transform.position.z - 0.2f);
            //    light_move.transform.position = newPos;
            //} while (my > -1);



            //trigger_Of_Light.target_is_found = false;
        }



    }
    public void spotlight_rotation()
    {
        
        y = y + 0.7f;
        
        transform.rotation = Quaternion.Euler(45, y, 0);
    }
    IEnumerator wait()
    {

        //Instantiate(light_move,original_position.position,Quaternion.Euler(0,0,0));
        //transform.LookAt(light_move.transform.position);
        //yield return new WaitForSeconds(2f);
        //do
        //{
        //    my = my + 0.1f;
        //    light_move.transform.position.Set(original_position.position.x, original_position.position.y, original_position.position.z + 10f);
        //} while (my < 3f);
         //yield return new WaitForSeconds(3f);
        my = my + 0.1f;
        if(my<10)
        {
            transform.Rotate(new Vector3(0, 0.5f, 0), Space.Self);
        }
        else if(my<20&&my>10)
        {
            transform.Rotate(new Vector3(0, -0.5f, 0), Space.Self);
        }
        
        //transform.Rotate(new Vector3(0, 0.5f, 0), Space.Self);
        //yield return new WaitForSeconds(2f);
        //transform.Rotate(new Vector3(0, -0.5f, 0), Space.Self);
        yield return new WaitForSeconds(3f);

        trigger_Of_Light.target_is_found = false;
        nocollider = true;
    }
}
