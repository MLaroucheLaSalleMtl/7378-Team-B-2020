using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // this.GetComponent<Button>().onClick.AddListener(ShowOptionPanel);
        //Debug.Log(GameObject.Find("MenuOptionsButton").name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptionPanel()
    {
        GameObject.Find("HomeCanvas").transform.Find("ShootPanel").gameObject.SetActive(false);
        GameObject.Find("HomeCanvas").transform.Find("OptionPanel").gameObject.SetActive(true);
        ShowVideoOptionsPanel();
    }

    public void HideOptionPanel()
    {
        GameObject.Find("HomeCanvas").transform.Find("OptionPanel").gameObject.SetActive(false);
    }

    public void MenuExitGame()
    {
        Application.Quit();
    }

    public void ShowVideoOptionsPanel() {
        HideAllOptionsPanel();
        GameObject.Find("OptionPanel/BodyPanel").transform.Find("VideoOptionsPanel").gameObject.SetActive(true);
    }

    public void ShowAudioOptionsPanel()
    {
        HideAllOptionsPanel();
        GameObject.Find("OptionPanel/BodyPanel").transform.Find("AudioOptionsPanel").gameObject.SetActive(true);
    }

    public void ShowControlOptionsPanel()
    {
        HideAllOptionsPanel();
        GameObject.Find("OptionPanel/BodyPanel").transform.Find("ControlOptionsPanel").gameObject.SetActive(true);
    }

    public void ShowGamePlayOptionsPanel()
    {
        HideAllOptionsPanel();
        GameObject.Find("OptionPanel/BodyPanel").transform.Find("GamePlayOptionsPanel").gameObject.SetActive(true);
    }

    private void HideAllOptionsPanel()
    {
        GameObject optionBodyPanel = GameObject.Find("OptionPanel/BodyPanel");
        optionBodyPanel.transform.Find("VideoOptionsPanel").gameObject.SetActive(false);
        optionBodyPanel.transform.Find("AudioOptionsPanel").gameObject.SetActive(false);
        optionBodyPanel.transform.Find("ControlOptionsPanel").gameObject.SetActive(false);
        optionBodyPanel.transform.Find("GamePlayOptionsPanel").gameObject.SetActive(false);
    }
    
    private bool shootPanelFlg = false;
    public void ShowWPanel()
    {
        if (!shootPanelFlg)
        {
            GameObject.Find("HomeCanvas").transform.Find("ShootPanel").gameObject.SetActive(true);
            shootPanelFlg = true;
        }
        else
        {
            GameObject.Find("HomeCanvas").transform.Find("ShootPanel").gameObject.SetActive(false);
            shootPanelFlg = false;
        }
    }
}
