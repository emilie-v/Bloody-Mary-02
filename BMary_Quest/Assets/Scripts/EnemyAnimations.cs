using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{

    public Animator enemyAnimator;
    public SpriteRenderer enemySpriteRenderer;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator.SetTrigger("Idle");
    }
}
