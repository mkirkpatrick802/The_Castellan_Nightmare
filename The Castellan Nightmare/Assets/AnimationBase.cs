using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationBase : MonoBehaviour
{
    protected Animator animator;
    protected AnimationStates currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void ChangeAnimationState(AnimationStates newState);

    protected abstract string GetAnimationName(AnimationStates state);
}

public enum AnimationStates
{
    Idle = 0,
    Walk,
    Attack,
}
