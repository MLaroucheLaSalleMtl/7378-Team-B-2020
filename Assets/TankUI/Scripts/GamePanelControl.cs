using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EscPanel;
    public GameObject PausePanel;

    private bool escFlag = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ToggleGamePausPanel();
        }
    }

    public void ContinueBtnClick()
    {
        ToggleGamePausPanel();
    }

    private void ToggleGamePausPanel()
    {
        escFlag = !escFlag;
        PausePanel.SetActive(escFlag);
        EscPanel.SetActive(escFlag);
        if (escFlag)
        {
            Cursor.lockState = CursorLockMode.None;
        } 
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void HomeBtnClick()
    {
        SceneManager.LoadScene("GameMenu"); 
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
}
