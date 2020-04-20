using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    private float enemynumber;
    // Start is called before the first frame update
    void Start()
    {
        enemynumber = 15;// = GameObject.FindGameObjectsWithTag("LT").Length + GameObject.FindGameObjectsWithTag("MT").Length + GameObject.FindGameObjectsWithTag("HT").Length;

    }

    // Update is called once per frame
    void Update()
    {
        //enemynumber = GameObject.FindGameObjectsWithTag("LT").Length + GameObject.FindGameObjectsWithTag("MT").Length + GameObject.FindGameObjectsWithTag("HT").Length;
        Debug.Log(enemynumber);
    }
    public void numberupdate()
    {
        enemynumber--;
        if(enemynumber<=0)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelEndWin(2);
        }
    }
}
