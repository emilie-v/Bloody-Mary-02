using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    public int piecesToMark = 1;
    public int piecesToMarkLeft = 1;
    
    public int staffCooldown = 2;
    
    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(SkeletonStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

    public void SkeletonStaffPassiveAbility()
    {
        //No passive ability for this one
    }

    public void SkeletonStaffActiveAbility()
    {
        if (!gameControl.staffUsed)
        {
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.skeleton && gameControl.playerStaffCooldown <= 0
                || DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton && gameControl.enemyStaffCooldown <= 0)
            {
                Debug.Log(piecesToMarkLeft);
                while (piecesToMarkLeft > 0)
                {
                    int check = 0;
                    foreach (Transform child in GameObject.Find("Spelplan").transform)
                    {
                        if (child.GetComponent<Owner>().owned == gameControl.playerTurn + 1 && !child.GetComponent<Owner>().skeletonMark)
                        {
                            if (Random.Range(0, 25) == 0 && piecesToMarkLeft > 0)
                            {
                                piecesToMarkLeft--;
                                child.GetComponent<Owner>().skeletonMark = true;
                            }
                        }

                        if (child.GetComponent<Owner>().owned != (int)Tile_State.empty && !child.GetComponent<Owner>().skeletonMark)
                        {
                            check++;
                        }
                    }

                    if (check == 0)
                    {
                        return;
                    }
                }
            }
            
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.skeleton && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.maryDamageMultiplier = 2;
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                dialogueTrigger.MaryStaffPower();
                SoundManager.Instance.ActivateStaffButtonSound();
            }

            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.enemyDamageMultiplier = 2;
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                SoundManager.Instance.ActivateStaffButtonSound();
            }
        }
    }
}
