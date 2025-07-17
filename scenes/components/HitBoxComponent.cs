using Godot;
using System;

public partial class HitBoxComponent : Area2D
{
	[Export]
	public Node2D Entity;

	[Export]
	public HealthComponent HealthComponent;
}

