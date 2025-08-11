using Godot;

public abstract partial class Ability : Node2D
{
    public Player Player;

    public Timer DurationTimer;
    public Timer CooldownTimer;

    private bool _canUseAbility = true;

    [Signal]
    public delegate void AbilityActivatedEventHandler();

    public override void _Ready()
    {
        Player = GetParent<Player>();
        AbilityActivated += OnAbilityActivated;

        DurationTimer = GetNode<Timer>("DurationTimer");
        CooldownTimer = GetNode<Timer>("CooldownTimer");

        DurationTimer.Timeout += OnDurationTimeout;
        CooldownTimer.Timeout += OnCooldownTimeout;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_ability") && _canUseAbility)
            EmitSignal(SignalName.AbilityActivated);
    }

    public virtual void OnAbilityActivated()
    {
        _canUseAbility = false;
        DurationTimer.Start();
    }

    public virtual void OnDurationTimeout()
    {
        CooldownTimer.Start();
    }

    public virtual void OnCooldownTimeout()
    {
        _canUseAbility = true;
    }
}
