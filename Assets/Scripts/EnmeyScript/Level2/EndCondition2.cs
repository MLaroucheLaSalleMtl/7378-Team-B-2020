using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndCondition2 : MonoBehaviour
{
    public Slider Slider;

    private bool isEnd=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Slider.value>=100)
        {
            if (!isEnd)
            {
                isEnd = true;
                //SceneManager.LoadScene("GameMenu");
                GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelEndWin(3);
            }
        }
    }
}
