using Godot;

namespace ExodusGame.Scripts.PSM.States;

public partial class Moving : PlayerState
{
    public override void PhysicsUpdate(double delta)
    {
        var direction = GetInputDirection();
        PSM.Player.Velocity = direction * PSM.Player.MovementSpeed;
        PSM.Player.MoveAndSlide();
    }
    private static Vector2 GetInputDirection()
    {
        var x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        var y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return new Vector2(x, y).Normalized();
    }
}