using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public float Delay;
    public Text MaryText;
    public Text EnemyText;
    private string MarysCurrentText;
    private string EnemysCurrentText;

    public CharacterAnimations characterAnimations;
    public EnemyAnimations enemyAnimations;

    public void Awake()
    {
        characterAnimations = GameObject.Find("PlayerMary").GetComponent<CharacterAnimations>();
        enemyAnimations = GameObject.Find("PlayerEnemy").GetComponent<EnemyAnimations>();
    }

    private string[][] MarysGreetings =
    {
        new string[] {"This line is as empty as my soul."},
        new string[] {"Hey, give me back my staff!","Long time no see, Ghastella","Oh boy… you…","No, you can’t borrow any money","Oh no… Ghastella. This will be awkward",""},
        new string[] {"Sorry, I don’t speak foreign.","Move along, flower boy!","I’m not that into skinny guys…","Senor Boned,I presume","You need a hamburger or something, dude.","Yo, bone head.","Gone a bit far with the vegan thing there?",""},
        new string[] {"Naww, it’s a puppy!","What’s that? Little Billy stuck in the well? Show me!","Nice doggy…!","Hey, when I throw you a staff, you’re supposed to retrieve it, not use it against me!","If you let me win I’ll give you a treat!",""},
        new string[] {"Sorry about my breath. Just had some garlic!","Thought about becoming a vegan, Drac? Steak’s bad for your heart!","Are you supposed to be some kind of swinger or something?","Are those fangs real or are they just dentures, old man?","Mirrors and vampires don’t mix, sweety.",""},
        new string[] {"There you are! Where have you been hiding, Luci?","Hey, I’ve been looking for you!","Don’t be scared, Luci. This will be over with quickly!","Jesus, would it kill you to put a shirt on?","Hit the bricks, Luci. There’s a new Princess of darkness in town!",""}
    };

    private string[][] MaryUsingStaffs =
    {
        new string[] {"Blood Magic!","Reflectga","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""},
        new string[] {"Time Magic!","I wonder what this will do…!","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""}, //Generic staff usage atm, but if we want we can change it to reflect *haha* what staff she's using.
        new string[] {"Blood Magic!","I wonder what this will do…!","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""},
        new string[] {"Blood Magic!","I wonder what this will do…!","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""},
        new string[] {"Blood Magic!","I wonder what this will do…!","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""},
        new string[] {"Blood Magic!","I wonder what this will do…!","I’m the most powerful witch alive!","Hope this old thing still works","Oops, did I do that?",""}
    };

    private string[][] MaryAttacks =
    {
        new string[] {"This shouldn't be seen"},
        new string[] {"Out of my way!","Let the ectoplasm flow!","Doesn’t look like you’re going to last long!","You feeling light headed?  You look a little pale, like a sheet!",""}, //Generic attack message atm, but if we want we can change it to match the opponent here.
        new string[] {"Out of my way!","You'll be my source for bone dust","Doesn’t look like you’re going to last long!","You feeling light headed? You look a little pale!",""},
        new string[] {"Out of my way!","Let the blood flow!","I feel kind of bad about this","Doesn’t look like you’re going to last long!","You feeling light headed? You look a little pale!",""},
        new string[] {"Out of my way!","Let the blood flow, from you!","Mhm! Sweet crimson!","Doesn’t look like you’re going to last long!","You feeling light headed? You look a little pale!",""},
        new string[] {"Out of my way!","Let the blood flow!","Mhm! Sweet crimson!","Doesn’t look like you’re going to last long!","You feeling light headed? You look a little pale!",""}
    };

    private string[][] MaryHurts =
    {
        new string[] {"This shouldn't be seen"},
        new string[] {"Agh!", "YOU DARE!?", "You’re spilling all my blood, you nerd!", "Oh no…!", "Okey, I won’t forget that one.", "I’ll get you back, you loser!", ""},  //Generic getting hurt message atm.
        new string[] {"Agh!", "What the hell, man!?", "You’re spilling all my blood, you nerd!", "Oh no…!", "Okey, I won’t forget that one.", "I'll carve your rib bones into flutes", ""},
        new string[] {"Agh!", "Bad dog, BAD DOG", "You’re spilling all my blood, bad pet!", "Oh no…!", "Okey, I won’t forget that one.", "I hope you don't have rabies!", ""},
        new string[] {"Agh!", "What the hell, man!?", "You’re spilling all my blood, you nerd!", "Oh no…!", "Okey, I won’t forget that one.", "I’ll get you back, you loser!", ""},
        new string[] {"Agh!", "What the hell, man!?", "You’re spilling all my blood, you nerd!", "Oh no…!", "Okey, I won’t forget that one.", "I’ll get you back, you loser!", ""}
    };

    private string[][] MaryWins =
    {
        new string[] {"This, was just the beginning","Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""},
        new string[] {"Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""},  //Generic victory message atm.
        new string[] {"Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""},
        new string[] {"Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""},
        new string[] {"Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""},
        new string[] {"Like there was any doubt…!","Would you believe it if I said I knew I was going to win?","Loser!","Now get out of my way. I’m on a quest!","I’ll take that staff, please and thank you", ""}
    };

    private string[][] MaryLoses =
    {
        new string[] {"No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""},
        new string[] {"No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""},  //Generic loss message atm.
        new string[] {"No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""},
        new string[] {"No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""},
        new string[] {"I'll See you in Hell.....again","No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""},
        new string[] {"I'll See you in Hell.....again","No no no!","Okey, fine. You got me.","This doesn’t make you better than me!","I wasn’t even trying.","This game sucks…", "Game over man, Game Over", ""}
    };
    
    private string[][] EnemyGreetings =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Oh, Hi there Mary","Don’t go too hard…","Can we talk this out like adults?","Here we go again…","Want to take a drink after this, Mary?",""},
        new string[] {"Hola, Amiga!","Saludos, Mirror lady!","I’ll show you flower power, chica!","Feel spooked yet?","Want a flower, girl?",""},
        new string[] {"*Pant pant*","*Head tilt*","*Bark bark!*",""},
        new string[] {"Well well well… Bloody Mary.","We meet at last, Necromancer.","This won’t take long, my dear.","You got a very pretty nec...name.","A toast to you, my lady!","" },
        new string[] {"And now we finally meet, Bloody Mary…!","Sorry, just came out of the brimstone showers.","Have you been a bad girl this year?","There’s no need for this, dear pet. We can rule the world together!","What’s all this I hear about some witch wanting to steal my staff?",""}
    };

    private string[][] EnemyUsingStaffs =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Freeze!","I didn’t want to do this…","I think I broke your staff…","Oops…!","I don’t think your staff is working correctly.",""},
        new string[] {"Feliz día de los Muertos!","Tiempo De morir!","Poder de la flor!","Grow and bloom, Mis hijos!","They always underestimate the power of flowers!",""},
        new string[] {"*Howls at the moon*","*Howls*","*Happy japing*","*Growl*","*Bark bark!*",""},
        new string[] {"This one is off limits!","Darkness consumes this one!","Sowilo!","Decay and stagnation!","Break and stagnate!",""},
        new string[] {"Brimstone!","I mark you, mirror wench!","Be careful now…","Power!","Let’s make this interesting…",""},
    };

    private string[][] EnemyAttacking =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Hope you won’t get too hurt","Oh, SORRY!","That must have hurt. I’m terribly sorry!","Heavens, Mary! You’re bleeding!","Man, I wish I could bleed…",""},
        new string[] {"Taste the power of flowers, tonto!","Don’t you know flesh is weak?","I love the smell of flowers in the morning!","If I kill you now, want to hang out later?","You don’t need that pesky flesh anyways.",""},
        new string[] {"*Growl*","*Howl*","*Pounce*","*Bark bark!*",""},
        new string[] {"Let me have a taste.","Mhm! Fresh blood.","Could you gather any spilt blood in a cup, please?","Your blood looks delicious!","*Sigh* I remember when I used to bleed.",""},
        new string[] {"Pain is pleasure, my sweet.","You’ve already lost, you just don’t know it yet…","No dishonor in giving up, girl.","What’s wrong, Mary? Don’t you want to live forever?","I can make the pain go away, Mary…",""}
    };

    private string[][] EnemyHurt =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Wait… I’m bleeding?","Ouch!","Yikes!","Look what you did! You got ectoplasm all over the floor!","I didn’t sign up to being a ghost, just to bleed out!",""},
        new string[] {"Ay!","I’m going to need two shots of milk for that one.","Bruto!","Hey! Who said you could touch my bones?","You don’t like flowers? What kind of loco broad are you?",""},
        new string[] {"*whimper*","*Bark bark bark!*","*woff!*","*Yelp!*",""},
        new string[] {"Argh!","Don’t spill the little blood I have left, you stupid little girl!","How dare you!","Ouch!","I’ll get you for that one, peasant!",""},
        new string[] {"Sweet pain!","Do you think you’re hurting me?","Stop this foolishness, girl and join me.","Puny necromancer…","Fun fact; my blood is flammable.Do you want to know the secret to this power?",""}
    };

    private string[][] EnemyLoss =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Mary!!!","Okey, okey! You can have the staff back!","Wait… where am I going now?","Did I just… die again?","I should have stayed in bed…",""},
        new string[] {"No! Don’t step on the flowers, tonto!","I’m going to need lots and lots of milk to repair this damage.","Stop kicking me, I’m a skeleton! I’m already dead!","This went a lot smoother in my head…","Estupendo…",""},
        new string[] {"*Sad howl*","*Whimper…*","*Sniff-…*","*Tucks tail between legs*",""},
        new string[] {"Wha-what happened?","Oh… no…","Back to the grave it is…","I’ll be back…","I am a son of darkness! You can’t do this to me!",""},
        new string[] {"This… is… impossible!","Mary, think this through!","What have you done?!","I will have my revenge…","My staff! My power! NO!",""}
    };

    private string[][] EnemyWin =
    {
        new string[] {"This line is empty and should never be seen"},
        new string[] {"Oh hey, I won!","Holy smokes! I didn’t mean to hurt you that much!","You owe me a drink","W-want to try again?","This was… unexpected",""},
        new string[] {"And just like that, the great Bloody Mary has now become fertilizer for Senior Bones’ flower garden!","Bested! Oh yeah, did you want to go out for lunch or something?","Don’t mess with me or my flowers again, chica!","Would it be weird of me to ask for a date now?","Don’t feel bad, Mary. You only got totally owned by the power of flowers, tonto!",""},
        new string[] {"*Howl!*","*Chases tail*","*Nap time*",""},
        new string[] {"Unsurprising…","You’re nothing but a clueless little girl, my dear.","And now for a taste test!","Lord, I’m thirsty!","Ah, the crimson water will flow like wine!",""},
        new string[] {"Who would have suspected this outcome, Muahahaha!","I’ll bring you back and destroy you again, Mary!","Now back in line, filthy sinner!","As punishment, you will be my personal plaything for the next few eternities.","Ridiculous display, my sweet daughter. You cannot best me.",""}
    };

    public void MaryGreeting()
    {
        characterAnimations.animator.SetTrigger("Smiling");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MarysGreetings[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MarysGreetings[i][j]));
    }
    
    public void MaryStaffPower()
    {
        enemyAnimations.animator.SetTrigger("Scared");
        int i = DataAcrossScenes.PlayerChosenStaff;
        int j = Random.Range(0, MaryUsingStaffs[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MaryUsingStaffs[i][j]));
    }
    
    public void MaryCashOut()
    {
        characterAnimations.animator.SetTrigger("Smiling");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MaryAttacks[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MaryAttacks[i][j]));
    }
    
    public void MaryTakingDamage()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MaryHurts[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MaryHurts[i][j]));
    }
    
    public void MaryWin()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MaryWins[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MaryWins[i][j]));
    }
    
    public void MaryLoss()
    {
        characterAnimations.animator.SetTrigger("Angry");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, MaryLoses[i].GetUpperBound(0));
        StartCoroutine(TypeWriterEffect(MaryLoses[i][j]));
    }
    
    public void MaryStalemate()
    {
        MaryText.text = "Using a mirror power on me...how qaint";
    }

    public void EnemyStalemate()
    {
        EnemyText.text = "I'm wasting your time, it's part of my job you know";
    }

    public void EnemyGreeting()
    {
        enemyAnimations.animator.SetTrigger("Angry");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyGreetings[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyGreetings[i][j]));
    }

    public void EnemyUsingStaff()
    {
        characterAnimations.animator.SetTrigger("Scared");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyUsingStaffs[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyUsingStaffs[i][j]));
    }

    public void EnemyAttack()
    {
        enemyAnimations.animator.SetTrigger("Angry");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyAttacking[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyAttacking[i][j]));
    }

    public void EnemyHurts()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyHurt[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyHurt[i][j]));
    }

    public void EnemyLoses()
    {
        enemyAnimations.animator.SetTrigger("Angry");
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyLoss[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyLoss[i][j]));
    }

    public void EnemyWins()
    {
        int i = DataAcrossScenes.EnemyChosenStaff;
        int j = Random.Range(0, EnemyWin[i].GetUpperBound(0)); 
        StartCoroutine(TypeWriterEffectEnemy(EnemyWin[i][j]));
    }

    private IEnumerator TypeWriterEffect(string MarysCurrentText)
    {
        for (int i = 0; i < MarysCurrentText.Length+1; i++)
        {
            MaryText.text = MarysCurrentText.Substring(0, i);

            yield return new WaitForSeconds(Delay);
        }
    }
    
    private IEnumerator TypeWriterEffectEnemy(string EnemysCurrentText)
    {
        for (int i = 0; i < EnemysCurrentText.Length + 1; i++)
        {
            EnemyText.text = EnemysCurrentText.Substring(0, i);

            yield return new WaitForSeconds(Delay);
        }
    }
}
