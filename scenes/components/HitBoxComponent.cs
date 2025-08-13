using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public Node2D Entity;

    [Export]
    public HealthComponent HealthComponent;
}
