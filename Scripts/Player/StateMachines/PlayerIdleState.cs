using Godot;

namespace ExodusGame.Scripts.Player.StateMachines;

public partial class PlayerIdleState : PlayerState
{
    public override void HandleInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("move_down") || Input.IsActionJustPressed("move_left") ||
            Input.IsActionJustPressed("move_right") || Input.IsActionJustPressed("move_up"))
        {
            PSM.TransitionTo("Moving");
        }

        if (Input.IsActionJustPressed("interact"))
        {
            PSM.TransitionTo("Interacting");
        }
    }
}