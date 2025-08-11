using Godot;

public partial class MovementComponent : Node2D
{
    [Export]
    public CharacterBody2D Entity;

    [Export]
    public float Speed = 100f;

    public override void _PhysicsProcess(double delta)
    {
        Entity.MoveAndSlide();
    }

    public void Move(Vector2 inputDirection, double delta)
    {
        Entity.Velocity = inputDirection * Speed * 100f * (float)delta;
    }
}
