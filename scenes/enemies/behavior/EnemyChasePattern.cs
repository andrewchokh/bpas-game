using Godot;
using System;

[GlobalClass]
public partial class EnemyChasePattern : EnemyBehavior
{
    public override bool IsProcessEnabled => false;
    public override bool IsPhysicsProcessEnabled => true;

    public override void ExecuteProcess(Enemy enemy, double delta)
    {
        return; // No operation is required in process
    }

    public override void ExecutePhysicsProcess(Enemy enemy, double delta)
    {
        enemy.Velocity = Vector2.Zero; // Ensure enemy starts with no velocity

        var player = Utils.Instance.GetFirstPlayer(enemy.GetTree());
        if (player == null)
            return;

        var playerPosition = player.GlobalPosition;
        var direction = (playerPosition - enemy.GlobalPosition).Normalized();

        enemy.MovementComponent.Move(direction, delta);
        enemy.MoveAndSlide();
    }
}
