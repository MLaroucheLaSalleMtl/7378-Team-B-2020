using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    
    [TextArea(2,5)]
    public string[] dialogues;
    public DialogueManager dialogueManager;
    public float timer = 0;
    public float waittime = 3;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.StartSentence(dialogues);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waittime)
        {
            dialogueManager.DialogueDisplay();

        }
        if (timer > waittime)
        {
            timer = 0f;
        }




    }
    

}
