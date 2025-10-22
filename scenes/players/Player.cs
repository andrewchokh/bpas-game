using Godot;
using System;

using static Ids;

/// <summary>
/// Represents a player character in the game.
/// </summary>
public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerCharacterId PlayerSceneId;
    [Export]
    public AnimationPlayer AnimationPlayer;

    [ExportCategory("Components")]
    [Export]
    public SpriteComponent SpriteComponent;
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
    [Export]
    public StateComponent StateComponent;

    public override void _Ready()
    {
        // Trigger invincibility frames when getting hit
        HealthComponent.HealthChanged += (int oldHealth, int newHealth) =>
        {
            AnimationPlayer.Play("inv_frames");
        };
    }
}
