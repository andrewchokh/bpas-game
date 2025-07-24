using Godot;
using System;

public partial class Spear : Weapon
{
    public override void _Ready()
    {
        base._Ready();
    }

    // public override void _Process(double delta)
    // {
    //     base._Process(delta);
    //     RigidBody2D Bullet = GetNode<RigidBody2D>("Bullet");

    //     Bullet.Rotation = GlobalRotation;
    //     Bullet.GlobalPosition = GlobalPosition;
    //     Bullet.LinearVelocity = Bullet.Transform.X * Speed;

    //     GetTree().Root.AddChild(Bullet);
    // }
}