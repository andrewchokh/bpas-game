using Godot;
using System;

public partial class Utils : Node
{
    public static Utils Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public static Func<object> BuildMethod<T>(Func<T> func)
    {
        return () => func();
    }

    public static Player GetFirstPlayer()
    {
        var Players = Instance.GetTree().GetNodesInGroup("Players");

        if (Players.Count > 0 && Players[0] is Player firstPlayer)
            return firstPlayer;

        return null;
    }
}