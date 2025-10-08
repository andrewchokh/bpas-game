using Godot;

/// <summary>
/// Stores all constant values used throughout the game.
/// </summary>
public partial class Constants : Node
{
    public const int DefaultTileSize = 16; // Size of each tile in pixels
    public const int RoomMaxSizeX = 64;
    public const int RoomMaxSizeY = 64;

    public const string NorthWaypointName = "NorthWaypoint";
    public const string WestWaypointName = "WestWaypoint";
    public const string SouthWaypointName = "SouthWaypoint";
    public const string EastWaypointName = "EastWaypoint";
}
