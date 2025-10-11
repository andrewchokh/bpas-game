using Godot;
using System;

using static Ids;

public partial class Waypoint : Area2D
{
    [Export]
    public WaypointId Id { get; set; }

    [Signal]
    public delegate void WaypointEnteredEventHandler(Area2D self, Area2D other);

    public override void _Ready()
    {
        AreaEntered += (area) => EmitSignal(SignalName.WaypointEntered, this, area);
    }

    public Vector2 GetDirection()
    {
        return Id switch
        {
            WaypointId.NorthWaypoint => new Vector2(0, -1),
            WaypointId.SouthWaypoint => new Vector2(0, 1),
            WaypointId.EastWaypoint => new Vector2(1, 0),
            WaypointId.WestWaypoint => new Vector2(-1, 0),
            _ => Vector2.Zero
        };
    }

    public WaypointId GetOppositeWaypointId()
    {
        return Id switch
        {
            WaypointId.NorthWaypoint => WaypointId.SouthWaypoint,
            WaypointId.SouthWaypoint => WaypointId.NorthWaypoint,
            WaypointId.EastWaypoint => WaypointId.WestWaypoint,
            WaypointId.WestWaypoint => WaypointId.EastWaypoint,
            _ => Id
        };
    }
}
