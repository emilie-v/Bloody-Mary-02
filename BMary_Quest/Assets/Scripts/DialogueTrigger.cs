﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Text MaryText;
    public Text EnemyText;


    private string[][] MarysGreetings =
    {
        new string[] {"There you are! Where have you been hiding, Luci?","Hey, I’ve been looking for you!","Don’t be scared, Luci. This will be over with quickly!","Jesus, would it kill you to put a shirt on?","Hit the bricks, Luci. There’s a new Princess of darkness in town!",""},
        new string[] {"Hey, give me back my staff!","Long time no see, Ghastella","Oh boy… you…","No, you can’t borrow any money","Oh no… Ghastella. This will be awkward",""},
        new string[] {"Sorry, I don’t speak foreign.","Move along, flower boy!","I’m not that into skinny guys…","Señor Boned,I presume","You need a hamburger or something, dude.","Yo, bone head.","Gone a bit far with the vegan thing there?",""},
        new string[] {"Naww, it’s a puppy!","What’s that? Little Billy stuck in the well? Show me!","Nice doggy…!","Hey, when I throw you a staff, you’re supposed to retrieve it, not use it against me!","If you let me win I’ll give you a treat!",""},
        new string[] {"Sorry about my breath. Just had some garlic!","Thought about becoming a vegan, Drac? Steak’s bad for your heart!","Are you supposed to be some kind of swinger or something?","Are those fangs real or are they just dentures, old man?","Mirrors and vampires don’t mix, sweety.",""}
    };
    //lots of jagged arrays, order of enemies, 0:Luci, 1:Ghastella, 2:Senor B 3:Umbra 4: Count not-dracula  
    private string[][] EnemyGreetings =
    {
        new string[] {"And now we finally meet, Bloody Mary…!","Sorry, just came out of the brimstone showers.","Have you been a bad girl this year?","There’s no need for this, dear pet. We can rule the world together!","What’s all this I hear about some witch wanting to steal my staff?",""},
        new string[] {"Oh, Hi there Mary","Don’t go too hard…","Can we talk this out like adults?","Here we go again…","Want to take a drink after this, Mary?",""},
        new string[] {"Hola, Amiga!","Saludos, Mirror lady!","I’ll show you flower power, chica!","Feel spooked yet?","Want a flower, girl?",""},
        new string[] {"*Pant pant*","*Head tilt*","*Bark bark!*",""},
        new string[] {"Well well well… Bloody Mary.","We meet at last, Necromancer.","This won’t take long, my dear.","You got a very pretty nec...name.","A toast to you, my lady!","" }
    };

    private string[][] EnemyUsingStaffs =
    {
        new string[] {"Brimstone!","I mark you, mirror wench!","Be careful now…","Power!","Let’s make this interesting…",""},
        new string[] {"Freeze!","I didn’t want to do this…","I think I broke your staff…","Oops…!","I don’t think your staff is working correctly.",""},
        new string[] {"Feliz día de los Muertos!","Tiempo De morir!","Poder de la flor!","Grow and bloom, Mis hijos!","They always underestimate the power of flowers!",""},
        new string[] {"*Howls at the moon*","*Howls*","*Happy japing*","*Growl*","*Bark bark!*",""},
        new string[] {"This one is off limits!","Darkness consumes this one!","Sowilo!","Decay and stagnation!","Break and stagnate!",""}
    };

    private string[][] EnemyAttacking =
    {
        new string[] {"Pain is pleasure, my sweet.","You’ve already lost, you just don’t know it yet…","No dishonor in giving up, girl.","What’s wrong, Mary? Don’t you want to live forever?","I can make the pain go away, Mary…",""},
        new string[] {"Hope you won’t get too hurt","Oh, SORRY!","That must have hurt. I’m terribly sorry!","Heavens, Mary! You’re bleeding!","Man, I wish I could bleed…",""},
        new string[] {"Taste the power of flowers, tonto!","Don’t you know flesh is weak?","I love the smell of flowers in the morning!","If I kill you now, want to hang out later?","You don’t need that pesky flesh anyways.",""},
        new string[] {"*Growl*","*Howl*","*Pounce*","*Bark bark!*",""},
        new string[] {"Let me have a taste.","Mhm! Fresh blood.","Could you gather any spilt blood in a cup, please?","Your blood looks delicious!","*Sigh* I remember when I used to bleed.",""}
    };

    private string[][] EnemyHurt =
    {
        new string[] {"Sweet pain!","Do you think you’re hurting me?","Stop this foolishness, girl and join me.","Puny necromancer…","Fun fact; my blood is flammable.Do you want to know the secret to this power?",""},
        new string[] {"Wait… I’m bleeding?","Ouch!","Yikes!","Look what you did! You got ectoplasm all over the floor!","I didn’t sign up to being a ghost, just to bleed out!",""},
        new string[] {"Ay!","I’m going to need two shots of milk for that one.","Bruto!","Hey! Who said you could touch my bones?","You don’t like flowers? What kind of loco broad are you?",""},
        new string[] {"*whimper*","*Bark bark bark!*","*woff!*","*Yelp!*",""},
        new string[] {"Argh!","Don’t spill the little blood I have left, you stupid little girl!","How dare you!","Ouch!","I’ll get you for that one, peasant!",""}
    };

    private string[][] EnemyLoss =
    {
        new string[] {"This… is… impossible!","Mary, think this through!","What have you done?!","I will have my revenge…","My staff! My power! NO!",""},
        new string[] {"Mary!!!","Okey, okey! You can have the staff back!","Wait… where am I going now?","Did I just… die again?","I should have stayed in bed…",""},
        new string[] {"No! Don’t step on the flowers, tonto!","I’m going to need lots and lots of milk to repair this damage.","Stop kicking me, I’m a skeleton! I’m already dead!","This went a lot smoother in my head…","Estupendo…",""},
        new string[] {"*Sad howl*","*Whimper…*","*Sniff-…*","*Tucks tail between legs*",""},
        new string[] {"Wha-what happened?","Oh… no…","Back to the grave it is…","I’ll be back…","I am a son of darkness! You can’t do this to me!",""}
    };

    private string[][] EnemyWin =
    {
        new string[] {"Who would have suspected this outcome, Muahahaha!","I’ll bring you back and destroy you again, Mary!","Now back in line, filthy sinner!","As punishment, you will be my personal plaything for the next few eternities.","Ridiculous display, my sweet daughter. You cannot best me.",""},
        new string[] {"Oh hey, I won!","Holy smokes! I didn’t mean to hurt you that much!","You owe me a drink","W-want to try again?","This was… unexpected",""},
        new string[] {"And just like that, the great Bloody Mary has now become fertilizer for Senior Bones’ flower garden!","Bested! Oh yeah, did you want to go out for lunch or something?","Don’t mess with me or my flowers again, chica!","Would it be weird of me to ask for a date now?","Don’t feel bad, Mary. You only got totally owned by the power of flowers, tonto!",""},
        new string[] {"*Howl!*","*Chases tail*","*Nap time*",""},
        new string[] {"Unsurprising…","You’re nothing but a clueless little girl, my dear.","And now for a taste test!","Lord, I’m thirsty!","Ah, the crimson water will flow like wine!",""}
    };




    //ok, vi har, greetings/staff power/Cashout/Taking damage/ victory / loss / stalemate

    public void MaryGreeting()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MarysGreetings[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = MarysGreetings[i][j];
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
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyGreetings[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyGreetings[i][j];
    }

    public void EnemyUsingStaff()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyUsingStaffs[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyUsingStaffs[i][j];
    }

    public void EnemyAttack()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyAttacking[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyAttacking[i][j];
    }

    public void EnemyHurts()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyHurt[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyHurt[i][j];
    }

    public void EnemyLoses()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyLoss[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyLoss[i][j];
    }

    public void EnemyWins()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyWin[i].GetUpperBound(0)); //glöm inte att lägga till en tom string på arrayen då random range är icke-inklusiv på upper range när det är ints! 
        EnemyText.text = EnemyWin[i][j];
    }

}
