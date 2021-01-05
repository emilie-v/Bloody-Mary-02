using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurnStartText : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    [SerializeField] private Text turnStartText;
    
    public IEnumerator TurnStart()
    {
        yield return new WaitForSeconds(0);
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            turnStartText.text = "Mary's Turn";
        }
        else if (gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            turnStartText.text = "Enemy's Turn";
        }
        
        turnStartText.gameObject.SetActive(true);
        turnStartText.GetComponent<RectTransform>().DOScale(1, 0.4f);
        yield return new WaitForSeconds(2);
        turnStartText.GetComponent<RectTransform>().DOScale(0.1f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        turnStartText.gameObject.SetActive(false);
    }
}
