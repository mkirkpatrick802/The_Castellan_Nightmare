using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AnimationBase
{
    public override void ChangeAnimationState(AnimationStates newState)
    {
        if (currentState == newState) return;

        animator.Play(GetAnimationName(newState));
        currentState = newState;
    }

    protected override string GetAnimationName(AnimationStates state)
    {
        string name = null;
        switch (state)
        {
            case AnimationStates.Idle:
                name = "Enemy_Idle";
                break;
            case AnimationStates.Walk:
                break;
            case AnimationStates.Attack:
                name = "Enemy_Attack";
                break;
        }

        return name;
    }
}
