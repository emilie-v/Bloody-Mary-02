using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public Animator animator;

    public Animator enemyAnimator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
    }
}
