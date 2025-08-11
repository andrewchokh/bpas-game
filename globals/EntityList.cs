using Godot;
using System.Collections.Generic;

public partial class EntityList : Node
{
    public static EntityList Instance { get; private set; }

    public enum PlayerSceneId
    {
        Bob
    }
    public enum EnemySceneId
    {
        Steve
    }
    public enum WeaponSceneId
    {
        ArcaneStaff,
        Dagger,
        Spear
    }
    public enum AbilitySceneId
    {
        AbilityAcceleration
    }
    public enum PickUpSceneId
    {
        ArcaneStaff,
        Dagger,
        Spear
    }

    public readonly Dictionary<PlayerSceneId, PackedScene> PlayerScenes = new()
    {
        { PlayerSceneId.Bob, GD.Load<PackedScene>("res://scenes/players/impl/Bob.tscn") },
    };
    public readonly Dictionary<EnemySceneId, PackedScene> EnemyScenes = new()
    {
        { EnemySceneId.Steve, GD.Load<PackedScene>("res://scenes/enemies/impl/Steve.tscn") },
    };
    public readonly Dictionary<WeaponSceneId, PackedScene> WeaponScenes = new()
    {
        { WeaponSceneId.ArcaneStaff, GD.Load<PackedScene>("res://scenes/weapons/impl/ArcaneStaff.tscn") },
        { WeaponSceneId.Dagger, GD.Load<PackedScene>("res://scenes/weapons/impl/Dagger.tscn") },
        { WeaponSceneId.Spear, GD.Load<PackedScene>("res://scenes/weapons/impl/Spear.tscn") },
    };
    public readonly Dictionary<AbilitySceneId, PackedScene> AbilityScenes = new()
    {
        { AbilitySceneId.AbilityAcceleration, GD.Load<PackedScene>("res://scenes/players/abilities/impl/AbilityAcceleration.tscn") },
    };
    public readonly Dictionary<PickUpSceneId, PackedScene> PickUpScenes = new()
    {
        
    };
    
    public override void _Ready()
    {
        Instance = this;
    }
}
