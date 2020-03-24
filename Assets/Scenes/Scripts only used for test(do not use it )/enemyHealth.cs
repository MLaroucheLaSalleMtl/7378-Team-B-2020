using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    private float maxhealth = 100f;
    public float CurrentHealth;

    public explosion explosion;
    public float damage = 50f;
    public GameObject enemy;






    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxhealth;


    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (explosion.ExplodeDistance < 3 && explosion.ExplodeDistance > 0)
        {
            DoDamage(damage * 1f);
        }
        else if (explosion.ExplodeDistance > 3 && explosion.ExplodeDistance < 7)
        {
            DoDamage(damage * 0.8f);
        }
        else if (explosion.ExplodeDistance > 7 && explosion.ExplodeDistance <= 10)
        {
            DoDamage(damage * 0.5f);
        }
    }
    public void DoDamage(float damage)
    {

        CurrentHealth = CurrentHealth - damage;





    }
}
