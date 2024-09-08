using System;
using Godot;

namespace Bunkify.Scripts.PlayerScripts;

public partial class PlayerController : Node2D
{
    // TODO: convert to inherit from Player, and child nodes to this
    
    private PlayerInputHandler _inputHandler;
    private PlayerInteractionController _interactionController;
    private PlayerMovementController _movementController;
    public Player Body;

    public Action<Vector2> BodyMovement;

    public override void _Ready()
    {
        Body = GetParent<Player>();
        _inputHandler = GetNode<PlayerInputHandler>("PlayerInputHandler");
        _interactionController = GetNode<PlayerInteractionController>("PlayerInteractionController");
        _movementController = GetNode<PlayerMovementController>("PlayerMovementController");

        // Subscribe to input events
        _inputHandler.OnPhysicsUpdate += direction => _movementController.Move(direction);
        _movementController.OnMove += velocity => BodyMovement?.Invoke(velocity);
      //  _inputHandler.InputHandled += 
        // TODO: subscribe to attack & interaction events
    }

    public void UpdatePlayerPhysics(double delta)
    {
        _inputHandler.HandlePhysics(delta);
    }

    public void InputUpdate(InputEvent @event)
    {
        _inputHandler.HandleInput(@event);
    }

    public override void _ExitTree()
    {
        _inputHandler.OnPhysicsUpdate -= _movementController.Move;
        _movementController.OnMove -= BodyMovement;
    }


}