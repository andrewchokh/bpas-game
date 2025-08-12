using Godot;

public partial class ArcaneStaff : Weapon
{
    [Export]
    public PackedScene ArcaneStaffProjectile;

    public override void OnWeaponUsed()
    {
        Vector2 MousePosition = GetGlobalMousePosition();
        Vector2 ArcaneStaffPosition = GlobalPosition;

        Vector2 Direction = (MousePosition - ArcaneStaffPosition).Normalized();

        float BulletRotation = Direction.Angle();

        var BulletInstance = ArcaneStaffProjectile.Instantiate<Projectile>();
        GetTree().Root.AddChild(BulletInstance);
        BulletInstance.GlobalPosition = GlobalPosition;
        BulletInstance.Rotation = BulletRotation;
    }
}