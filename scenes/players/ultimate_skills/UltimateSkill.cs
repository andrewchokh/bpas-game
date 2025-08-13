using Godot;

public partial class UltimateSkill : Node2D
{
    [Export]
    public Timer DurationTimer;
    [Export]
    public Timer CooldownTimer;

    public Player Player;

    private bool _canUseAbility = true;

    [Signal]
    public delegate void AbilityActivatedEventHandler();

    public override void _Ready()
    {
        Player = GetParent<Player>();

        AbilityActivated += OnAbilityActivated;
        DurationTimer.Timeout += OnDurationTimeout;
        CooldownTimer.Timeout += OnCooldownTimeout;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("activate_ultimate_skill") && _canUseAbility)
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
