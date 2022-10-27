using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    [SerializeField] private float interactionRadius;
    private CircleCollider2D _col;

    protected virtual void Awake()
    {
        _col = gameObject.AddComponent<CircleCollider2D>();
        _col.radius = interactionRadius;
        _col.isTrigger = true;
    }

    protected virtual void OnEnable()
    {
        PlayerInput.playerInteract += InteractCheck;
        PlayerInput.playerMoving += Cancel;
    }

    protected virtual void OnDisable()
    {
        PlayerInput.playerInteract -= InteractCheck;
        PlayerInput.playerMoving -= Cancel;
    }
    
    private void InteractCheck(Vector2 playerPos)
    {
        if(!_col.OverlapPoint(playerPos)) return;
        Interact();
    }

    protected virtual void Cancel() { }

    protected virtual void Interact() { }


    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, interactionRadius);
    }
}
