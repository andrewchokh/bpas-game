using Godot;
using System;
using System.Collections.Generic;

public partial class Levels : Node
{
    public enum RoomType
    {
        ENTRANCE,
        BATTLE,
        TUNNEL
    }

    public enum LocationType
    {
        FOREST,
        DUNGEON
    }

    public static Levels Instance { get; private set; }

    public static readonly Dictionary<LocationType, Dictionary<RoomType, PackedScene[]>> LevelScenes = new()
    {
        { LocationType.FOREST, new Dictionary<RoomType, PackedScene[]>()
            {
                { RoomType.ENTRANCE, [GD.Load<PackedScene>("res://scenes/levels/forest/Entrance.tscn")] },
                { RoomType.BATTLE, [GD.Load<PackedScene>("res://scenes/levels/forest/Battle1.tscn")] },
                { RoomType.TUNNEL, [GD.Load<PackedScene>("res://scenes/levels/forest/Tunnel1.tscn")] },
            }
        },
        { LocationType.DUNGEON, new Dictionary<RoomType, PackedScene[]>() }
    };

    public override void _Ready()
    {
        Instance = this;
    }
}
