using ExodusGame.Scripts.PlayerScripts;
using Godot;
using Godot.Collections;

namespace ExodusGame.Scripts.PSM;

public partial class PlayerStateMachine : Node
{
    private NodePath _initialState;

    public Player Player { get; private set; }

    private Dictionary<string, PlayerState> _states;
    public PlayerState CurrentState { get; private set; }
    
    public override void _Ready()
    {
        _initialState = GetNode("%Idle").GetPath();

        _states = new Dictionary<string, PlayerState>();
        foreach (var node in GetNode<PlayerState>("PlayerState").GetChildren())
            if (node is PlayerState s)
            {
                Logger.Log($"PlayerState Loaded: {node.Name}", Name);
                _states[node.Name] = s;
                s.PSM = this;
                s._Ready();
                s.Exit(); // reset all states
            }

        CurrentState = GetNode<PlayerState>(_initialState);
        CurrentState.Enter();

        //

        Player = GetParent<Player>();
    }

    public override void _Process(double delta)
    {
        CurrentState.Update((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState.PhysicsUpdate((float)delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        CurrentState.HandleInput(@event);
    }

    public void TransitionTo(string key)
    {
        if (!_states.ContainsKey(key) || CurrentState == _states[key])
            return;
        CurrentState.Exit();
        CurrentState = _states[key];
        CurrentState.Enter();
    }

    public string GetState()
    {
        return CurrentState.ToString();
    }
}