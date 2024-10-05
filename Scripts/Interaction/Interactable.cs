using Godot;

namespace ExodusGame.Scripts.Interaction;

public partial class Interactable : Node2D
{
    [Signal] public delegate void OnInteractEventHandler();
    public bool IsInteractable { get; private set; } = true;
    public string ObjectName { get; protected set; }

    public override void _Ready()
    {
        OnInteract += Interact;
    }
    public virtual void Interact()
    {
        GD.Print("Interacting with base object.");
    }
}