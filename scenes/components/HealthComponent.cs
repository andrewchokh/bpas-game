using Godot;

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
                EmitSignal(SignalName.EntityDied);
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
    public delegate void EntityDiedEventHandler();

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Max(1, damage - _defence);
    }
}
