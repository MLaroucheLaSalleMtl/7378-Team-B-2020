using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class movetest2 : MonoBehaviour
{
    public Transform player;
    NavMeshAgent enemy;
    private NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<NavMeshAgent>();
         path = new NavMeshPath();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.CalculatePath(player.transform.position, path))
        {
            Debug.Log("yes");
        }
    }
}
