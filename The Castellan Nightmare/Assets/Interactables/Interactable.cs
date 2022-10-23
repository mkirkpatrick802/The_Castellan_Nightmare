using UnityEngine;

public abstract class Interactable : Upgrade
{
    [Header("Interactable Settings")]
    [SerializeField] private float interactionRadius;
    private CircleCollider2D _col;

    protected override void Awake()
    {
        base.Awake();
        _col = gameObject.AddComponent<CircleCollider2D>();
        _col.radius = interactionRadius;
        _col.isTrigger = true;
    }

    protected override void OnEnable()
    {
        base .OnEnable();
        PlayerInput.playerInteract += InteractCheck;
        PlayerInput.playerMoving += Cancel;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, interactionRadius);
    }
}
