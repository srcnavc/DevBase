using System;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager _gameStateManager;
    [SerializeField] private GameState currentState;
    public static event Action<GameState> OnGameStateChange;

    public static GameState GetState()
    {
        if (_gameStateManager == null)
        {
            _gameStateManager = new GameStateManager();
        }

        return _gameStateManager.currentState;
    }

    public static void SetState(GameState gameState)
    {
        if (_gameStateManager == null)
        {
            _gameStateManager = new GameStateManager();
        }

        _gameStateManager.currentState = gameState;
        OnGameStateChange?.Invoke(gameState);
    }
}

public enum GameState
{
    StartScreen = 0, // Very first State  (Tap To start)
    Loading = 1, // loadingSplash
    Launching = 2, //Game play start to Controls are available period (character wake up, plane takes off ect.)
    play = 3, // controls are available and game has started
    failCalculating = 4, // Game ended player lose , waiting for calculation or adscene or any other penalty to end (untill retry enabled)
    fail = 5, // Game ended and player lose state untill retry pressed
    winCalculating = 6, //  Game ended player won , waiting for calculation or adscene or anyother penalty to end (untill Next Level enabled)
    win = 7, // player won state (untill the retry pressed)
    Tutorial = 8, // Tutorial info
}