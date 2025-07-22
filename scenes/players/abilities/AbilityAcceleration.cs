using Godot;
using System;

public partial class AbilityAcceleration : Ability
{
	public override void _Ready()
	{
		var Timer = GetNode<Timer>("AbilityDurationTimer");
		Timer.Start(3f);
		Timer.Timeout += () =>
		{
			Player.MovementComponent.Speed /= 2;
			IsAbilityActive = false;
		};
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public override void OnAbilityActivated()
	{
		Player.MovementComponent.Speed *= 2;
	}
}
