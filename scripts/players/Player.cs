using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public MovementComponent MovementComponent{get; private set;}
    
    public override void _Ready()
    {
        base._Ready();

        MovementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}
