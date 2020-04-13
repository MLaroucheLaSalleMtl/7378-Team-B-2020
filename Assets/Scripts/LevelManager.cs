using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int Current_Level;
    public GameObject Menu_Win;
    public GameObject Menu_Lose;
    // Start is called before the first frame update
    void Start()
    {
        Menu_Win.SetActive(false);
        Menu_Lose.SetActive(false);
        Current_Level = 0;
        SceneManager.LoadScene("TutorialField");
    }

    public void LoadLevel1()
    {
        Menu_Win.SetActive(false);
        Menu_Lose.SetActive(false);
        Current_Level = 1;
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        Menu_Win.SetActive(false);
        Menu_Lose.SetActive(false);
        Current_Level = 2;
        SceneManager.LoadScene("LevelFinal");
    }

    public void LevelEndWin()
    {
        Menu_Win.SetActive(true);
    }

    public void levelEndLose()
    {
        Menu_Lose.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
