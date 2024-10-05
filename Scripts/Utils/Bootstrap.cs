using Godot;

namespace ExodusGame.Scripts.Utils;

public partial class Bootstrap : Node
{
    private Node _sceneContainer;

    public override void _Ready()
    {
        _sceneContainer = GetNode("SceneContainer");
        Systems.GameManager.Instance.LoadGame();

        // Load default map scene
        SwitchScene("res://Scenes/MainGame/BunkerScene.tscn");
    }

    public void SwitchScene(string scenePath)
    {
        _sceneContainer.QueueFreeChildren();
        var newScene = GD.Load<PackedScene>(scenePath);
        var newSceneInstance = newScene?.Instantiate();
        _sceneContainer.AddChild(newSceneInstance);
    }
}

// Helper method to free all children in the container
public static class NodeExtensions
{
    public static void QueueFreeChildren(this Node node)
    {
        foreach (var child in node.GetChildren()) child.QueueFree();
    }
}