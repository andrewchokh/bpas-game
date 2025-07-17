using Godot;
using System;

public partial class MovementComponent : Node2D
{
	[Export]
	public Player Player;

	[Export]
	public float Speed = 100f;

	public override void _Ready()
	{
		base._Ready();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput(delta);
		Player.MoveAndSlide();        
	}
	
	public void GetInput(double delta)
	{
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Player.Velocity = inputDirection * Speed;
	}
}
