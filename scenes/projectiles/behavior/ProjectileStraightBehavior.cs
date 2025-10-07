using Godot;
using System;

[GlobalClass]
public partial class ProjectileStraightBehavior : ProjectileBehavior
{
    public override void Process(Projectile projectile, double delta)
    {
        projectile.Position += projectile.Transform.X * projectile.Speed * (float)delta;
    }

    public override void OnHit(Projectile projectile, Area2D area)
    {
        projectile.QueueFree();
    }
}
