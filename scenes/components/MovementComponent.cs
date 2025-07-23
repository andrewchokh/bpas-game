using Godot;
using System;

/// <summary>
/// Component responsible for moving an entity based on directional input.
/// Wraps and delegates movement logic to a CharacterBody2D node.
/// </summary>
public partial class MovementComponent : Node2D
{
    /// <summary>
    /// The entity this movement component controls.
    /// Must be a CharacterBody2D.
    /// </summary>
    [Export]
    public CharacterBody2D Entity;

    /// <summary>
    /// Movement speed multiplier applied to input direction.
    /// </summary>
    [Export]
    public float Speed = 100f;

    /// <summary>
    /// Called when the node is added to the scene.
    /// Used for setup or validation.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
    }

    /// <summary>
    /// Applies the entity's current velocity using Godot's built-in movement function.
    /// </summary>
    /// <param name="delta">Time since the last physics frame.</param>
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Entity.MoveAndSlide();
    }

    /// <summary>
    /// Sets the velocity of the entity based on input direction and movement speed.
    /// </summary>
    /// <param name="InputDirection">Normalized direction vector from player input.</param>
    /// <param name="delta">Time since the last physics frame.</param>
    public void Move(Vector2 InputDirection, double delta)
    {
        Entity.Velocity = InputDirection * Speed * 100f * (float)delta;
    }
}
