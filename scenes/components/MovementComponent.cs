using Godot;
using System;

public partial class MovementComponent : Node2D
{
	[Export]
	public CharacterBody2D Entity;

	[Export]
	public float Speed = 100f;

	public override void _Ready()
	{
		base._Ready();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Entity.MoveAndSlide();        
	}
	
	public void Move(Vector2 InputDirection, double delta)
	{
        Entity.Velocity = InputDirection * Speed * 100f * (float)delta;
	}
}
