using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Inspector
    [SerializeField] private float moveSpeed;
    
    //Private
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void PlayerMove(Vector2 moveDir)
    {
        _rb.velocity = moveDir * moveSpeed;
    }
}
