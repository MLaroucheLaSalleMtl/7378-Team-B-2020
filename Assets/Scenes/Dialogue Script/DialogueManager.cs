using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    
    public Queue<string> sentences;
    
    public Text DialogueText;
    public Animator DialogueAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        
        sentences = new Queue<string>();
    }

    public void StartSentence(string[] dialogues)
    {
        DialogueAnimator.SetBool("StartDialogue", true);
        sentences.Clear();
        foreach(string sentence in dialogues)
        {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }


        DialogueDisplay();

    }
    
    public void DialogueDisplay()
    {
        if(sentences.Count==0)
        {
            EndDialogue();

        }
        else
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            //DialogueText.text = sentence;
            StartCoroutine(LetterByLetter(sentence));
        }
    }

    IEnumerator LetterByLetter(string sentence)
    {
        DialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
        
    }
    public void EndDialogue()
    {
        
        DialogueAnimator.SetBool("StartDialogue", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
