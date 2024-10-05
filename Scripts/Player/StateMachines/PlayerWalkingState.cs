using Godot;

namespace ExodusGame.Scripts.Player.StateMachines;

public class PlayerWalkingState : PlayerState
{
    public PlayerWalkingState(Player.StateMachines.PlayerStateMachine stateMachine) : base(stateMachine) {}

    private Vector2 _velocity;

    public override void Enter()
    {
        base.Enter();
        #if DEBUG
        Logger.Log("Player started walking");
        #endif
    }

    public override void Exit()
    {
        base.Exit();
        #if DEBUG
        Logger.Log("Player stopped walking");
        #endif
    }

    public override void HandleInput(InputEvent @event)
    {
        _velocity = Vector2.Zero;
        
        // Process input for walking
        // Use pre-defined actions from the Input Map
        if (Input.IsActionPressed("move_up"))
        {
            _velocity.Y = -1;
        }
        if (Input.IsActionPressed("move_down"))
        {
            _velocity.Y = 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            _velocity.X = -1;
        }
        if (Input.IsActionPressed("move_right"))
        {
            _velocity.X = 1;
        }
        
        // Normalize velocity to prevent faster diagonal movement
        _velocity = _velocity.Normalized();
    }

    public override void UpdateState(double delta)
    {
        var player = stateMachine.Player;
        if (_velocity != Vector2.Zero)
        {
            player.Position += _velocity * player.MovementSpeed * (float)stateMachine.GetProcessDeltaTime();
        }
        else
        {
            stateMachine.ChangeState(new PlayerIdleState(stateMachine));
        }
    }
    

}