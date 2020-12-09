using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGameButtons : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
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
        if (this.gameObject == markButton)
        {
            markButtonInfo.SetActive(true);
        }
        else if (this.gameObject == staffButton)
        {
            staffButtonInfo.SetActive(true);
        }
        else if (this.gameObject == cashoutButton)
        {
            cashoutButtonInfo.SetActive(true);
        }
        else if (this.gameObject == endTurnButton)
        {
            endTurnButtonInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject == markButton)
        {
            markButtonInfo.SetActive(false);
        }
        else if (this.gameObject == staffButton)
        {
            staffButtonInfo.SetActive(false);
        }
        else if (this.gameObject == cashoutButton)
        {
            cashoutButtonInfo.SetActive(false);
        }
        else if (this.gameObject == endTurnButton)
        {
            endTurnButtonInfo.SetActive(false);
        }
    }
}
