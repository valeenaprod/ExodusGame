﻿using ExodusGame.Scripts.Utils;
using Godot;

namespace ExodusGame.Scripts.Player.StateMachines;

public partial class PlayerState : Node
{
    public PlayerStateMachine PSM;
    

    public virtual void Enter()
    {
        Logger.Log($"Entering State: {Name}");
    }

    public virtual void Exit()
    {
    }

#pragma warning disable CS0108, CS0114
    public virtual void Ready()
    {
    }
#pragma warning restore CS0108, CS0114
    public virtual void Update(double delta)
    {
    }

    public virtual void PhysicsUpdate(double delta)
    {
    }

    public virtual void HandleInput(InputEvent @event)
    {
    }
    
}