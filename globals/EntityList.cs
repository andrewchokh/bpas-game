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

    public readonly Dictionary<PlayerSceneId, PackedScene> PlayerScenes = new()
    {

    };
    public readonly Dictionary<EnemySceneId, PackedScene> EnemyScenes = new()
    {

    };
    public readonly Dictionary<WeaponSceneId, PackedScene> WeaponScenes = new()
    {
        { WeaponSceneId.ArcaneStaff, GD.Load<PackedScene>("res://scenes/weapons/impl/ArcaneStaff.tscn") },
        { WeaponSceneId.Dagger, GD.Load<PackedScene>("res://scenes/weapons/impl/Dagger.tscn") },
        { WeaponSceneId.Spear, GD.Load<PackedScene>("res://scenes/weapons/impl/Spear.tscn") }
    };
    public readonly Dictionary<WeaponSceneId, PackedScene> PickUpScenes = new()
    {
        { WeaponSceneId.ArcaneStaff, GD.Load<PackedScene>("res://scenes/pickups/impl/ArcaneStaffPickUp.tscn") },
        { WeaponSceneId.Spear, GD.Load<PackedScene>("res://scenes/pickups/impl/SpearPickUp.tscn") }
    };

    public override void _Ready()
    {
        Instance = this;
    }
    
    public PickUp FindPickUpByWeaponSceneId(WeaponSceneId weaponSceneId)
    {
        foreach (var key in PickUpScenes.Keys)
        {
            if (key == weaponSceneId)
            {
                return PickUpScenes[key].Instantiate<PickUp>();
            }
        }
        
        GD.PushError($"No PickUp found for WeaponSceneId: {weaponSceneId}");
        return null;
    }
}
