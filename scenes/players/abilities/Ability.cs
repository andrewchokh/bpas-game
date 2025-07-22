using Godot;
using System;

public abstract partial class Ability : Node2D
{
    public Player Player;
    public Timer DurationTimer;
    public Timer CooldownTimer;
    public bool CanUseAbility = true;

    [Signal]
    public delegate void AbilityActivatedEventHandler();

    public override void _Ready()
    {
        base._Ready();

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

        if (Input.IsActionJustPressed("use_ability") && CanUseAbility)
            EmitSignal(SignalName.AbilityActivated);
    }

    public virtual void OnAbilityActivated()
    {
        CanUseAbility = false;
        DurationTimer.Start();
    }

    public virtual void OnDurationTimeout()
    {
        CooldownTimer.Start();
    }

    public virtual void OnCooldownTimeout()
    {
        CanUseAbility = true;
    }
}