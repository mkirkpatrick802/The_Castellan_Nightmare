using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationPower;
    private Rigidbody2D _rb;
    private Coroutine _spinCoroutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Interactable.playerInteracting += PlayerInteract;
        PlayerInput.playerMoving += PlayerMoving;
    }

    private void OnDisable()
    {
        Interactable.playerInteracting -= PlayerInteract;
        PlayerInput.playerMoving -= PlayerMoving;
    }

    private void PlayerInteract()
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
                if (_spinCoroutine != null) return;
                _spinCoroutine = StartCoroutine(Spin());
                break;
            case PlayerAnimationStates.Moving:
                if (_spinCoroutine == null) return;
                StopCoroutine(_spinCoroutine);
                _spinCoroutine = null;
                break;
        }
    }

    private IEnumerator Spin()
    {
        while(true)
        {
            var impulse = (rotationPower * Mathf.Deg2Rad) * _rb.inertia;
            _rb.AddTorque(impulse, ForceMode2D.Impulse);
            yield return new WaitForSeconds(rotationSpeed);
        }
    }
}

public enum PlayerAnimationStates
{
    Interact,
    Moving,
}
