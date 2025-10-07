using Godot;

/// <summary>
/// Manages hitbox for a Node2D entity.
/// </summary>
public partial class HitboxComponent : Area2D
{
    [Export]
    public Node2D Entity;

    [Export]
    public HealthComponent HealthComponent;
}
