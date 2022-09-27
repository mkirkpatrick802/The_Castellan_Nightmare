using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action<GameState> gameStateChanged;
    public GameState currentGameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SpawnEnemies);
    }

    public void UpdateGameState(GameState newState)
    {
        currentGameState = newState;
        switch (currentGameState)
        {
            case GameState.Menu:
                break;
            case GameState.LoadGame:
                break;
            case GameState.StartGame:
                break;
            case GameState.SpawnEnemies:
                break;
            case GameState.EndGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        gameStateChanged?.Invoke(currentGameState);
    }
}

public enum GameState
{
    Menu,
    LoadGame,
    StartGame,
    SpawnEnemies,
    EndGame
}
