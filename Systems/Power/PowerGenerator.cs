using ExodusGame.Scripts.Interaction;
using ExodusGame.Scripts.Utils;

<<<<<<< HEAD:Systems/Power/PowerGenerator.cs
namespace ExodusGame.Systems.Power;

public partial class PowerGenerator : Interactable
=======
namespace ExodusGame.Scripts.Machine
>>>>>>> master:Scripts/Machine/PowerGenerator.cs
{
    public partial class PowerGenerator : Interactable
    {
        private bool _isActive;
        public int PowerGeneratedPerSecond { get; private set; } = 50;

        public override void _Ready()
        {
<<<<<<< HEAD:Systems/Power/PowerGenerator.cs
            case true:
                _isActive = false;
                Logger.Log("Generator has been turned off!");
                break;
            case false:
                _isActive = true;
                Logger.Log("Generator has been turned on!");
                break;
=======
            ObjectName = "Power Generator";
        }

        public override void _Process(double delta)
        {
            if (!_isActive) return;
            // Increase the power of the generator
            GameManager.Instance.IncreasePower((int)(PowerGeneratedPerSecond * delta));
        }

        // Switch the generator to on or off
        public override void Interact()
        {
            SwitchGenerator();
        }

        private void SwitchGenerator()
        {
            switch (_isActive)
            {
                case true:
                    _isActive = false;
                    Logger.GameLog("Generator has been turned off!");
                    break;
                case false:
                    _isActive = true;
                    Logger.GameLog("Generator has been turned on!");
                    break;
            }
>>>>>>> master:Scripts/Machine/PowerGenerator.cs
        }
    }
}