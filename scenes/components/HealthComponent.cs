using Godot;
using System;

/// <summary>
/// Component that manages an entity's health and damage handling.
/// Emits signals when health changes or the entity dies.
/// </summary>
public partial class HealthComponent : Node2D
{
    /// <summary>
    /// The entity this health component belongs to.
    /// Usually the parent or owner node.
    /// </summary>
    [Export]
    public Node2D Entity;

    /// <summary>
    /// Current health of the entity.
    /// </summary>
    [Export]
    public int Health = 1;

    /// <summary>
    /// Damage reduction applied when taking damage.
    /// </summary>
    [Export]
    public int Defense = 0;

    /// <summary>
    /// Emitted when health is changed.
    /// <para>OldHealth: Health before taking damage.</para>
    /// <para>NewHealth: Health after taking damage.</para>
    /// </summary>
    [Signal]
    public delegate void HealthChangedEventHandler(int OldHealth, int NewHealth);

    /// <summary>
    /// Emitted when health reaches 0 or below.
    /// </summary>
    [Signal]
    public delegate void EntityDiedEventHandler();

    /// <summary>
    /// Applies incoming damage to the entity, accounting for defense.
    /// Triggers death if health falls to 0 or below, and emits signals.
    /// </summary>
    /// <param name="Damage">The raw amount of incoming damage.</param>
    public void TakeDamage(int Damage)
    {
        int OldHealth = Health;
        Health -= Math.Max(1, Damage - Defense);

        if (Health <= 0)
            EmitSignal(SignalName.EntityDied);

        EmitSignal(SignalName.HealthChanged, OldHealth, Health);
    }
}
