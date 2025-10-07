using Godot;

/// <summary>
/// Manages health for a Node2D entity.
/// </summary>
public partial class HealthComponent : Node2D
{
    [Export]
    public Node2D Entity;

    private int _health;
    [Export]
    public int Health
    {
        get => _health;
        set
        {
            int oldHealth = _health;
            _health = Mathf.Max(0, value);

            EmitSignal(SignalName.HealthChanged, oldHealth, _health);

            if (_health == 0)
                EmitSignal(SignalName.EntityDestroyed);
        }
    }

    private int _defence;
    [Export]
    public int Defense
    {
        get => _defence;
        set
        {
            _defence = Mathf.Max(0, value);
        }
    }

    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);

    [Signal]
    public delegate void EntityDestroyedEventHandler();

    public override void _Ready()
    {
        EntityDestroyed += () =>
        {
            GD.Print($"{Entity.Name} has been destroyed.");
            Entity.QueueFree();
        };
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(1, damage - Defense);
        Health -= finalDamage;

        GD.Print($"{Entity.Name} took {finalDamage} damage, remaining health: {Health}");
    }
}
