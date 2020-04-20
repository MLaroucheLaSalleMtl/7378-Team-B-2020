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
    public void OnWinWndNextLevelClick()
    {
        WinWnd.SetActive(false);
        if (WinWndOnNextLevelClickCallBack != null)
            WinWndOnNextLevelClickCallBack();
    }

    public void OnWinWndGameMenuClick()
    {
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        ShowWnd(false, WndType.WND_WIN);
        SceneManager.LoadScene(2);
    }

    public void OnLoseWndGameMenuClick()
    {
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        ShowWnd(false, WndType.WND_LOSE);
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        ShowWnd(false);
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (WinWnd.gameObject.activeInHierarchy || LoseWnd.gameObject.activeInHierarchy) return;
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