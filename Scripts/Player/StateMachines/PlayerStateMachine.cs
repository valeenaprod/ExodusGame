using Godot;

namespace ExodusGame.Scripts.Player.StateMachines;

public partial class PlayerStateMachine : Node
{
    public PlayerController Player { get; private set; }
    
    private PlayerState _currentState;

    public void Initialize(PlayerController player)
    {
        Player = player;
    }
    public void ChangeState(PlayerState newState)
    {
        if (newState == null)
        {
            Logger.Log("Cannot change to a null state.", Logger.LogLevel.Error);
            return;
        }
        
        // Handle optional transition logic in the current state
        _currentState?.OnTransition(newState);
        
        // Exit the current stat if it exists
        _currentState?.Exit();
        
        // Switch to the new state
        _currentState = newState;
        
        // Enter the new state
        _currentState.Enter();
    }

    public override void _Process(double delta)
    {
        _currentState?.UpdateState(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState?.HandleInput(@event);
    }
    
}