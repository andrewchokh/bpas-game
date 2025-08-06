using Godot;
using System;
using System.Collections.Generic;

public partial class Levels : Node
{
    public enum RoomType : int
    {
        ENTRANCE = 1,
        BATTLE = 2,
        TUNNEL = 3,
        BOSS = 4,
        TREASURE = 5
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
                { RoomType.ENTRANCE,
                    [
                        GD.Load<PackedScene>("res://scenes/levels/forest/LeftEntrance.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/RightEntrance.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/UpEntrance.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/DownEntrance.tscn"),
                    ]
                },
                { RoomType.BATTLE,
                    [
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle1.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle2.tscn"),
                    ] 
                },
                { RoomType.TUNNEL,
                    [
                        GD.Load<PackedScene>("res://scenes/levels/forest/Tunnel1.tscn")
                    ] 
                },
            }
        },
        { LocationType.DUNGEON, new Dictionary<RoomType, PackedScene[]>() }
    };

    public override void _Ready()
    {
        Instance = this;
    }
}
