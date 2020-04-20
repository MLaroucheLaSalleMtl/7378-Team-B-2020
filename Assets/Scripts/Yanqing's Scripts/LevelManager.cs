using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;
    private static int Current_Level;
    public GameObject Menu_Win;
    public GameObject Menu_Lose;

    private void Awake()
    {
        Ins = this;
    }

    void Start()
    {
        Menu_Win = ModuleRoot.Ins.UIModule.WinWnd;
        Menu_Lose = ModuleRoot.Ins.UIModule.LoseWnd;
        //Menu_Win.SetActive(false);
        //Menu_Lose.SetActive(false);
        //Current_Level = 0;
        //SceneManager.LoadScene("TutorialField");
    }

    public void LoadLevel1()
    {
        ModuleRoot.Ins.UIModule.ShowWnd(false,WndType.WND_WIN);
        Current_Level = 1;
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        ModuleRoot.Ins.UIModule.ShowWnd(false,WndType.WND_WIN);
        Current_Level = 2;
        SceneManager.LoadScene("terrain");
    }
    public void LoadLevel3()
    {
        ModuleRoot.Ins.UIModule.ShowWnd(false,WndType.WND_WIN);
        Current_Level = 3;
        SceneManager.LoadScene("LevelFinal");
    }

    public void LevelEndWin(int levelIndex)
    {
        Action act;
        if (levelIndex == 1)
        { 
            ModuleRoot.Ins.UIModule.ShowWnd(true,WndType.WND_WIN,LoadLevel1);
        }
        else if(levelIndex ==2)
        {
            ModuleRoot.Ins.UIModule.ShowWnd(true,WndType.WND_WIN,LoadLevel2);
        }
        else if (levelIndex == 3)
        {
            ModuleRoot.Ins.UIModule.ShowWnd(true, WndType.WND_WIN, LoadLevel3);
        }
        // Menu_Win.SetActive(true);
    }

    public void levelEndLose()
    {
        ModuleRoot.Ins.UIModule.ShowWnd(true,WndType.WND_LOSE,null);
      //  Menu_Lose.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
