using Godot;

public partial class MovementComponent : Node2D
{
    [Export]
    public CharacterBody2D Entity;

    private float _speed;
    [Export]
    public float Speed
    { 
        get => _speed;
        set
        {
            _speed = Mathf.Max(0f, value);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Entity.MoveAndSlide();
    }

    public void Move(Vector2 inputDirection, double delta)
    {
        Entity.Velocity = inputDirection * _speed * 100f * (float)delta;
    }
}
