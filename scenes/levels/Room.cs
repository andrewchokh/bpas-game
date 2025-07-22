using Godot;
using System;

public abstract partial class Room : Node2D
{
    [Export]
    public Marker2D[] WayPoints;
}
