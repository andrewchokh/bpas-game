using Godot;
using System;

public partial class ArcaneStaffProjectile : Projectile
{
    public override void _Process(double delta)
    {
        Position += Transform.X * Speed * (float)delta;
    }
}