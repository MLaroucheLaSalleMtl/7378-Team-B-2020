using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explode;
    public GameObject self;
    public float speed = 10;
    public float timer = 0;
    public float waittime = 1;
    public bool collided = false;
    public bool onlyonce = false;
    RaycastHit hit;
    public float ExplodeRadius = 10;
    public LayerMask LayerMask;
    public float ExplodeDistance;//the distance between shell and enemy, it is used to figure out how much damage the enemy will take
    public bool EnemyInRange = false;//it is used to check if the enemy enter 
    public int i = 0;
    public enemyHealth enemyhp;
    public bool damageonce = true;

    void Start()
    {
        enemyhp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyHealth>();
    }


    void Update()
    {


        //if (Physics.SphereCast(transform.position, ExplodeRadius, transform.forward, out hit, 0))
        //{
        //    Debug.Log("yes");
        //    Debug.Log(hit.transform.tag);
        //    if (hit.transform.tag == "Enemy")
        //    {
        //        EnemyInRange = true;


        //        if (ExplodeDistance < 3)
        //        {
        //            enemyHealth.DoDamage(damage * 1f);
        //        }
        //        else if (ExplodeDistance > 3 && ExplodeDistance < 7)
        //        {
        //            enemyHealth.DoDamage(damage * 0.8f);
        //        }
        //        else if (ExplodeDistance > 7 && ExplodeDistance <= 10)
        //        {
        //            enemyHealth.DoDamage(damage * 0.5f);
        //        }
        //    }
        //}







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


        //if (collided && EnemyInRange)
        //{
        //    ExplodeDistance = Vector3.Distance(transform.position, hit.transform.position);
        //}
    }


    // Update is called once per frame



    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        Instantiate(explode, self.transform.position, Quaternion.identity);

        if (collision.transform.tag == "Enemy")
        {
            enemyhp.DoDamage(50f);
        }






    }
}
