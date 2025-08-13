using Godot;

public partial class PlayerMovementController : Node2D
{
    [Export]
    public Player Player;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Player.MovementComponent.Move(inputDirection, delta);
    }
}