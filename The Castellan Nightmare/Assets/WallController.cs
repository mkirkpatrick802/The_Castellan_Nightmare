using System;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    public List<Transform> targets = new List<Transform>();

    private void Awake()
    {
        WallHealth.Health = startingHealth;
    }

    private void Update()
    {
        if(WallHealth.Health <= 0)
            GameManager.Instance.UpdateGameState(GameState.EndGame);
    }
}

public static class WallHealth
{
    public static event Action<int> wallHeathChanged;
    private static int _health;

    public static int Health
    {
        get => _health;
        set
        {
            if(_health == value) return;
            _health = value;
            wallHeathChanged?.Invoke(_health);
        }
    }
}
