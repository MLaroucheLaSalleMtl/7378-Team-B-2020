using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUICtrl : MonoBehaviour
{
    public GameObject OptionWnd;

    public void ShowWnd(bool flag)
    {
        if (flag)
        {
            Time.timeScale = 0f;
            OptionWnd.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            OptionWnd.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        ShowWnd(false);
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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