using Godot;
using System;

public partial class ChasePattern : BehaviorPattern
{
    public override void _PhysicsProcess(double delta)
    {
        var player = Utils.Instance.GetFirstPlayer();
        if (player == null)
            return;

        var playerPosition = player.GlobalPosition;
        var direction = (playerPosition - Enemy.GlobalPosition).Normalized();

        Enemy.MovementComponent.Move(direction, delta);
        Enemy.MoveAndSlide();
    }
}
