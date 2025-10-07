using Godot;
using System;

[GlobalClass]
public partial class WeaponRangedBehavior : WeaponBehavior
{
    [Export]
    public PackedScene ProjectileScene;

    public override void Attack(Weapon weapon, Marker2D weaponPoint, Vector2 mousePosition)
    {
        var direction = (mousePosition - weaponPoint.GlobalPosition).Normalized();
        float projectileRotation = direction.Angle();
        var projectile = ProjectileScene.Instantiate<Projectile>();
        weaponPoint.GetTree().Root.AddChild(projectile);

        projectile.Damage = weapon.Damage;
        projectile.GlobalPosition = weaponPoint.GlobalPosition;
        projectile.Rotation = projectileRotation;
    }
}
