using Bunkify.Scripts.PlayerScripts;
using Godot;

namespace Bunkify.Scripts;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    // Properties

    public int PowerLevel { get; private set; } = 100; // Current power level

    public int MaxPowerLevel { get; private set; } = 100; // Maximum power storage
    public int PowerConsumptionRate { get; private set; } = 10; // Power used per second
    public int FoodSupply { get; private set; } = 50;

    public override void _Ready()
    {
        if (Instance == null)

        {
            Instance = this;
            // Make sure this Node doesn't get removed when scenes changes
            SetProcess(true); // Keeps the GameManager active
        }
        else
        {
            QueueFree(); // Remove any duplicate instance
        }
    }

    public override void _Process(double delta)
    {
        // Decrease power level based on consumption rate
        PowerLevel -= PowerConsumptionRate * (int)delta;
        
        // Clamp the power value to avoir negative pwoer
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);

        // Check if power runs out
        if (PowerLevel <= 0)
        {
            Logger.GameLog("Power is out! Systems are shutting down...");
            //TODO: Add logic to handle power shut down
        }
    }

    public void LoadGame()
    {
        var currentRoot = GetTree().Root;
        // Load and instantiate the player
        var playerScene = GD.Load<PackedScene>("res://Player.tscn");
        var playerInstance = playerScene.Instantiate<Player>();
        playerInstance.Position = new Vector2(144, 112);
        currentRoot?.CallDeferred("add_child", playerInstance);

        // Set the value of the HUD
        _updateHUD(HudItem.FoodSupply);
        _updateHUD(HudItem.PowerLevel);
    }

    #region HUD

    private void _updateHUD(HudItem item)
    {
        var foodSupply = GetNode<Label>("%FS_Value");
        var powerLevel = GetNode<Label>("%PL_Value");
        switch (item)
        {
            case HudItem.FoodSupply:
                foodSupply.Text = FoodSupply.ToString();
                break;
            case HudItem.PowerLevel:
                powerLevel.Text = PowerLevel.ToString();
                break;
            default:
                return;
        }
    }

    private enum HudItem
    {
        PowerLevel,
        FoodSupply
    }

    #endregion

    #region Resource Management

    #region PowerManagement

    public void DecreasePower(int amount)
    {
        PowerLevel += amount;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);
        _updateHUD(HudItem.PowerLevel);
        Logger.GameLog($"PowerLevel has decreased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreasePower(int amount)
    {
        PowerLevel -= amount;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);
        _updateHUD(HudItem.PowerLevel);
        Logger.GameLog($"PowerLevel has increased!\n New PowerLevel: {PowerLevel}");
    }

    #endregion

    #region FoodManagement

    public void IncreaseFoodSupply(int amount)
    {
        if (FoodSupply + amount > 50)
        {
            Logger.LogError("Food Supply cannot be more than 50!", Name);
            return;
        }

        FoodSupply += amount;
        _updateHUD(HudItem.FoodSupply);
    }

    public void DecreaseFoodSupply(int amount)
    {
        if (FoodSupply - amount < 0)
        {
            Logger.LogError("Food Supply cannot less than 0!", Name);
            return;
        }

        FoodSupply -= amount;
        _updateHUD(HudItem.FoodSupply);
    }

    #endregion

    #endregion
}