using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    private readonly float maxhealth = 1000f;
    private float CurrentHealth;
    [Header("Death")]
    public bool IsDead = false;
    public bool OnlyOnce = false;
    [Header("Display Hp")]
    public Text PlayerHpText;
    

    public 
    // Start is called before the first frame update
    void Start()
    {
        PlayerHpText = GameObject.FindGameObjectWithTag("HP").GetComponent<Text>();
        CurrentHealth = maxhealth;
        PlayerHpText.text = CurrentHealth.ToString();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        PlayerHpText.text = ((int)CurrentHealth).ToString();
        if (CurrentHealth <= 0)
        {
            Debug.Log("dead");
        }
    }
    public void DoDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;

    }
    //public void Death()
    //{
    //    IsDead = true;
        
    //    Debug.Log("dead");
    //    if (!OnlyOnce)
    //    {
    //        EndCondition.numberupdate();
    //        OnlyOnce = true;
    //    }

    //}
}
