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

    public void Start()
    {
        hover.AddListener(OnMouseEnter);
        hover.AddListener(OnMouseExit);
    }

    public void Update()
    {
       
    }
    public void OnMouseEnter()
    {
        Debug.Log("Vi hovrade över knappen!");
        marysInfoCanvas.SetActive(true);
        enemyInfoCanvas.SetActive(true);
    }
    public void OnMouseExit()
    {
        Debug.Log("Vi lämnade knappen!");
        marysInfoCanvas.SetActive(false);
        enemyInfoCanvas.SetActive(false);
    }

}