using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUIPool : MonoBehaviour
{
    public float LifeTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (LifeTime > 0)
        {
            StartCoroutine(ObjectDestroy(LifeTime));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ObjectDestroy(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
		this.gameObject.SetActive(false);
        GameObject.Destroy(this.gameObject, 5.0f);
	}
}
