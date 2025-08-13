using Godot;
using System;

public partial class RotatingPattern : BehaviorPattern
{
    public override void _Ready()
    {
        Enemy.Velocity = Vector2.Zero; // Ensure enemy starts with no velocity
        Enemy.Rotation = 0;
    }
    public override void _PhysicsProcess(double delta)
    {
        Enemy.Velocity = Vector2.Zero; // Stop any movement
        Enemy.Rotation += (float)(Math.PI / 180) * 15 * (float)delta; // Rotate 45 degrees per second
    }
}
