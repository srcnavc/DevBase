using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IStateProvider<PlayerState>
{
    public static PlayerController ins;
    public static Action<PlayerState> OnPlayerStateChanged;
    public static Action<GameType> OnGameTypeChanged;
    [SerializeField] PlayerState state;
    [SerializeField] GameState gameState;
    [SerializeField] GameType gameType;
    
    public PlayerState State
    {
        get => state;

        set
        {
            if(state != value)
            {
                state = value;
                OnPlayerStateChanged?.Invoke(state);
            }
        }
    }

    public GameType GameType
    {
        get => gameType; 
        
        set
        {
            if (gameType != value)
            {
                gameType = value;
                OnGameTypeChanged?.Invoke(gameType);
            }
        }
    }

    public void SetGameStateToPlay()
    {
        GameStateManager.SetState(GameState.play);
    }
    
    
    private void Awake()
    {
        if (ins == null)
            ins = this;

        GameStateManager.OnGameStateChange += OnGameGameStateChanged;
    }

    private void Update()
    {
        if (GameStateManager.GetState() == GameState.StartScreen || GameStateManager.GetState() == GameState.win || GameStateManager.GetState() == GameState.fail)
        {
            if (State != PlayerState.Idle)
                State = PlayerState.Idle;
        }
    }

    private void OnGameGameStateChanged(GameState newState)
    {
        gameState = newState;
    }

    public PlayerState GetState()
    {
        return State;
    }
}
public enum PlayerState
{
    Idle,
    Run
}

