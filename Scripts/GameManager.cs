using ExodusGame.Scripts;
using Godot;
using Player = ExodusGame.Scripts.PlayerScripts.Player;

namespace ExodusGame.Scripts;

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
        if (PowerLevel <= 0) Logger.GameLog("Power is out! Systems are shutting down...");
        //TODO: Add logic to handle power shut down
       _foodSupplyLabel.Text = FoodSupply.ToString();
       _powerLevelLabel.Text = PowerLevel.ToString();
    }

    public void LoadGame()
    {
        var currentRoot = GetTree().Root;
        // Load and instantiate the player
        var playerScene = GD.Load<PackedScene>("res://Player.tscn");
        var playerInstance = playerScene.Instantiate<Player>();
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
        Logger.GameLog($"PowerLevel has decreased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreasePower(int amount)
    {
        PowerLevel -= amount;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);
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
    }

    public void DecreaseFoodSupply(int amount)
    {
        if (FoodSupply - amount < 0)
        {
            Logger.LogError("Food Supply cannot less than 0!", Name);
            return;
        }

        FoodSupply -= amount;
    }

    #endregion

    #endregion
}