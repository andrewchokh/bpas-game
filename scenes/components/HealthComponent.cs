using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export]
	public Node2D Entity;

	[Export]
	public int Health = 1;
	[Export]
	public int Defense = 0;

	[Signal]
	public delegate void HealthChangedEventHandler(int OldHealth, int NewHealth);
	[Signal]
	public delegate void EntityDiedEventHandler();

	public void TakeDamage(int Damage)
	{
		int OldHealth = Health;
		Health -= Math.Max(1, Damage - Defense);

		if (Health <= 0)
			EmitSignal(SignalName.EntityDied);
			
		EmitSignal(SignalName.HealthChanged, OldHealth, Health);
	}
}