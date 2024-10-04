using ExodusGame.Scripts;

namespace ExodusGame.Scripts.Machine
{
    public partial class WaterPurifier : PoweredMachine
    {
        public override void _Ready()
        {
            UsePower = true;
            PowerConsumption = 20;
            ObjectName = "Water Purifier";
        }

        public override void _Process(double delta)
        {
            if (!IsActive) return;
            switch (GameManager.Instance.PowerLevel >= PowerConsumption)
            {
                case true:
                    GameManager.Instance.DecreasePower((int)(PowerConsumption * delta));
                    break;
                case false:
                    Shutdown();
                    break;
            }
        }
    }
}