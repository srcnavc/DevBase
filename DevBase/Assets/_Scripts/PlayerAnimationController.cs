using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationController : BaseAnimationController<PlayerState>
{
    public override Animator Anim
    {
        get
        {
            
            return GetComponentInChildren<Animator>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
    }
    
    private void OnDestroy()
    {
        PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

    protected override void OnPlayerStateChanged(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                SetBool("IsRunning", false);
                break;
            case PlayerState.Run:
                SetBool("IsRunning", true);
                break;
            default:
                break;
        }
    }
}
public enum GameType
{
    IO,
    Runner
}