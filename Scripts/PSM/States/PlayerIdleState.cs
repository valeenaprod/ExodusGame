using Godot;

namespace ExodusGame.Scripts.PSM.States;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        // Play idle animation or stop movement action...
        Logger.Log("Player is now idle.");
    }

    public override void Exit()
    {
        base.Exit();
        // Example: prepare for transition to another state
    }

    public override void HandleInput(InputEvent @event)
    {
        // If movement keys are pressed, transition to WalkingState
        
        if (Input.IsActionJustPressed("move_up") || Input.IsActionJustPressed("move_down") 
            || Input.IsActionJustPressed("move_right") || Input.IsActionJustPressed("move_left"))
        {
            stateMachine.ChangeState(new PlayerWalkingState(stateMachine));
        }
            
    }

    public override void UpdateState(double delta)
    {
        // No continuous update while in Idle, so nothing to do here for now
    }
}