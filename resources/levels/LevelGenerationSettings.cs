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
    public Dictionary<RoomId, int> SpecialRooms = new()
    {
        { RoomId.Treasure, 2 }
    };
    [Export]
    public Vector2 LayoutSize = new(12, 9);
}
