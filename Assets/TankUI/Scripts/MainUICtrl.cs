using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WndType
{
    WND_OPTION,
    WND_WIN,
    WND_LOSE
}
public class MainUICtrl : MonoBehaviour
{
    
    public GameObject OptionWnd;
    public GameObject WinWnd;
    public GameObject LoseWnd;
    public Action WinWndOnNextLevelClickCallBack;
    public static int Current_Level;
    // public Action WinWndOnNextToGameMenuClickCallBack;
    // public Action LoseWndOnGameMenuClickCallBack;
    public void ShowWnd(bool flag,WndType type=WndType.WND_OPTION,Action nextLevelCallBack=null)
    {
        if (flag)
        {
            if (nextLevelCallBack != null) WinWndOnNextLevelClickCallBack = nextLevelCallBack;
            Time.timeScale = 0f;
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                TankCamera.Ins.UnLockCursor();
            }

            switch (type)
            {
                case WndType.WND_OPTION:
                    OptionWnd.SetActive(true);
                    break;
                case WndType.WND_WIN:
                    WinWnd.SetActive(true);
                    break;
                case WndType.WND_LOSE:
                    LoseWnd.SetActive(true);
                    break;
            }
        }
        else
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                TankCamera.Ins.LockCursor();
            }
            Time.timeScale = 1f;
            switch (type)
            {
                case WndType.WND_OPTION:
                    OptionWnd.SetActive(false);
                    break;
                case WndType.WND_WIN:
                    WinWnd.SetActive(false);
                    break;
                case WndType.WND_LOSE:
                    LoseWnd.SetActive(false);
                    break;
            }
        }
        
        
       
    }

    public void OnOptionWndResumeClick()
    {
        Time.timeScale = 1f;
        OptionWnd.SetActive(false);
        TankCamera.Ins.LockCursor();
    }
    public void LoadLevel1()
    {
        Current_Level = 1;
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        Current_Level = 2;
        SceneManager.LoadScene("terrain");
    }
    public void LoadLevel3()
    {
        Current_Level = 3;
        SceneManager.LoadScene("LevelFinal");
    }
    public void OnWinWndNextLevelClick()
    {
        Time.timeScale = 1f;
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        if (Current_Level == 1)
        {
            Current_Level += 1;
            SceneManager.LoadScene("terrain");
        }
        else if (Current_Level == 2)
        {
            Current_Level += 1;
            SceneManager.LoadScene("LevelFinal");
        }
        //if (WinWndOnNextLevelClickCallBack != null)
        //    WinWndOnNextLevelClickCallBack();
    }

    public void OnWinWndGameMenuClick()
    {
        Time.timeScale = 1f;
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        ShowWnd(false, WndType.WND_WIN);
        SceneManager.LoadScene(2);
    }

    public void OnLoseWndGameMenuClick()
    {
        Time.timeScale = 1f;
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        ShowWnd(false, WndType.WND_LOSE);
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        ShowWnd(false);
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        print(Current_Level);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (TankCamera.Ins != null)
            {
                if (OptionWnd.gameObject.activeInHierarchy)
                    TankCamera.Ins.LockCursor();
                else
                    TankCamera.Ins.UnLockCursor();
            }
            ShowWnd(!OptionWnd.gameObject.activeInHierarchy);
        }
    }
}