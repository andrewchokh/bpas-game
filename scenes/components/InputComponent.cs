using Godot;
using System;

public partial class InputComponent : Node2D
{
    [Export]
    public Player Entity;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 InputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Entity.MovementComponent.Move(InputDirection, delta);
    }
}