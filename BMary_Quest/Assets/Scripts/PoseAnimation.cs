using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseAnimation : MonoBehaviour
{
    Sprite[] maryPoses;
    Sprite maryAngry;
    Sprite maryDamaged;
    Sprite maryScared;

    public void Start()
    {
        maryAngry = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Angry");
        maryDamaged = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Damaged");
        maryScared = Resources.Load<Sprite>("Sprites/Characters/Mary_Reactions/Pose_Mary_Scared");

        maryPoses = new Sprite[3];
        maryPoses[0] = maryAngry;
        maryPoses[1] = maryDamaged;
        maryPoses[2] = maryScared;
    }
}
