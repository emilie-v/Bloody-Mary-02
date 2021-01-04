using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class InfoButton : MonoBehaviour
{
    public UnityEvent hover = new UnityEvent();
    public GameObject marysInfoCanvas;
    public GameObject enemyInfoCanvas;
    public Text marysStaff;
    public Text enemyStaff;


    public void Start()
    {
        hover.AddListener(OnMouseEnter);
        hover.AddListener(OnMouseExit);
        SetText();
        
    }

    public void OnMouseEnter()
    {
        SetText();
        marysInfoCanvas.SetActive(true);
        enemyInfoCanvas.SetActive(true);
    }

    public void OnMouseExit()
    {
        marysInfoCanvas.SetActive(false);
        enemyInfoCanvas.SetActive(false);
    }

    void SetText()
    {
        // Marys Descriptions
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.mirror)
            marysStaff.text = "Passive ability: Adds 5 hp\nActive ability: mirrors the opponents move, flipped on the y-axis";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.pumpkin)
            marysStaff.text = "Passive ability: Adds a extra move\nActive ability:Adds extra cooldown to the enemy staff ability";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.skeleton)
            marysStaff.text = "Passive ability: none\nActive ability:Doubles the amount of damage your marks do";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon)
            marysStaff.text = "Passive ability: makes you bark\nActive ability:Reverses the ownership of the marks on the board";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night)
            marysStaff.text = "Passive ability:vampiric, you gain half the health of the damage you do\nActive ability:Locks tiles down making them unusable";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
            marysStaff.text = "Passive ability:you gain health every time you attack\nActive ability:curses the enemy, if they attack during that turn, they take the same amount of damage";
        // Enemy Descriptions
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.mirror)
            enemyStaff.text = "Passive ability: Adds 5 hp\nActive ability: mirrors the players move if able,flipped on the y-axis";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
            enemyStaff.text = "Passive ability: Adds a extra move for them\nActive ability:Adds extra cooldown to the player staff ability";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
            enemyStaff.text = "Passive ability: none\nActive ability:Doubles the amount of damage their marks do";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
            enemyStaff.text = "Passive ability: makes them bark\nActive ability:Reverses the ownership of the marks on the board";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
            enemyStaff.text = "Passive ability:vampiric, they gain half the health of the damage they do\nActive ability:Locks tiles down making them unusable";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
            enemyStaff.text = "Passive ability:The enemy gains health every time you attack\nActive ability:curses your tiles, if you attack during that turn, you take the same amount of damage";
    }


}