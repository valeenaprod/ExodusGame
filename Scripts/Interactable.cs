using Godot;

namespace Bunkify.Scripts;

public partial class Interactable : Node2D
{
    public bool IsInteractable { get; private set; } = true;
    public string ObjectName { get; protected set; }
    
    public virtual void Interact()
    {
        GD.Print("Interacting with base object.");
    }
}