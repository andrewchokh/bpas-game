using Godot;
using System;

public partial class Steve : Enemy
{
    public override void _Ready()
    {
        base._Ready();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        var Player = Utils.GetFirstPlayer();
        
        if (Player == null)
            return;
        
        var PlayerPosition = Player.GlobalPosition;

        var Direction = (PlayerPosition - GlobalPosition).Normalized();
        MovementComponent.Move(Direction, delta);
        
        MoveAndSlide();
    }
}