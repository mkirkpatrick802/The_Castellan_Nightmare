using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class UpgradeStation : MonoBehaviour
{
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
    }

    private void OnDisable()
    {
        PlayerInput.playerInteract -= InteractCheck;
    }
    
    private void InteractCheck(Vector2 playerPos)
    {
        if(!_col.OverlapPoint(playerPos)) return;
        if(CoinsManager.Coins < 1) return;
        Interact();
    }
    
    protected abstract void Interact();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, interactionRadius);
    }
}
