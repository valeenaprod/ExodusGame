using Godot;

namespace ExodusGame.Scripts.Player;

public partial class PlayerController : CharacterBody2D
{
    private Node2D _inArea;

    // Setters
    private Area2D _interactionArea;
    public float MovementSpeed { get; private set; } = 100f;

    // Player Properties
    public override void _Ready()
    {
        _interactionArea = GetNode<Area2D>("InteractionArea");
        _inArea = null;

        // Subscribe to events
        _interactionArea.AreaEntered += InteractionAreaOnAreaEntered;
        _interactionArea.AreaExited += InteractionAreaOnAreaExited;
    }


    public override void _ExitTree()
    {
        // Interaction Area; Area Entered/Exited
        _interactionArea.AreaEntered -= InteractionAreaOnAreaEntered;
        _interactionArea.AreaExited -= InteractionAreaOnAreaExited;
    }

    private void InteractionAreaOnAreaExited(Area2D area)
    {
        if (!area.GetParent().IsInGroup("interactable")) return;
       // Logger.Log($"Left {area.GetParent().Name} {area.Name}!", Name);
        _inArea = null;
    }

    private void InteractionAreaOnAreaEntered(Area2D area)
    {
        if (!area.GetParent().IsInGroup("interactable")) return;
     //   Logger.Log($"Entered {area.GetParent().Name}{area.Name}", Name);
        _inArea = area;
    }

    private void BodyMovement(Vector2 newVelocity)
    {
        Velocity = newVelocity;
        MoveAndSlide();
    }
}