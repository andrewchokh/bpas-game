using Godot;
using System;

[GlobalClass]
public abstract partial class ProjectileBehavior : Resource
{
    public abstract void Process(Projectile projectile, double delta);
    public abstract void OnHit(Projectile projectile, Area2D area);
}
