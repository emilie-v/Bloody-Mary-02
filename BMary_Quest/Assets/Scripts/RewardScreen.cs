using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    Sprite[] staffRewardList;
    //TODO change countReward to his staff
    Sprite pumpkinReward;
    Sprite skeletonReward;
    Sprite moonReward;
    Sprite countReward;
    Sprite hellReward;
    Sprite replayReward;

    Sprite[] opponentRewardList;

    Sprite theCountReward;
    Sprite luciferReward;
    Sprite bonesReward;
    Sprite umbralinaReward;

    public Image currentStaffReward;
    public Image currentOpponentReward;
    public GameObject rewardScreen;

    void Start()
    {
        pumpkinReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin_Portrait");
        skeletonReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        moonReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        countReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Darkest_Night_Portrait");
        hellReward = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell_Portrait");
        replayReward = Resources.Load<Sprite>("Sprites/Staffs/Replay");

        bonesReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_SenorBones_Portrait");
        umbralinaReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Umbralina_Portrait");
        theCountReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count_Portrait");
        luciferReward = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Lucifer_Portrait");

        staffRewardList = new Sprite[6];
        staffRewardList[0] = pumpkinReward;
        staffRewardList[1] = skeletonReward;
        staffRewardList[2] = moonReward;
        staffRewardList[3] = countReward;
        staffRewardList[4] = hellReward;
        staffRewardList[5] = replayReward;

        opponentRewardList = new Sprite[6];
        opponentRewardList[0] = bonesReward;
        opponentRewardList[1] = umbralinaReward;
        opponentRewardList[2] = theCountReward;
        opponentRewardList[3] = luciferReward;
        opponentRewardList[5] = replayReward;
    }

    public void newReward(int index)
    {
        currentStaffReward.sprite = staffRewardList[index];
        currentOpponentReward.sprite = opponentRewardList[index];
        rewardScreen.SetActive(true);
    }


}
