using Godot;
using Godot.Collections;
using System;

using static Ids;

[GlobalClass] // allows you to make .tres from it in the editor
public partial class LevelDatabase : Resource
{
    [Export] public PackedScene[] LevelScenes { get; set; } = [];
}
