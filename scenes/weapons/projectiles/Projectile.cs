using Godot;
using System;

public partial class Projectile : Node
{
    private int _damage;

    public void SetDamage(int Damage)
    {
        _damage = Damage;
    }
}