using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : RecycleObject
{    
    Animator animator;
    float animLength = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;         
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(animLength));
    }


}
