using ExodusGame.Scripts;
using ExodusGame.Scripts.Player;
using Godot;

namespace ExodusGame.Systems;

public partial class GameManager : Node
{
    private Label _foodSupplyLabel;
    private Label _powerLevelLabel;
    public static GameManager Instance { get; private set; }

    // Properties

    public float PowerLevel { get; private set; } = 100; // Current power level

    public float MaxPowerLevel { get; private set; } = 100; // Maximum power storage
    public float PowerConsumptionRate { get; private set; } = 0.5f; // Power used per second
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

        _foodSupplyLabel = GetNode<Label>("%FS_Value");
        _powerLevelLabel = GetNode<Label>("%PL_Value");
    }

    public override void _Process(double delta)
    {
        // Decrease power level based on consumption rate
        PowerLevel -= PowerConsumptionRate * (float)delta;
        // Clamp the power value to avoir negative power
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);

        // Check if power runs out
        if (PowerLevel <= 0) Logger.Log("Power is out! Systems are shutting down...");
        //TODO: Add logic to handle power shut down
       _foodSupplyLabel.Text = FoodSupply.ToString();
       _powerLevelLabel.Text = PowerLevel.ToString();
    }

    public void LoadGame()
    {
        var currentRoot = GetTree().Root;
        // Load and instantiate the player
        var playerScene = GD.Load<PackedScene>("res://Player.tscn");
        var playerInstance = playerScene.Instantiate<PlayerController>();
        playerInstance.Position = new Vector2(144, 112);
        currentRoot?.CallDeferred("add_child", playerInstance);
    }

    #region HUD

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
        Logger.Log($"PowerLevel has decreased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreasePower(int amount)
    {
        PowerLevel -= amount;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);
        //  Logger.GameLog($"PowerLevel has increased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreaseConsumptionRate(float amount)
    {
        PowerConsumptionRate += amount;
    }

    public void DecreaseConsumptionRate(float amount)
    {
        PowerConsumptionRate -= amount;
    }

    #endregion

    #region FoodManagement

    public void IncreaseFoodSupply(int amount)
    {
        if (FoodSupply + amount > 50)
        {
            Logger.Log("Food Supply cannot be more than 50!");
            return;
        }

        FoodSupply += amount;
    }

    public void DecreaseFoodSupply(int amount)
    {
        if (FoodSupply - amount < 0)
        {
            Logger.Log("Food Supply cannot be less than 0!", Logger.LogLevel.Error);
            return;
        }

        FoodSupply -= amount;
    }

    #endregion

    #endregion
}