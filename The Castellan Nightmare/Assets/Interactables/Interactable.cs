using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    [SerializeField] private float interactionRadius;
    private CircleCollider2D _col;

    private void Awake()
    {
        _col = gameObject.AddComponent<CircleCollider2D>();
        _col.radius = interactionRadius;
        _col.isTrigger = true;
    }

    private void OnEnable()
    {
        PlayerInput.playerInteract += InteractCheck;
        PlayerInput.playerMoving += Cancel;
    }

    private void OnDisable()
    {
        PlayerInput.playerInteract -= InteractCheck;
        PlayerInput.playerMoving -= Cancel;
    }
    
    private void InteractCheck(Vector2 playerPos)
    {
        if(!_col.OverlapPoint(playerPos)) return;
        Interact();
    }

    protected virtual void Cancel() { } // Only Tasks are cancelable so im not making it a pure virtual
    
    protected abstract void Interact();


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, interactionRadius);
    }
}
