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

    int index = 0;
    Image currentStaff;

    void Start()
    {
        currentStaff = GetComponent<Image>();

        mirror = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror");
        hell = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell");

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

    public void SelectStaffButton()
    {
        if (index == 0)
        {
            SceneManager.LoadScene("GameBoard");
        }

    }

}
