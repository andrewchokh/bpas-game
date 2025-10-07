using Godot;
using System.Collections.Generic;

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

public partial class EntityList : Node
{
    public static EntityList Instance { get; private set; }

    public readonly Dictionary<PlayerCharacterId, PackedScene> PlayerScenes = new()
    {

    };
    public readonly Dictionary<EnemyCharacterId, PackedScene> EnemyScenes = new()
    {

    };
    public readonly Dictionary<WeaponId, PackedScene> WeaponScenes = new()
    {
        { WeaponId.ArcaneStaff, GD.Load<PackedScene>("res://scenes/weapons/impl/ArcaneStaff.tscn") },
        { WeaponId.Dagger, GD.Load<PackedScene>("res://scenes/weapons/impl/Dagger.tscn") },
        { WeaponId.Spear, GD.Load<PackedScene>("res://scenes/weapons/impl/Spear.tscn") }
    };
    public readonly Dictionary<WeaponId, PackedScene> PickUpScenes = new()
    {
        { WeaponId.ArcaneStaff, GD.Load<PackedScene>("res://scenes/pickups/impl/ArcaneStaffPickUp.tscn") },
        { WeaponId.Spear, GD.Load<PackedScene>("res://scenes/pickups/impl/SpearPickUp.tscn") }
    };

    public override void _Ready()
    {
        Instance = this;
    }

    public PickUp GetPickUpByWeaponId(WeaponId weaponId)
    {
        foreach (var key in PickUpScenes.Keys)
        {
            if (key == weaponId)
                return PickUpScenes[key].Instantiate<PickUp>();
        }

        GD.PushError($"No PickUp found for WeaponId: {weaponId}");
        return null;
    }
}
