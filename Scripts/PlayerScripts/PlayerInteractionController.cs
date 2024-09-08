using Godot;

namespace Bunkify.Scripts.PlayerScripts;

public partial class PlayerInteractionController : Node2D
{
    private CharacterBody2D _body;
    private PlayerController _controller;

    public override void _Ready()
    {
        _controller = GetParent<PlayerController>();
        _body = _controller.Body;
    }

    // Todo: method called by PlayerController to handle interaction
}