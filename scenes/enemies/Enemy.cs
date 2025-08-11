using Godot;

public abstract partial class Enemy : CharacterBody2D
{
    public HealthComponent HealthComponent { get; private set; }
    public HitBoxComponent HitBoxComponent { get; private set; }
    public MovementComponent MovementComponent { get; private set; }

    public override void _Ready()
    {
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HitBoxComponent = GetNode<HitBoxComponent>("HitBoxComponent");
        MovementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}