using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    Sprite[] staffRewardList;

    Sprite pumpkinReward;
    Sprite countReward;
    Sprite hellReward;
    Sprite replayReward;

    public Image currentStaffReward;

    public GameObject rewardScreen;

    void Start()
    {
        pumpkinReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        countReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count");
        hellReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell_Portrait");
        replayReward = Resources.Load<Sprite>("Sprites/Staffs/Replay");

        staffRewardList = new Sprite[4];
        staffRewardList[0] = pumpkinReward;
        staffRewardList[1] = countReward;
        staffRewardList[2] = hellReward;
        staffRewardList[3] = replayReward;
    }

    public void newReward(int index)
    {
        currentStaffReward.sprite = staffRewardList[index];
        rewardScreen.SetActive(true);
        
    }
}
