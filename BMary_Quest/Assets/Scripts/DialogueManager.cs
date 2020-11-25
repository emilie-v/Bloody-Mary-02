using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public Text characterName;
    public Text characterLine;

    int dialogueLine;

    bool started;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!started)
        {

            characterName.text = dialogue.name;
            dialogueLine = 0;
            started = true;
        }
        if (dialogue.sentences.Length > dialogueLine)
        {
            characterLine.text = dialogue.sentences[dialogueLine];
        }
        else
        {
            started = false;

            characterName.text = "";
            characterLine.text = "";
        }

       /* Debug.Log("Starting conversation with " + dialogue.name);*/



        dialogueLine++;
    }


}
