using Godot;

namespace Bunkify.Scripts;

public partial class Machine : Interactable
{
    public bool UsePower { get; protected set; }
    
    public int PowerLevel { get; private set; }
    
    public bool IsPowered { get; protected set; }
    public int PowerConsumption { get; protected set; }
    public int  Noise { get; protected set; }
    public int Health { get; protected set; }
}