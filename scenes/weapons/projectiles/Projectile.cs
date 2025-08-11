using Godot;
using System;

public abstract partial class Projectile : Area2D
{
    [Export]
    public float Speed;

    public int Damage { get; private set; }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }
}