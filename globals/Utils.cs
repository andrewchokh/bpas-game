using Godot;
using System;

/// <summary>
/// Utility singleton providing helper methods for the game.
/// </summary>
public partial class Utils : Node
{
    /// <summary>
    /// Singleton instance of the Utils class.
    /// </summary>
    public static Utils Instance { get; private set; }

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Initializes the singleton instance.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }

    /// <summary>
    /// Wraps a typed function into a generic Func<object> delegate.
    /// Useful for assigning methods to delegates expecting Func<object>.
    /// </summary>
    /// <typeparam name="T">Return type of the function.</typeparam>
    /// <param name="func">Function to wrap.</param>
    /// <returns>A Func<object> delegate that calls the provided function.</returns>
    public static Func<object> BuildMethod<T>(Func<T> func)
    {
        return () => func();
    }

    /// <summary>
    /// Finds and returns the first player node in the "Players" group.
    /// Returns null if no player is found.
    /// </summary>
    /// <returns>The first Player instance found in the scene tree, or null.</returns>
    public static Player GetFirstPlayer()
    {
        var Players = Instance.GetTree().GetNodesInGroup("Players");

        if (Players.Count > 0 && Players[0] is Player FirstPlayer)
            return FirstPlayer;

        return null;
    }
}
