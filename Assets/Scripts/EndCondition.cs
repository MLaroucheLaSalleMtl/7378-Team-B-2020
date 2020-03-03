using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    public float enemynumber;
    // Start is called before the first frame update
    void Start()
    {
        enemynumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(enemynumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void numberupdate()
    {
        enemynumber--;
        if(enemynumber<=0)
        {
            SceneManager.LoadScene("GameMenu");
        }
    }
}
