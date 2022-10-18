using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> gameStateChanged;
    public GameState currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        UpdateGameState(GameState.SpawnEnemies);
    }

    public void UpdateGameState(GameState newState)
    {
        currentGameState = newState;

        gameStateChanged?.Invoke(currentGameState);
    }
}

public enum GameState
{
    Menu,
    LoadGame,
    StartGame,
    SpawnEnemies,
    EnemiesActive,
    EndGame
}
