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
            marysStaff.text = "Passive: \nAdds 5 blood points.\nActive: Mirrors the opponents move, flipped on the y-axis.";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.pumpkin)
            marysStaff.text = "Passive: \nAdds an extra move.\nActive: \nAdds extra cooldown to the enemy's staff ability.";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.skeleton)
            marysStaff.text = "Passive: \nnone!\nActive: Doubles the amount of damage your marks do.";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon)
            marysStaff.text = "Passive: \nnone!\nActive: \nReverses the ownership of the marks on the board.";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night)
            marysStaff.text = "Passive: \nVampiric, you gain half the health of the damage you do.\nActive: \nLocks down down making them unusable.";
        else if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
            marysStaff.text = "Passive: \nYou gain health every time you attack.\nActive: \nCurses the enemy, if they attack during that turn, they take the same amount of damage.";
        
        // Enemy Descriptions
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.mirror)
            enemyStaff.text = "Passive: \nAdds 5 hp.\nActive: \nMirrors the players move if able, flipped on the y-axis.";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
            enemyStaff.text = "Passive: \nAdds an extra move for them.\nActive:\nAdds extra cooldown to the player staff ability.";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
            enemyStaff.text = "Passive: \nnone!\nActive: \nDoubles the amount of damage their marks do.";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
            enemyStaff.text = "Passive: \nnone!\nActive: \nReverses the ownership of the marks on the board.";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
            enemyStaff.text = "Passive: \nVampiric, they gain half the health of the damage they do.\nActive: \nLocks down tiles making them unusable.";
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
            enemyStaff.text = "Passive: \nThe enemy gains health every time you attack.\nActive: \nCurses your tiles; if you attack during that turn, you take the same amount of damage.";
    }
}