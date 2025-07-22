using Godot;
using System;

public partial class AbilityAcceleration : Ability
{
	[Export]
	public float MovementSpeedMultiplier = 2.0f;

	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void OnAbilityActivated()
	{
		base.OnAbilityActivated();

		Player.MovementComponent.Speed *= MovementSpeedMultiplier;
	}
	
	public override void OnDurationTimeout()
	{
		base.OnDurationTimeout();

		Player.MovementComponent.Speed /= MovementSpeedMultiplier;
	}
}
