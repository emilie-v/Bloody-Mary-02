using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public int maryIndex = 0;
    public int enemyIndex = 0;

    public Text currentMaryDialogue;
    public Text currentEnemyDialogue;

    public string[] dialogueMaryList;
    public string[] dialogueLuciferList;

    string emptyDialgoue = "";

    string luciferWelcome = "- How dare you enter my dungeon! I will burn your soul!";
    string luciferCashOut01 = "- I will destroy you!";
    string luciferCashOut02 = "- I love the sound of pain.";
    string luciferDamaged01 = "- Your foolish girl!";
    string luciferDamaged02 = "- Your power is strong, but not strong enough!";
    string luciferLose = "- I cannot believe this...";
    string luciferWin = "- Well tried, No one will ever defeat me!";
    string luciferEveryoneLose = "- You are too weak!";

    string maryWelcome = "- I'd like to see you try!";
    string maryCashOut01 = "- Haha - You make me laugh";
    string maryCashOut02 = "- Watch this!";
    string maryDamaged01 = "- That was unnecessarily mean...";
    string maryDamaged02 = "- You will pay for this!";
    string maryLose = "- I will finish you next time we meet again!";
    string maryWin = "- Haha! Too easy!";
    string maryEveryoneLose = "- I know you are cheating...";

    public void Start()
    {
        dialogueLuciferList = new string[] { emptyDialgoue, luciferWelcome, luciferCashOut01, luciferCashOut02, luciferDamaged01, luciferDamaged02, luciferLose, luciferWin, luciferEveryoneLose };
        dialogueMaryList = new string[] { emptyDialgoue, maryWelcome, maryCashOut01, maryCashOut02, maryDamaged01, maryDamaged02, maryLose, maryWin, maryEveryoneLose };

        enemyIndex = 1;
        maryIndex = 1;

        currentEnemyDialogue.text = dialogueLuciferList[enemyIndex];
        currentMaryDialogue.text = dialogueMaryList[enemyIndex];
    }
}
