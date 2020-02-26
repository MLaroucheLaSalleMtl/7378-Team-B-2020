using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public GameObject shellExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onTriggerEvent(Collider collider) {
        GameObject.Instantiate(shellExplosionPrefab, transform.position, transform.rotation);
    }
}
