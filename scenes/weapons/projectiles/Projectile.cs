using Godot;
using System;

public abstract partial class Projectile : Area2D
{
    [Export]
    public float Speed;

    public int Damage;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent hitbox)
        {
            GD.Print($"Projectile hit: {hitbox.Entity.Name}");
            hitbox.HealthComponent.TakeDamage(Damage);
            QueueFree();
        }
    }
}