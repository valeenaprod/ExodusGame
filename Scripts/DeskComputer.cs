using System;
using Bunkify.Scripts;

public partial class DeskComputer : Machine
{

    public override void _Ready()
    {
        ObjectName = "Bunker Management Computer";
    }
    
    public override void Interact()
    {
        base.Interact();
    }
}