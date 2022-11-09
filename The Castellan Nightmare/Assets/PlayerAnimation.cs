using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //[SerializeField] private float 
    private Coroutine _spinCoroutine;

    private void OnEnable()
    {
        PlayerInput.playerInteract += PlayerInteract;
        PlayerInput.playerMoving += PlayerMoving;
    }

    private void OnDisable()
    {
        PlayerInput.playerInteract -= PlayerInteract;
        PlayerInput.playerMoving -= PlayerMoving;
    }

    private void PlayerInteract(Vector2 vector2)
    {
        AnimatePlayer(PlayerAnimationStates.Interact);
    }

    private void PlayerMoving()
    {
        AnimatePlayer(PlayerAnimationStates.Moving);
    }

    public void AnimatePlayer(PlayerAnimationStates state)
    {
        switch (state)
        {
            case PlayerAnimationStates.Interact:
                _spinCoroutine = StartCoroutine(Spin());
                break;
            case PlayerAnimationStates.Moving:
                StopCoroutine(_spinCoroutine);
                break;
        }
    }

    private IEnumerator Spin()
    {
        while(true)
        {
            //transform.Rotate();
        }
    }
}

public enum PlayerAnimationStates
{
    Interact,
    Moving,
}
