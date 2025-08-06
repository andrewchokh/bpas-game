using Godot;
using System;

public partial class Constants : Node
{
    /// <summary>
    /// Singleton instance of the EntityList.
    /// </summary>
    public static Constants Instance { get; private set; }

    public const int DEFAULT_TILE_SIZE = 16; // Size of each tile in pixels
    public const int ROOM_MAX_SIZE_X = 64;
    public const int ROOM_MAX_SIZE_Y = 64;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Initializes the singleton instance.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }
}
