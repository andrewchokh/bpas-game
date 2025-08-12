using Godot;
using System;

public abstract partial class Room : Node2D
{
    [Export]
    public Marker2D[] Waypoints;

    public Marker2D SelectRandomWaypoint()
    {
        return Waypoints[GD.RandRange(0, Waypoints.Length - 1)];
    }
}
