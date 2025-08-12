using Godot;
using System.Collections.Generic;

public partial class LevelsData : Node
{
    public enum RoomType : int
    {
        ENTRANCE = 1,
        BATTLE = 2,
        TUNNEL = 3,
        BOSS = 4,
        TREASURE = 5
    }

    public enum LocationId
    {
        FOREST,
        DUNGEON
    }

    public static LevelsData Instance { get; private set; }

    public readonly Dictionary<LocationId, Dictionary<RoomType, PackedScene[]>> LevelScenes = new()
    {
        { LocationId.FOREST, new Dictionary<RoomType, PackedScene[]>()
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
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle1.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle2.tscn"),
                    ]
                },
                { RoomType.BOSS,
                    [
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle1.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle2.tscn"),
                    ]
                },
                { RoomType.TREASURE,
                    [
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle1.tscn"),
                        GD.Load<PackedScene>("res://scenes/levels/forest/Battle2.tscn"),
                    ]
                },
                
            }
        },
        { LocationId.DUNGEON, new Dictionary<RoomType, PackedScene[]>() }
    };

    public override void _Ready()
    {
        Instance = this;
    }
}
