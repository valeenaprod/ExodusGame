using ExodusGame.Scripts.Machine;

namespace ExodusGame.Scripts;

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