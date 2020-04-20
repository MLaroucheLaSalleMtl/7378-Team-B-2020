using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndCondition2 : MonoBehaviour
{
    public Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Slider.value>=100)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelEndWin(3);
        }
    }
}
