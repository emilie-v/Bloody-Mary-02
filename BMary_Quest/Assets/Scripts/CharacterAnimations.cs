using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("Idle");
        }
    }
}
