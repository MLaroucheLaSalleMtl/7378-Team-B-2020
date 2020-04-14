using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private Transform player;
    private GameManagerLelvel2 GameManagerLelvel2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameManagerLelvel2 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameManagerLelvel2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            GameManagerLelvel2.Retreat = true;

        }
    }
}
