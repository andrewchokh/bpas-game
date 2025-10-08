using Godot;
using System;

public partial class Room : Node2D
{
    [Export]
    public RoomData Data;
    
    [Export]
    public Marker2D[] Waypoints;

    public Marker2D SelectRandomWaypoint()
    {
        return Waypoints[GD.Randi() % Waypoints.Length];
    }
}
