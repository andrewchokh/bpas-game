using Godot;
using System;

public partial class Ids : Node
{
    public enum PlayerCharacterId
    {
        Bob
    }

    public enum EnemyCharacterId
    {
        Steve
    }

    public enum WeaponId
    {
        ArcaneStaff,
        Dagger,
        Spear
    }

    public enum LocationId : int
    {
        Forest = 1,
        Dungeon = 2
    }

    public enum RoomId : int
    {
        Empty = 1,
        Entrance = 2,
        Battle = 3,
        Tunnel = 4,
        Treasure = 5,
        Boss = 6
    }

    public enum WaypointId
    {
        NorthWaypoint,
        SouthWaypoint,
        EastWaypoint,
        WestWaypoint,
    }
}
