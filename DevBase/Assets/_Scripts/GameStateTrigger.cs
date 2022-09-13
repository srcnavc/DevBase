using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateTrigger : MonoBehaviour
{
    [SerializeField] GameState TriggerState;
    [SerializeField] UnityEvent TriggerEvent;
 
    public UnityEvent _TriggerEvent => TriggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= StateChange;
    }
    public void StateChange(GameState newState)
    {
        if (newState == TriggerState)
        {
            TriggerEvent.Invoke();
        }
    }

    
}
