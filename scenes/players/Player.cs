using Godot;
using System;

/// <summary>
/// Represents a player character in the game.
/// </summary>
public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerCharacterId PlayerSceneId;

    [ExportCategory("Components")]
    [Export]
    public MovementComponent MovementComponent;
    [Export]
    public HealthComponent HealthComponent;
    [Export]
    public HitboxComponent HitboxComponent;
    [Export]
    public PlayerMovementController PlayerMovementController;
    [Export]
    public WeaponComponent WeaponComponent;
    [Export]
    public SuperAbilityComponent SuperAbilityComponent;
}
