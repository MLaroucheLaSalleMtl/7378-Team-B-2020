using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShellExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("GameObjects")]
    public GameObject explode;
    public GameObject self;

    [Header("Set Disappearance Of Shell")]
    public bool ExplodeOnlyOnce = false;
    public float timer = 0;
    public float waittime = 1;
    public bool collided = false;
    public bool onlyonce = false;



    void Start()
    {

    }


    void Update()
    {










        if (collided)
        {





            if (onlyonce == false)
            {
                timer += Time.deltaTime;
                if (timer >= waittime)
                {

                    self.SetActive(false);
                }
                if (timer > waittime)
                {

                    onlyonce = true;
                }
            }

        }



    }


    // Update is called once per frame



    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        if (!ExplodeOnlyOnce)
        {
            Instantiate(explode, self.transform.position, Quaternion.identity);
            ExplodeOnlyOnce = true;
        }



        if (collision.transform.tag == "Player")
        {
            PlayerHealth playerhp = collision.transform.GetComponent < PlayerHealth>();
            playerhp.DoDamage(50f);
        }






    }
}
