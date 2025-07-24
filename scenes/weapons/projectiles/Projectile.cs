using Godot;
using System;

/// <summary>
/// Represents a projectile that can deal damage to entities.
/// </summary>
public partial class Projectile : Node
{
    /// <summary>
    /// The amount of damage this projectile will inflict.
    /// </summary>
    private int _damage;

    /// <summary>
    /// Sets the damage value of the projectile.
    /// </summary>
    /// <param name="Damage">The damage amount to assign.</param>
    public void SetDamage(int Damage)
    {
        _damage = Damage;
    }
}
