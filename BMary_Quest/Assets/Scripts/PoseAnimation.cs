using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseAnimation : MonoBehaviour
{
    public Sprite[] maryPoseList;
    public Sprite maryAngry;
    public Sprite maryDamaged;
    public Sprite maryScared;

    public Sprite currentMaryPose;

    public int maryIndex;

    public void Start()
    {
        maryAngry = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Angry");
        maryDamaged = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Damaged");
        maryScared = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Scared");

        maryPoseList = new Sprite[3];
        maryPoseList[0] = maryAngry;
        maryPoseList[1] = maryDamaged;
        maryPoseList[2] = maryScared;
    }

    public void updateMaryPose(int index)
    {
     //   currentMaryPose.sprite = maryPoseList[index];
    }
}
