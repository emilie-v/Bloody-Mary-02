using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    
    public void OptionsPanel()
    {
        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        else if (!optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
        }
    }
}
