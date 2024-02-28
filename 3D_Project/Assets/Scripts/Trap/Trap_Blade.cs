using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Blade : TrapBase
{
    Animator animator;
    readonly int ActivateHash = Animator.StringToHash("Activate");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnTrapActivate(GameObject target)
    {
        animator.SetTrigger(ActivateHash);
    }
}
