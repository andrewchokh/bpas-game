using Godot;
using System;

public abstract partial class Ability : Node
{
    [Export]
    public float Duration = 1.0f;

    [Signal]
    public delegate void AbilityActivatedEventHandler();

    public override void _Ready()
    {
        base._Ready();

        AbilityActivated += OnAbilityActivated;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_ability"))
        {
            EmitSignal(SignalName.AbilityActivated);
        }
    }

    private void OnAbilityActivated()
    {
        // Logic to handle the ability activation
    }

}
