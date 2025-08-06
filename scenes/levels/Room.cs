using Godot;
using System;

public abstract partial class Room : Node2D
{
    [Export]
    public Marker2D[] WayPoints;

    [ExportGroup("Directions")]
    [Export]
    public bool IsRightDirection;
    [ExportGroup("Directions")]
    [Export]
    public bool IsLeftDirection;
    [ExportGroup("Directions")]
    [Export]
    public bool IsUpDirection;
    [ExportGroup("Directions")]
    [Export]
    public bool IsDownDirection;
}
