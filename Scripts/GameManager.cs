using Bunkify.Scripts.PlayerScripts;
using Godot;

namespace Bunkify.Scripts;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    // Properties

    public int PowerLevel { get; private set; } = 100;
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
        if (PowerLevel - amount < 0)
        {
            Logger.LogError("PowerLevel cannot be less than zero!", Name);
            return;
        }

        PowerLevel -= amount;
        _updateHUD(HudItem.PowerLevel);
        Logger.GameLog($"PowerLevel has decreased!\n New PowerLevel: {PowerLevel}");
    }

    public void IncreasePower(int amount)
    {
        if (PowerLevel + amount > 100)
        {
            Logger.LogError("PowerLevel cannot be more than 100!", Name);
            return;
        }

        PowerLevel += amount;
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