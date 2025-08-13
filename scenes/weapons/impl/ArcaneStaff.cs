using Godot;

public partial class ArcaneStaff : Weapon
{
    [Export]
    public PackedScene ArcaneStaffProjectile;

    public override void OnWeaponUsed()
    {
        var mousePosition = GetGlobalMousePosition();
        var arcaneStaffPosition = GlobalPosition;

        var direction = (mousePosition - arcaneStaffPosition).Normalized();

        float bulletRotation = direction.Angle();

        var BulletInstance = ArcaneStaffProjectile.Instantiate<Projectile>();
        GetTree().Root.AddChild(BulletInstance);
        BulletInstance.Damage = Damage;
        BulletInstance.GlobalPosition = GlobalPosition;
        BulletInstance.Rotation = bulletRotation;
    }
}