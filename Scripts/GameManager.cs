using Godot;
using Player = ExodusGame.Scripts.PlayerScripts.Player;

namespace ExodusGame.Scripts;

public partial class GameManager : Node
{
    #region TopLevel

    private Label _foodSupplyLabel;
    private Label _powerLevelLabel;
    private Label _waterSupplyLabel;
    public PackedScene LineEditScene;
    public static GameManager Instance { get; private set; }
    
    #region PowerLevelProperties

    public float PowerLevel { get; private set; } = 100f; // Current power level
    public float MaxPowerLevel { get; private set; } = 100f; // Maximum power storage
    public float PowerConsumptionRate { get; private set; } = 0.5f; // Power used per second

    #endregion

    #region FoodSupplyProperties

    public float FoodSupply { get; private set; } = 50;
    public float MaxFood { get; private set; } = 50;
    public float FoodConsumption { get; private set; }

    #endregion

    #region WaterSupplyProperties

    public float WaterSupply { get; private set; } = 50;
    public float MaxWater { get; private set; } = 50;
    public float WaterConsumption { get; private set; }

    #endregion
    
    [Signal]
    public delegate void PlayerMenuEventHandler(bool value);

    public bool PlayerInMenu { get; private set; }
    
    #endregion

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
        _waterSupplyLabel = GetNode<Label>("%WS_Value");

        //

        PlayerMenu += value => PlayerInMenu = value;
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
        _waterSupplyLabel.Text = FoodSupply.ToString();
    }

    public void LoadGame()
    {
        var currentRoot = GetTree().Root;
        // Load and instantiate the player
        var playerScene = GD.Load<PackedScene>("res://Player.tscn");
        var playerInstance = playerScene.Instantiate<Player>();
        LineEditScene = GD.Load<PackedScene>("res://Scenes/LineEdit.tscn");
        playerInstance.Position = new Vector2(144, 112);
        currentRoot?.CallDeferred("add_child", playerInstance);
    }

    #region HUD

    private enum HudItem
    {
        PowerLevel,
        FoodSupply,
        WaterSupply
    }

    #endregion



    #region Resource Management

    #region PowerManagement

    public void DecreasePower(float amount)
    {
        PowerLevel += amount;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, MaxPowerLevel);
        // Logger.GameLog($"PowerLevel has decreased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreasePower(float amount)
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
            Logger.Log("Food Supply cannot be more than 50!", Logger.LogLevel.Error);
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