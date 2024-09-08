using System;
using Godot;

namespace Bunkify.Scripts.PlayerScripts;

public partial class PlayerMovementController : Node2D
{
    private CharacterBody2D _body;
    private PlayerController _controller;

    public event Action<Vector2> OnMove;

    public override void _EnterTree()
    {
        _controller = GetParent<PlayerController>();
        _body = _controller.Body;
    }

    // Method called by PlayerController to handle movement
    public void Move(Vector2 direction)
    {
        var velocity = direction * 200;
        OnMove?.Invoke(velocity);
    }
}