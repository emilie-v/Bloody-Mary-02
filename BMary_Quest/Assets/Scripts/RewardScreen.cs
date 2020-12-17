using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    Sprite[] staffRewardList;
    //TODO change countReward to his staff
    Sprite pumpkinReward;
    Sprite countReward;
    Sprite hellReward;
    Sprite replayReward;

    Sprite[] opponentRewardList;

    Sprite theCountReward;
    Sprite luciferReward;

    public Image currentStaffReward;
    public Image currentOpponentReward;
    public GameObject rewardScreen;

    void Start()
    {
        pumpkinReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        countReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count");
        hellReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell_Portrait");
        replayReward = Resources.Load<Sprite>("Sprites/Staffs/Replay");

        theCountReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count");
        luciferReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Lucifer_Portrait");

        staffRewardList = new Sprite[4];
        staffRewardList[0] = pumpkinReward;
        staffRewardList[1] = countReward;
        staffRewardList[2] = hellReward;
        staffRewardList[3] = replayReward;

        opponentRewardList = new Sprite[2];
        opponentRewardList[0] = theCountReward;
        opponentRewardList[1] = luciferReward;
    }

    public void newReward(int index)
    {
        currentStaffReward.sprite = staffRewardList[index];
        currentOpponentReward.sprite = opponentRewardList[index];
        rewardScreen.SetActive(true);

        
    }


}
