using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> playerInteract;
    private Vector2 _movementDir;
    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) playerInteract?.Invoke(transform.position);
    }

    private void FixedUpdate()
    {
        _controller.PlayerMove(_movementDir);
    }
    
}
