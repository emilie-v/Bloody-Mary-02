using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        int index = DataAcrossScenes.EnemyChosenStaff;
        animator = gameObject.GetComponent<Animator>();
        if(index == (int)Chosen_Staff.pumpkin)
        {
            animator.runtimeAnimatorController = Resources.Load("Animations/EnemyAnimatorOverrides/Ghastella_AnimatorOverride") as RuntimeAnimatorController;
        }
        if (index == (int)Chosen_Staff.skeleton)
        {
            animator.runtimeAnimatorController = Resources.Load("Animations/EnemyAnimatorOverrides/SenorBones_AnimatorOverride") as RuntimeAnimatorController;
        }
        if (index == (int)Chosen_Staff.moon)
        {
            animator.runtimeAnimatorController = Resources.Load("Animations/EnemyAnimatorOverrides/Umbralina_AnimatorOverride") as RuntimeAnimatorController;
        }
        if (index == (int)Chosen_Staff.hell)
        {
            animator.runtimeAnimatorController = Resources.Load("Animations/EnemyAnimatorOverrides/Lucifer_AnimatorOverride") as RuntimeAnimatorController;
        }
        if (index == (int)Chosen_Staff.night)
        {
            animator.runtimeAnimatorController = Resources.Load("Animations/EnemyAnimatorOverrides/Count_AnimatorOverride") as RuntimeAnimatorController;
        }
    }
}
