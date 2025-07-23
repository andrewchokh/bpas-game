using Godot;
using System;

/// <summary>
/// A specific enemy implementation that chases the player character.
/// Inherits from the abstract Enemy base class.
/// </summary>
public partial class Steve : Enemy
{
    /// <summary>
    /// Called when the node enters the scene tree.
    /// Inherits component initialization from the base Enemy class.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();
    }

    /// <summary>
    /// Called every physics frame.
    /// Moves the enemy toward the first available player if one exists.
    /// </summary>
    /// <param name="delta">The frame time since the last physics update.</param>
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        var Player = Utils.GetFirstPlayer();
        if (Player == null)
            return;

        var PlayerPosition = Player.GlobalPosition;
        var Direction = (PlayerPosition - GlobalPosition).Normalized();

        MovementComponent.Move(Direction, delta);
        MoveAndSlide();
    }
}
