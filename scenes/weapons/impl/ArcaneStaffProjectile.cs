using Godot;
using System;

public partial class ArcaneStaffProjectile : Node2D
{
    [Export]
    float Speed = 300f;

    public override void _Process(double delta)
    {
        base._Process(delta);

        Position += Transform.X * Speed * (float)delta;
    }
}