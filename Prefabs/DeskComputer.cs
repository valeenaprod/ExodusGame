using ExodusGame.Scripts.Interaction;

namespace ExodusGame.Prefabs;

public partial class DeskComputer : PoweredMachine
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