using Godot;

public partial class SuperAbilityComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public SuperAbility Behavior;

    [Export]
    public Timer DurationTimer;
    [Export]
    public Timer CooldownTimer;

    private bool _canUseAbility = true;

    public override void _Ready()
    {
        DurationTimer.Timeout += OnDurationTimeout;
        CooldownTimer.Timeout += OnCooldownTimeout;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("activate_ability") && _canUseAbility)
            ActivateAbility();
    }

    public void ActivateAbility()
    {
        Behavior.Activate(Player);

        _canUseAbility = false;
        DurationTimer.Start();
    }

    public void OnDurationTimeout()
    {
        Behavior.Deactivate(Player);

        CooldownTimer.Start();
    }

    public void OnCooldownTimeout()
    {
        _canUseAbility = true;
    }
}
