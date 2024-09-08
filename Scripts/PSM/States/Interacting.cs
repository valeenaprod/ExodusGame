﻿using Godot;

namespace ExodusGame.Scripts.PSM.States;

public partial class Interacting : PlayerState
{
    public override void HandleInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseEvent) return;
        var collider = GetObjectMouseClick(mouseEvent.Position);
        if (collider is not Interactable) return;
        if (!collider.HasSignal("OnInteract")) return;
        collider.EmitSignal("OnInteract");
    }
    
    private Node2D GetObjectMouseClick(Vector2 mousePosition)
    {
        // Create a new PhysicsPointQueryParameters2D object
        var pointQueryParameters = new PhysicsPointQueryParameters2D();
        // Set the Position of the Query to the Vector2 mousePosition
        pointQueryParameters.Position = mousePosition;

        // Perform the query using a direct space state
        var spaceState = PSM.Player.GetWorld2D().DirectSpaceState;
        var results = spaceState.IntersectPoint(pointQueryParameters);

        // Check if the results contains a collider

        foreach (var result in results)
            if (result.TryGetValue("collider", out var value))
            {
                var collider = (Node2D)value;
                Logger.Log(result.ToString(), Name);
                Logger.Log($"Collider found: {collider.Name}", Name);
                return collider;
            }
            else
            {
                Logger.LogError("No collider found at this point!", Name);
                Logger.Log(result.ToString(), Name);
                return null;
            }

        return null;
    }
}