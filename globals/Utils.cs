using Godot;
using System;

/// <summary>
/// Contains utility functions and helpers.
/// </summary>
public partial class Utils : Node
{
    public static Utils Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public Func<object> BuildMethod<T>(Func<T> func)
    {
        return () => func();
    }

    public Player GetFirstPlayer()
    {
        var players = Instance.GetTree().GetNodesInGroup("Players");

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