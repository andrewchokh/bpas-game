using Godot;
using Godot.Collections;
using System;

using static Ids;

[GlobalClass]
public partial class LevelGenerationSettings : Resource
{
    [Export]
    public int MaxTries { get; set; } = 1000;
    [Export]
    public Dictionary<RoomId, int> RoomCount = new()
    {
        { RoomId.Entrance, 1 },
        { RoomId.Battle, 2 },
        { RoomId.Boss, 0 },
        { RoomId.Treasure, 0 }
    };
    [Export]
    public Vector2 LayoutSize = new(12, 9);
}
