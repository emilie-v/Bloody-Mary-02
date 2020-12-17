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

        staffRewardList = new Sprite[6];
        staffRewardList[0] = pumpkinReward;
        staffRewardList[1] = pumpkinReward; //Change to Skeleton
        staffRewardList[2] = pumpkinReward; //Change to moon
        staffRewardList[3] = countReward;
        staffRewardList[4] = hellReward;
        staffRewardList[5] = replayReward;
    }

    public void newReward(int index)
    {
        currentStaffReward.sprite = staffRewardList[index];
        rewardScreen.SetActive(true);
        
    }
}
