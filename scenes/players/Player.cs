using Godot;
using System;

/// <summary>
/// Abstract base class for player-controlled characters.
/// Provides references to core components such as movement, health, hitbox, and input.
/// </summary>
public abstract partial class Player : CharacterBody2D
{
    /// <summary>
    /// Controls player movement behavior.
    /// </summary>
    public MovementComponent MovementComponent { get; private set; }

    /// <summary>
    /// Manages the player's health and damage logic.
    /// </summary>
    public HealthComponent HealthComponent { get; private set; }

    /// <summary>
    /// Detects collisions and interacts with damage systems.
    /// </summary>
    public HitBoxComponent HitBoxComponent { get; private set; }

    /// <summary>
    /// Handles player input and passes commands to movement and other systems.
    /// </summary>
    public InputComponent InputComponent { get; private set; }

    /// <summary>
    /// Called when the player enters the scene tree.
    /// Initializes references to required components.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();

        MovementComponent = GetNode<MovementComponent>("MovementComponent");
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HitBoxComponent = GetNode<HitBoxComponent>("HitBoxComponent");
        InputComponent = GetNode<InputComponent>("InputComponent");
    }
}
