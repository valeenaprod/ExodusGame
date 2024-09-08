using System;
using Godot;

namespace Bunkify.Scripts.PlayerScripts;

public partial class PlayerInputHandler : Node2D
{
    private CharacterBody2D _body;
    private PlayerController _controller;

    public event Action<Vector2> OnPhysicsUpdate;
    public event Action<Node2D> InputHandled ;
    public event Action OnAttack;
    public event Action OnInteract;

    public override void _Ready()
    {
        _controller = GetParent<PlayerController>();
        _body = _controller.Body;
    }

    // Method to get the input direction that was pressed
    private static Vector2 GetInputDirection()
    {
        var x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        var y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return new Vector2(x, y).Normalized();
    }

    // Method called by the PlayerController to tell what input was used
    public void HandlePhysics(double delta)
    {
        if (GetInputDirection() == Vector2.Zero) return;
        var direction = GetInputDirection();
        OnPhysicsUpdate?.Invoke(direction);
    }
    
    public void HandleInput(InputEvent @event)
    {
        if (!Input.IsActionJustPressed("interact") || @event is not InputEventMouseButton mouseEvent) return;
        var collider = GetObjectMouseClick(mouseEvent.Position);
        InputHandled?.Invoke(collider);
    }

    private Node2D GetObjectMouseClick(Vector2 mousePosition)
    {
        // Create a new PhysicsPointQueryParameters2D object
        var pointQueryParameters = new PhysicsPointQueryParameters2D();
        // Set the Position of the Query to the Vector2 mousePosition
        pointQueryParameters.Position = mousePosition;

        // Perform the query using a direct space state
        var spaceState = GetWorld2D().DirectSpaceState;
        var results = spaceState.IntersectPoint(pointQueryParameters);

        // Check if the results contains a collider

        foreach (var result in results)
            if (result.TryGetValue("collider", out var value))
            {
                var collider = (Node2D)value;
                Logger.Log(result.ToString(), Name);
                Logger.Log($"Collider found: {collider.Name}", Name);
                return collider;
            }
            else
            {
                Logger.LogError("No collider found at this point!", Name);
                Logger.Log(result.ToString(), Name);
                return null;
            }
        return null;
    }
}