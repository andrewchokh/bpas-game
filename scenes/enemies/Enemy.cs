using Godot;

public partial class Enemy : CharacterBody2D
{
    [ExportCategory("Components")]
    [Export]
    public MovementComponent MovementComponent;
    [Export]
    public HealthComponent HealthComponent;
    [Export]
    public HitboxComponent HitboxComponent;
}