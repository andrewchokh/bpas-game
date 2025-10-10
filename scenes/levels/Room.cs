using Godot;
using System;

public partial class Room : Node2D
{
    [Export]
    public RoomData Data;
    
    [Export]
    public Area2D[] Waypoints;

    public Area2D SelectRandomWaypoint()
    {
        return Waypoints[GD.Randi() % Waypoints.Length];
    }
}
