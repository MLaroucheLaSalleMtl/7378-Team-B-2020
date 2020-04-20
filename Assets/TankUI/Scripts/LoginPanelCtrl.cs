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
    public GameObject tankSwitcher;
    public GameObject bgVideo;
    public Text[] nameTextArr;
    public Text[] scoreTextArr;

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
        /*if (userIdText.text == "123" && passwdText.text == "123")
        {
            menuPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
        else
        {
            passwdInput.GetComponent<InputField>().text = "";
            passwdText.text = "";
            passwdHolder.text = "wrong passwd!";
            passwdHolder.fontStyle = FontStyle.Bold;
            passwdHolder.color = Color.red;
        }*/
        ModuleRoot.Ins.ModuleData.OnGameStart(userIdText.text);
        tankSwitcher.GetComponent<Menu_Switch>().Spawn_Light();
        menuPanel.SetActive(true);
        loginPanel.SetActive(false);
        bgVideo.SetActive(false);
    }

    public void UpdateRankView()
    {
     List<PlayerData> lst=   ModuleRoot.Ins.ModuleData.GetScoreLst();
     for (int i = 0; i <7; i++)
     {
         nameTextArr[i].text = "";
         scoreTextArr[i].text = "";
     }
     for (int i = 0; i < lst.Count; i++)
     {
        nameTextArr[i].text= lst[i].Name;
        scoreTextArr[i].text = lst[i].Score.ToString();
     }

     
    }
}
