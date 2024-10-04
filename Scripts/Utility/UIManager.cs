using Godot;

namespace ExodusGame.Scripts.Utility;

public partial class UIManager : Node
{
    private Control _currentMenu;
    public static UIManager Instance { get; private set; }

    public bool IsMenuOpen { get; private set; }

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
            SetProcess(true);
        }
        else
        {
            QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        if (!Input.IsActionPressed("ui_cancel")) return;
        if (_currentMenu == null) return;
        CloseCurrentMenu();
    }

    public void RegisterMenu(Control menu)
    {
        _currentMenu ??= menu;
        IsMenuOpen = true;
    }

    public void DeregisterMenu(Control menu)
    {
        if (_currentMenu == menu)
        {
            _currentMenu = null;
            IsMenuOpen = false;
        }
    }

    public void CloseCurrentMenu()
    {
        if (_currentMenu.HasMethod("CloseMenu"))
            _currentMenu?.Call("CloseMenu");
        else
            _currentMenu?.Hide();
    }
}