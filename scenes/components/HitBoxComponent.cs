using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public Node2D Entity;

    [Export]
    public HealthComponent HealthComponent;
    
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is Projectile projectile)
            HealthComponent.TakeDamage(projectile.Damage);
    }
}
