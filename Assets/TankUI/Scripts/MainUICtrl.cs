using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WndType
{
    WND_OPTION,
    WND_WIN,
    WND_LOSE,
    WND_OVER
}
public class MainUICtrl : MonoBehaviour
{
    
    public GameObject OptionWnd;
    public GameObject WinWnd;
    public GameObject LoseWnd;
    public GameObject GameOverWnd;
    public Action WinWndOnNextLevelClickCallBack;
    public Text TexScore;
    

    public void UpdateScoreText(string text)
    {
        TexScore.text = text;
    }
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
                case WndType.WND_OVER:
                    GameOverWnd.SetActive(true);
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
                case WndType.WND_OVER:
                    GameOverWnd.SetActive(false);
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
        GameOverWnd.SetActive(false);
        ShowWnd(false, WndType.WND_WIN);
        SceneManager.LoadScene(2);
    }

    public void OnLoseWndGameMenuClick()
    {
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        GameOverWnd.SetActive(false);
        ShowWnd(false, WndType.WND_LOSE);
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        ModuleRoot.Ins.ModuleData.SaveScore();
        ModuleRoot.Ins.ModuleData.ResetScore();
        ShowScoreText(false);
        ShowWnd(false);
        WinWnd.SetActive(false);
        LoseWnd.SetActive(false);
        OptionWnd.SetActive(false);
        GameOverWnd.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void ShowScoreText(bool flag)
    {
        TexScore.transform.parent.gameObject.SetActive(flag);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (WinWnd.gameObject.activeInHierarchy || LoseWnd.gameObject.activeInHierarchy|| GameOverWnd.gameObject.activeInHierarchy) return;
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