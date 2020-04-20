using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public GameObject LoadPanel;
    private AsyncOperation asyncOp;
    private float loadPct = 0;
    public GameObject LoadProgress;

    // Start is called before the first frame update
    void Start()
    {
       // this.GetComponent<Button>().onClick.AddListener(ShowOptionPanel);
        //Debug.Log(GameObject.Find("MenuOptionsButton").name);
    }

    // Update is called once per frame
    void Update()
     {
        if (asyncOp != null)
        {
            loadPct = asyncOp.progress;
            if (loadPct >= 0.9f)
            {
                loadPct = 1;
            }
            LoadProgress.GetComponent<bl_ProgressBar>().Value = loadPct*100;
        }
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

    public void ShowRankPanel()
    {
        GameObject.Find("HomeCanvas").transform.Find("MenuPanel").gameObject.SetActive(false);
        GameObject.Find("HomeCanvas").transform.Find("RankPanel").gameObject.SetActive(true);
    }

    public void HideRankPanel()
    {
        GameObject.Find("HomeCanvas").transform.Find("RankPanel").gameObject.SetActive(false);
        GameObject.Find("HomeCanvas").transform.Find("MenuPanel").gameObject.SetActive(true);
    }

    public void MenuExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
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
    
    public void LoadingGameScence()
    {
        print("clicked");
        LoadPanel.SetActive(true);
        if(!Instruction.Completed)
        {
                SceneManager.LoadScene("TutorialField");
        }
        else
        {
            switch (MainUICtrl.Current_Level)
            {
                case 1:
                    StartCoroutine(LoadingGameScenceAsync("Level1"));
                    break;
                case 2:
                    StartCoroutine(LoadingGameScenceAsync("terrain"));
                    break;
                case 3:
                    StartCoroutine(LoadingGameScenceAsync("LevelFinal"));
                    break;
            }
        }

    }

    public void LoadTutorial()
    {
        SceneManager.LoadSceneAsync("TutorialField");
    }

    IEnumerator LoadingGameScenceAsync(string gameScenceNam)
    {
        asyncOp = SceneManager.LoadSceneAsync(gameScenceNam);
        loadPct = asyncOp.progress;
        yield return loadPct;
    }
}
