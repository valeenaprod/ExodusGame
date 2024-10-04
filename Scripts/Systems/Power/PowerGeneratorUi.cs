using System.Threading.Tasks;
using ExodusGame.Scripts.Utility;
using Godot;
using UIManager = ExodusGame.Scripts.Utility.UIManager;

namespace ExodusGame.Scripts.Systems.Power;

public partial class PowerGeneratorUi : MarginContainer
{
    private Button _addFuel;
    private PanelContainer _mainUI;
    private PowerGenerator _powerGenerator;
    private Button _togglePower;

    public override void _Ready()
    {
        _powerGenerator = GetParent() as PowerGenerator;

        _mainUI = GetNode<PanelContainer>("MainUI");

        _addFuel = GetNode<Button>("%AddFuel");
        _addFuel.Pressed += AddFuelButton;
        _togglePower = GetNode<Button>("%PowerSwitch");
        _togglePower.Pressed += TogglePowerButton;
    }

    private void AddFuelButton()
    {
        _ = EnterFuelAsync();
    }

    private void TogglePowerButton()
    {
        Logger.Log("Toggled", Logger.LogLevel.Debug);
        _powerGenerator.SwitchGenerator();
    }

    public void CloseMenu()
    {
        UIManager.Instance.DeregisterMenu(this);
        QueueFree();
    }

    private async Task EnterFuelAsync()
    {
        _mainUI.Hide();
        var lineEdit = CreateLineEdit();
        AddChild(lineEdit);
        while (true)
        {
            await ToSignal(lineEdit, "text_submitted");
            if (float.TryParse(lineEdit.Text, out var value))
            {
                if (_powerGenerator.AddFuel(value)) break;
                lineEdit.Text = string.Empty;
                lineEdit.PlaceholderText = $"The generator has no room for {value} Fuel.";
            }
            else
            {
                lineEdit.Text = string.Empty;
                lineEdit.PlaceholderText = "Please enter a valid number.";
            }
        }

        lineEdit.QueueFree();
    }

    private LineEdit CreateLineEdit()
    {
        var lineEdit = new LineEdit
        {
            PlaceholderText = "Fuel Amount",
            Alignment = HorizontalAlignment.Center,
            TopLevel = true
        };

        return lineEdit;
    }

    public override void _ExitTree()
    {
        UIManager.Instance.DeregisterMenu(this);
    }
}