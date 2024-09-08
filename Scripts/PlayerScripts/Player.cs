using System;
using Godot;

namespace Bunkify.Scripts.PlayerScripts;

public partial class Player : CharacterBody2D
{
    // Setters
    private Area2D _interactionArea;
    private PlayerController _playerController;
    private Node2D _inArea;
    
    // Player Properties
    public override void _Ready()
    {
        _interactionArea = GetNode<Area2D>("InteractionArea");
        _playerController = GetNode<PlayerController>("PlayerController");
        _inArea = null;

        // Subscribe to events
        _playerController.BodyMovement += BodyMovement;
        _interactionArea.AreaEntered += InteractionAreaOnAreaEntered;
        _interactionArea.AreaExited += InteractionAreaOnAreaExited;
    }
    

    public override void _ExitTree()
    {
        // Unsubscribe to events
        _playerController.BodyMovement -= BodyMovement;
        // Interaction Area; Area Entered/Exited
        _interactionArea.AreaEntered -= InteractionAreaOnAreaEntered;
        _interactionArea.AreaExited -= InteractionAreaOnAreaExited;
    }

    private void InteractionAreaOnAreaExited(Area2D area)
    {
        if (!area.GetParent().IsInGroup("interactable")) return;
        Logger.Log($"Left {area.GetParent().Name} {area.Name}!", Name);
        _inArea = null;
    }

    private void InteractionAreaOnAreaEntered(Area2D area)
    {
        if (!area.GetParent().IsInGroup("interactable")) return;
        Logger.Log($"Entered {area.GetParent().Name}{area.Name}", Name);
        _inArea = area;
    }

    public override void _PhysicsProcess(double delta)
    {
        _playerController.UpdatePlayerPhysics(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _playerController.InputUpdate(@event);
    }

    private void BodyMovement(Vector2 newVelocity)
    {
        Velocity = newVelocity;
        MoveAndSlide();
    }
}