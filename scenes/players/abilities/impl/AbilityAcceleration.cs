using Godot;
using System;

/// <summary>
/// Increases the player's movement speed temporarily when the ability is activated.
/// </summary>
public partial class AbilityAcceleration : Ability
{
    /// <summary>
    /// Multiplier applied to the player's movement speed.
    /// </summary>
    [Export]
    public float MovementSpeedMultiplier = 2.0f;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
    }

    /// <summary>
    /// Called every frame. Inherited behavior only.
    /// </summary>
    /// <param name="delta">Time elapsed since last frame.</param>
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    /// <summary>
    /// Called when the ability is activated.
    /// Multiplies the player's movement speed.
    /// </summary>
    public override void OnAbilityActivated()
    {
        base.OnAbilityActivated();

        Player.MovementComponent.Speed *= MovementSpeedMultiplier;
    }

    /// <summary>
    /// Called when the ability's duration ends.
    /// Resets the player's movement speed to normal.
    /// </summary>
    public override void OnDurationTimeout()
    {
        base.OnDurationTimeout();
		
        Player.MovementComponent.Speed /= MovementSpeedMultiplier;
    }
}
