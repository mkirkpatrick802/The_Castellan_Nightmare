using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> playerInteract;
    public static event Action playerMoving;
    private Vector2 _movementDir;
    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _movementDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) playerInteract?.Invoke(transform.position);
        if (math.any(_movementDir)) playerMoving?.Invoke();
    }

    private void FixedUpdate()
    {
        _controller.PlayerMove(_movementDir);
    }
    
}
