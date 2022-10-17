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
        UpdateGameState(GameState.StartGame);
    }

    public void UpdateGameState(GameState newState)
    {
        currentGameState = newState;
        gameStateChanged?.Invoke(currentGameState);
        
        switch (currentGameState)
        {
            case GameState.Menu:
                break;
            case GameState.LoadGame:
                break;
            case GameState.StartGame:
                UpdateGameState(GameState.SpawnEnemies);
                break;
            case GameState.SpawnEnemies:
                break;
            case GameState.EnemiesActive:
                break;
            case GameState.EndGame:
                print("Game Lost");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
