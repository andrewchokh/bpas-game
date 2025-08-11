using Godot;

public partial class Steve : Enemy
{
    public override void _PhysicsProcess(double delta)
    {
        var player = Utils.Instance.GetFirstPlayer();
        if (player == null)
            return;

        var playerPosition = player.GlobalPosition;
        var direction = (playerPosition - GlobalPosition).Normalized();

        MovementComponent.Move(direction, delta);
        MoveAndSlide();
    }
}
