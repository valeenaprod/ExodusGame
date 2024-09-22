using System;
using Godot;

namespace ExodusGame.Scripts.PSM;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine))
    }
    
    // Log when the state is entered
    public virtual void Enter()
    {
        #if DEBUG
        Logger.Log($"Entered {GetType().Name} state.");
        #endif
    }

    // Log when the state is exited
    public virtual void Exit()
    {
        #if DEBUG
        Logger.Log($"Exited {GetType().Name} state.");
        #endif
    }

    public virtual void OnTransition(PlayerState nextState)
    {
        // Override in specific states for custom transition logic (animations, effects, etc)
    }

    public abstract void HandleInput(InputEvent @event);
    public abstract void UpdateState(double delta);

}