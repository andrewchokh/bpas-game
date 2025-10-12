using Godot;
using Godot.Collections;
using System;

using static Ids;

[GlobalClass] // allows you to make .tres from it in the editor
public partial class EntityDatabase : Resource
{
    [Export] public Dictionary<PlayerCharacterId, PackedScene> PlayerScenes { get; set; } = new();
    [Export] public Dictionary<EnemyCharacterId, PackedScene> EnemyScenes { get; set; } = new();
    [Export] public Dictionary<WeaponId, PackedScene> WeaponScenes { get; set; } = new();
    [Export] public Dictionary<WeaponId, PackedScene> PickUpScenes { get; set; } = new();
}