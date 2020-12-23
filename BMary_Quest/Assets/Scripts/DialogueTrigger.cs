using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Text MaryText;
    public Text EnemyText;
   // public MaryDialogue dialogue;

   // public void TriggerDialogue()
  //  {
       // FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  //  }
    //ok, vi har, greetings/staff power/Cashout/Taking damage/ victory / loss / stalemate


    public void MaryGreeting()
    {
        MaryText.text = "hello, is this thing on";
    }
    public void MaryStaffPower()
    {
        MaryText.text = "Reflectga!!!";
    }
    public void MaryCashOut()
    {
        MaryText.text = "This is going to hurt, for you";
    }
}
