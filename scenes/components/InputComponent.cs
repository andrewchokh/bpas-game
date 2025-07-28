using Godot;
using System;

/// <summary>
/// Component responsible for reading player input and passing it to the movement logic.
/// Should be attached to a player-controlled entity.
/// </summary>
public partial class InputComponent : Node2D
{
    /// <summary>
    /// The entity this input component belongs to.
    /// Must have a MovementComponent for directional input to work.
    /// </summary>
    [Export]
    public Player Player;

    /// <summary>
    /// Reads directional input each physics frame and sends it to the entity's movement component.
    /// </summary>
    /// <param name="delta">Time since the last physics frame.</param>
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
        Vector2 InputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Player.MovementComponent.Move(InputDirection, delta);
    }
}