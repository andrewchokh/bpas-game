using Godot;
using System;

/// <summary>
/// Contains utility functions and helpers.
/// </summary>
public partial class Utils : Node
{
    public static Func<object> BuildMethod<T>(Func<T> func)
    {
        return () => func();
    }

    public static Player GetFirstPlayer(SceneTree root)
    {
        var players = root.GetNodesInGroup("Players");

        if (players.Count > 0 && players[0] is Player FirstPlayer)
            return FirstPlayer;

        return null;
    }

    public void RemoveAllChildren(Node Node)
    {
        if (Node == null)
            return;
        foreach (var Child in Node.GetChildren())
            Child.QueueFree();
    }
}