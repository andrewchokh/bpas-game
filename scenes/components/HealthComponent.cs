using Godot;

public partial class HealthComponent : Node2D
{
    [Export]
    public Node2D Entity;

    [Export]
    public int Health = 1;
    [Export]
    public int Defense = 0;

    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);

    [Signal]
    public delegate void EntityDiedEventHandler();

    public void TakeDamage(int damage)
    {
        int oldHealth = Health;
        Health -= Mathf.Max(1, damage - Defense);

        EmitSignal(SignalName.HealthChanged, oldHealth, Health);

        if (Health <= 0)
            EmitSignal(SignalName.EntityDied);
    }
}
