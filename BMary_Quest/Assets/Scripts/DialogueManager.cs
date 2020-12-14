using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public int index = 0;
    public Text currentEnemyDialogue;
    public string [] dialogueEnemyList;


private void Start()
    {
        dialogueEnemyList = new string[] { "Hurry up", "Hi there", "Is this working?"};
    }

    void Update()
    {
      //  currentEnemyDialogue.text = dialogueEnemyList[index];
    }
}
