using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseAnimation : MonoBehaviour
{
    public Sprite[] maryPoseList;
    public Sprite maryIdle;
    public Sprite maryAngry;
    public Sprite maryDamaged;
    public Sprite maryScared;

    public Image currentMaryPose;
    public int maryIndex;

    public void Start()
    {
        maryIdle = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Idle");
        maryAngry = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Angry");
        maryDamaged = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Damaged");
        maryScared = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Scared");

        maryPoseList = new Sprite[4];
        maryPoseList[0] = maryIdle;
        maryPoseList[1] = maryAngry;
        maryPoseList[2] = maryDamaged;
        maryPoseList[3] = maryScared;

        updateMaryPose(0);


    }

    public void updateMaryPose(int index)
    {
      currentMaryPose.sprite = maryPoseList[index];
    }
}
