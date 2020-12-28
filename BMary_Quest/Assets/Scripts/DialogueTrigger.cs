using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Text MaryText;
    public Text EnemyText;


    //greetings
    private string[][] EnemyGreetings =
    {
        new string[] {"And now we finally meet, Bloody Mary…!","Sorry, just came out of the brimstone showers.","Have you been a bad girl this year?","There’s no need for this, dear pet. We can rule the world together!","What’s all this I hear about some witch wanting to steal my staff?",""},
        new string[] {"Oh, Hi there Mary","Don’t go too hard…","Can we talk this out like adults?","Here we go again…","Want to take a drink after this, Mary?",""},
        new string[] {"Hola, Amiga!","Saludos, Mirror lady!","I’ll show you flower power, chica!","Feel spooked yet?","Want a flower, girl?",""},
        new string[] {"*Pant pant*","*Head tilt*","*Bark bark!*",""},
        new string[] {"Well well well… Bloody Mary.","We meet at last, Necromancer.","This won’t take long, my dear.","You got a very pretty nec...name.","A toast to you, my lady!","" }
    };


    //ok, vi har, greetings/staff power/Cashout/Taking damage/ victory / loss / stalemate

    public void MaryGreeting()
    {
        if(DataAcrossScenes.EnemyChosenStaff ==(int)Chosen_Staff.pumpkin)
            MaryText.text = "Ghastella hello, is this thing on";
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
            MaryText.text = "Senor boned I presume";
    }
    public void MaryStaffPower()
    {
        MaryText.text = "Reflectga!!!";
    }
    public void MaryCashOut()
    {
        MaryText.text = "This is going to hurt, for you";
    }
    public void MaryTakingDamage()
    {
        MaryText.text = "You DARE?!";
    }
    public void MaryWin()
    {
        MaryText.text = "This is just a stepping stone";
    }
    public void MaryLoss()
    {
        MaryText.text = "I'll see you, in hell...again";
    }
    public void MaryStalemate()
    {
        MaryText.text = "Using a mirror power on me...how qaint";
    }

    public void EnemyGreeting()
    {
        Debug.Log("We get this far");
        int i = DataAcrossScenes.EnemyChosenStaff;
        Debug.Log("int i= " + i);
        int j = Random.Range(0, EnemyGreetings[i].GetUpperBound(0)); //glöm inte att lägga till en tom string då random range är icke-inklusiv på upper range!
        Debug.Log("int i= "+i + "int j= " +j);
        EnemyText.text = EnemyGreetings[i][j];
    }

}
