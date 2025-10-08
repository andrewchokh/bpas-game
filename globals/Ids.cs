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
        Entrance = 1,
        Battle = 2,
        Tunnel = 3,
        Boss = 4,
        Treasure = 5
    }
}
