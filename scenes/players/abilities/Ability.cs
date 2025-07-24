using Godot;
using System;

/// <summary>
/// Abstract base class for player abilities. Handles activation, duration, and cooldown logic.
/// </summary>
public abstract partial class Ability : Node2D
{
    /// <summary>
    /// Reference to the owning player.
    /// </summary>
    public Player Player;

    /// <summary>
    /// Timer tracking how long the ability remains active.
    /// </summary>
    public Timer DurationTimer;

    /// <summary>
    /// Timer tracking cooldown period after ability use.
    /// </summary>
    public Timer CooldownTimer;

    /// <summary>
    /// Whether the ability is currently available for use.
    /// </summary>
    public bool CanUseAbility = true;

    /// <summary>
    /// Emitted when the ability is activated by the player.
    /// </summary>
    [Signal]
    public delegate void AbilityActivatedEventHandler();

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Initializes timers and signal connections.
    /// </summary>
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

    /// <summary>
    /// Called every frame. Checks for ability input trigger.
    /// </summary>
    /// <param name="delta">Time elapsed since last frame.</param>
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_ability") && CanUseAbility)
            EmitSignal(SignalName.AbilityActivated);
    }

    /// <summary>
    /// Called when the ability is activated.
    /// Starts the duration timer and disables further use.
    /// </summary>
    public virtual void OnAbilityActivated()
    {
        CanUseAbility = false;
        DurationTimer.Start();
    }

    /// <summary>
    /// Called when the duration timer finishes.
    /// Starts the cooldown timer.
    /// </summary>
    public virtual void OnDurationTimeout()
    {
        CooldownTimer.Start();
    }

    /// <summary>
    /// Called when the cooldown timer finishes.
    /// Re-enables ability use.
    /// </summary>
    public virtual void OnCooldownTimeout()
    {
        CanUseAbility = true;
    }
}
