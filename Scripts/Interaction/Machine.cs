﻿using ExodusGame.Scripts.Utils;

<<<<<<< HEAD:Scripts/Interaction/Machine.cs
namespace ExodusGame.Scripts.Interaction;

public partial class PoweredMachine : Interactable
=======
namespace ExodusGame.Scripts.Machine
>>>>>>> master:Scripts/Machine/Machine.cs
{
    public partial class PoweredMachine : Interactable
    {
<<<<<<< HEAD:Scripts/Interaction/Machine.cs
        IsActive = false;
        Logger.Log($"{ObjectName} has been turned off!");
    }
=======
        public bool UsePower { get; protected set; }
>>>>>>> master:Scripts/Machine/Machine.cs

        public int PowerLevel { get; private set; }

        public bool IsPowered { get; protected set; }

        public bool IsActive { get; protected set; }
        public int PowerConsumption { get; protected set; }
        public int Noise { get; protected set; }
        public int Health { get; protected set; }

        public virtual void Shutdown()
        {
<<<<<<< HEAD:Scripts/Interaction/Machine.cs
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
=======
            IsActive = false;
            Logger.GameLog($"{ObjectName} has been turned off!");
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
            if (GameManager.Instance.PowerLevel < PowerConsumption)
            {
                Logger.GameLog($"Cannot start {ObjectName}. Not enough power.");
                return;
            }

            IsActive = true;
            Logger.GameLog($"{ObjectName} has been turned on!");
        }
>>>>>>> master:Scripts/Machine/Machine.cs
    }
}