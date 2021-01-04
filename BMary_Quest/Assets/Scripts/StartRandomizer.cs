using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StartRandomizer : MonoBehaviour
{
    public bool gameStart;
    public GameControl gameControl;
    
    public Sprite maryCard;
    public Sprite enemysCard;

    private Transform playerCard;
    private Transform enemyCard;

    private void Start()
    {
        playerCard = GameObject.Find("TurnCards").transform.GetChild(0);
        enemyCard = GameObject.Find("TurnCards").transform.GetChild(1);
        
        maryCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_BloodyMary");
        
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            enemysCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_Ghastella");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
        {
            enemysCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_SenorBones");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            enemysCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_Umbralina");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            enemysCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_Count");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            enemysCard = Resources.Load<Sprite>("Sprites/Characters/TurnCard/TurnCard_Lucifer");
        }

        playerCard.GetComponent<Image>().sprite = maryCard;
        enemyCard.GetComponent<Image>().sprite = enemysCard;
        
        StartCoroutine(TurnRandomizer());
    }

    private void Update()
    {
        HideCard(playerCard);
        HideCard(enemyCard);
    }

    private IEnumerator TurnRandomizer()
    {
        yield return new WaitForSeconds(0.5f);
        gameControl.UpdateBloodPoints();
        
        for (int i = 0; i < Random.Range(8, 13); i++)
        {
            yield return new WaitForSeconds(0.1f * i);
            if (gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerTurn = (int) Player_Turn.enemy;
            }
            else if (gameControl.playerTurn == (int) Player_Turn.enemy)
            {
                gameControl.playerTurn = (int) Player_Turn.mary;
            }

            CardSpin(i);
        }
        
        yield return new WaitForSeconds(1f);
        GameObject.Find("TurnCards").SetActive(false);
        
        gameControl.TurnStart();
        gameStart = true;
    }

    
    private void CardSpin(int i)
    {
        ResetRotation(playerCard);
        ResetRotation(enemyCard);
        
        playerCard.GetComponent<RectTransform>().DORotate(new Vector3(0, playerCard.GetComponent<RectTransform>().rotation.eulerAngles.y + 180, 0), 0.1f * i);
        enemyCard.GetComponent<RectTransform>().DORotate(new Vector3(0, enemyCard.GetComponent<RectTransform>().rotation.eulerAngles.y + 180, 0), 0.1f * i);
    }

    private void HideCard(Transform card)
    {
        if (card.GetComponent<RectTransform>().rotation.eulerAngles.y >= 90 && card.GetComponent<RectTransform>().rotation.eulerAngles.y <= 270)
        {
            card.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        
        if (card.GetComponent<RectTransform>().rotation.eulerAngles.y >= 0 && card.GetComponent<RectTransform>().rotation.eulerAngles.y <= 90
            || card.GetComponent<RectTransform>().rotation.eulerAngles.y >= 270)
        {
            card.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    private void ResetRotation(Transform card)
    {
        if (card.GetComponent<RectTransform>().rotation.eulerAngles.y >= 360)
        {
            card.GetComponent<RectTransform>().rotation.Set(0, 0, 0, 0);
        }
    }
}
