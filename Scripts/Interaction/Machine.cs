using ExodusGame.Scripts.Utils;

namespace ExodusGame.Scripts.Interaction;

public partial class PoweredMachine : Interactable
{
    public bool UsePower { get; protected set; }

    public int PowerLevel { get; private set; }

    public bool IsPowered { get; protected set; }

    public bool IsActive { get; protected set; }
    public int PowerConsumption { get; protected set; }
    public int Noise { get; protected set; }
    public int Health { get; protected set; }

    public virtual void Shutdown()
    {
        IsActive = false;
        Logger.Log($"{ObjectName} has been turned off!");
    }

    public override void Interact()
    {
        switch (IsActive)
        {
            case true:
                Shutdown();
                break;
            case false:
                StartMachine();
                break;
        }
    }

    private void StartMachine()
    {
        if (Systems.GameManager.Instance.PowerLevel < PowerConsumption)
        {
            Logger.Log($"Cannot start {ObjectName}. Not enough power.");
            return;
        }

        IsActive = true;
        Logger.Log($"{ObjectName} has been turned on!");
    }
}