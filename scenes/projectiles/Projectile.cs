using Godot;
using System;

/// <summary>
/// Represents a projectile in the game.
/// </summary>
public partial class Projectile : Area2D
{
    [Export]
    public ProjectileBehavior Behavior;

    [ExportCategory("Properties")]
    [Export]
    public int Damage;
    [Export]
    public float Speed;
    
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public override void _Process(double delta)
    {
        Behavior.Process(this, delta);
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent hitbox)
        {
            GD.Print($"Projectile hit: {hitbox.Entity.Name}");
            hitbox.HealthComponent.TakeDamage(Damage);
            
            Behavior.OnHit(this, area);
        }
    }
}