using Godot;
using System;

public abstract partial class Player : CharacterBody2D
{
    [Export]
    public MovementComponent MovementComponent { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();

        MovementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}
