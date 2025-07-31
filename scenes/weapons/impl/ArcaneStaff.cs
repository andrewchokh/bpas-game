using Godot;
using System;
using System.Numerics;

public partial class ArcaneStaff : Weapon
{
    [Export]
    public PackedScene ArcaneStaffProjectile;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void OnWeaponUsed()
    {
        base.OnWeaponUsed();

        GD.Print("Damage = " + Damage);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_weapon"))
        {
            if (ArcaneStaffProjectile != null)
            {
                Godot.Vector2 MousePosition = GetGlobalMousePosition();
                Godot.Vector2 ArcaneStaffPosition = GlobalPosition;

                Godot.Vector2 Direction = (MousePosition - ArcaneStaffPosition).Normalized();

                float BulletRotation = Direction.Angle();

                var BulletInstance = ArcaneStaffProjectile.Instantiate<Node2D>();
                GetTree().Root.AddChild(BulletInstance);
                BulletInstance.GlobalPosition = GlobalPosition;
                BulletInstance.Rotation = BulletRotation;
            }
            else
                GD.PrintErr("ArcaneStaffProjectile is not assigned!");
        }
    }
}