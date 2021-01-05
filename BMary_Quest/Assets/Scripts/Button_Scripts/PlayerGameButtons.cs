using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerGameButtons : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] private GameControl gameControl;
    [SerializeField] private GameObject spelplan;
    [SerializeField] private GameObject markButton;
    [SerializeField] private GameObject staffButton;
    [SerializeField] private GameObject cashoutButton;
    [SerializeField] private GameObject endTurnButton;
    
    [SerializeField] private GameObject markButtonInfo;
    [SerializeField] private GameObject staffButtonInfo;
    [SerializeField] private GameObject cashoutButtonInfo;
    [SerializeField] private GameObject endTurnButtonInfo;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject == markButton)
        {
            markButtonInfo.SetActive(true);
        }
        else if (gameObject == staffButton)
        {
            staffButtonInfo.SetActive(true);
        }
        else if (gameObject == cashoutButton)
        {
            int marysTP = 0;
            for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                {
                    if (spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player1 
                        && spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                    {                    
                        marysTP++;                    
                    }
                }
            }
            
            marysTP++; //Startmarker
            marysTP = marysTP * gameControl.maryDamageMultiplier; //Skeletonmark
            
            GameObject.Find("Button_Info").transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text 
                = "Hotkey: " + gameControl.playerCashOutHotkey + "\nCurrent Damage: " + marysTP;
            
            cashoutButtonInfo.SetActive(true);
        }
        else if (gameObject == endTurnButton)
        {
            endTurnButtonInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject == markButton)
        {
            markButtonInfo.SetActive(false);
        }
        else if (gameObject == staffButton)
        {
            staffButtonInfo.SetActive(false);
        }
        else if (gameObject == cashoutButton)
        {
            cashoutButtonInfo.SetActive(false);
        }
        else if (gameObject == endTurnButton)
        {
            endTurnButtonInfo.SetActive(false);
        }
    }
}
