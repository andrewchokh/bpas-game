using Godot;
using System;

public abstract partial class Ability : Node2D
{
    [Export]
    public float Duration = 1.0f;
    [Export]
    public float Cooldown = 5.0f;

    [Signal]
    public delegate void AbilityActivatedEventHandler();

    public Player Player { get; private set; } 

    public override void _Ready()
    {
        base._Ready();

        Player = GetParent<Player>();
        AbilityActivated += OnAbilityActivated;
    }

    public bool IsAbilityActive = false;
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_ability") && IsAbilityActive == false)
        {
            EmitSignal(SignalName.AbilityActivated);
            GD.PushWarning("Ability activated signal emitted.");
            IsAbilityActive = true;
        }
        else
        {
            return;
        }
    }

    public abstract void OnAbilityActivated();
}
