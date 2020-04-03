using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelCtrl : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject loginPanel;
    public Text userIdText;
    public Text passwdText;
    public GameObject passwdInput;
    public Text passwdHolder;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitBtnOnclick() {
        Application.Quit();
    }

    public void LoginBtnOnClick() {
        menuPanel.SetActive(true);
        loginPanel.SetActive(false);
        GameSession.setUserId(userIdText.text);
    }
}
