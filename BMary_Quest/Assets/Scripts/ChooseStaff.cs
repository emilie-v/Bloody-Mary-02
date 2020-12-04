using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseStaff : MonoBehaviour
{
    Sprite[] staffList;
    Sprite mirror;
    Sprite hell;
    Sprite padlock;

    public GameObject chooseEnemy;
    public GameObject chooseStaff;

    public GameObject selectWarning;
    public GameObject cancelWarning;

    public GameObject padlockImage;

    int index = 0;
    Image currentStaff;

    void Start()
    {
        currentStaff = GetComponent<Image>();

        mirror = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror");
        hell = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell");
        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");

        staffList = new Sprite[2];
        staffList[0] = mirror;
        staffList[1] = hell;

        currentStaff.sprite = staffList[index];
    }

    public void RightStaffButton()
    {
        index++;

        if (index >= staffList.Length)
        {
            index = 0;
        }
        currentStaff.sprite = staffList[index];
    }

    public void LeftStaffButton()
    {
        index--;

        if (index < 0)
        {
            index = staffList.Length - 1;
        }

        currentStaff.sprite = staffList[index];
    }

    public void CancelStaffWarningButton()
    {
        selectWarning.SetActive(false);
    }

    public void SelectStaffButton()
    {
        if (index == 0)
        {
            SceneManager.LoadScene("GameBoard");
        }
        else
        {
            selectWarning.SetActive(true);
        }

    }

    public void BackToEnemyButton()
    {
        chooseStaff.SetActive(false);
        chooseEnemy.SetActive(true);
    }

    private void Update()
    {
        if (index == 0)
        {
            padlockImage.SetActive(false);
        }
        else if(index == 1)
        {
            padlockImage.SetActive(true);
        }
    }

}
