using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class triggerButtonWithKey : MonoBehaviour
{
    public KeyCode key;
    public KeyCode key1;

    public Button button { get; private set; }

    Graphic targetGraphic;
    Color normalColor;

    void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        targetGraphic = GetComponent<Graphic>();

        ColorBlock cb = button.colors;
        cb.disabledColor = cb.normalColor;
        button.colors = cb;
    }

    void Start()
    {
        button.targetGraphic = null;
        Up();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) || Input.GetKeyDown(key1))
        {
            Down();
        }
        else if (Input.GetKeyUp(key) || Input.GetKeyUp(key1))
        {
            Up();
        }
    }

    void Up()
    {
        StartColorTween(button.colors.normalColor, false);
    }

    void Down()
    {
        StartColorTween(button.colors.pressedColor, false);
        button.onClick.Invoke();
    }

    void StartColorTween(Color targetColor, bool instant)
    {
        if (targetGraphic == null)
            return;

        targetGraphic.CrossFadeColor(targetColor, instant ? 0f : button.colors.fadeDuration, true, true);
    }

    void OnApplicationFocus(bool focus)
    {
        Up();
    }

    public void LogOnClick()
    {
        Debug.Log("LogOnClick() - " + GetComponentInChildren<Text>().text);
    }
}