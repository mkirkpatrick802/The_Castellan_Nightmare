using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> playerInteractCheck;
    public static event Action<Vector2> playerUpgradeCheck;
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
        if (Input.GetKeyDown(KeyCode.Space)) playerInteractCheck?.Invoke(transform.position);        //Invoke an event if the player try's to interact.
        if (Input.GetKeyDown(KeyCode.Mouse0)) playerUpgradeCheck?.Invoke(transform.position);         //Invoke an event if the player try's to upgrade

        //TODO: Find a way to do this only if the player is in progress of doing a task
        if (math.any(_movementDir)) playerMoving?.Invoke();                                     //Invoke an event if the player is moving
    }

    private void FixedUpdate()
    {
        _controller.PlayerMove(_movementDir);
    }
}
