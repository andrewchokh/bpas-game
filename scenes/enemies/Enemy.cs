using Godot;
using System;

/// <summary>
/// Abstract base class for all enemy entities.
/// Handles common setup and provides references to core components.
/// </summary>
public abstract partial class Enemy : CharacterBody2D
{
    /// <summary>
    /// Manages the health and damage logic for the enemy.
    /// </summary>
    public HealthComponent HealthComponent { get; private set; }

    /// <summary>
    /// Detects collisions and interacts with damage systems.
    /// </summary>
    public HitBoxComponent HitBoxComponent { get; private set; }

    /// <summary>
    /// Controls enemy movement behavior.
    /// </summary>
    public MovementComponent MovementComponent { get; private set; }

    /// <summary>
    /// Called when the enemy enters the scene tree.
    /// Initializes references to required components.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();

        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HitBoxComponent = GetNode<HitBoxComponent>("HitBoxComponent");
        MovementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}