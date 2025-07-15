using Godot;
using System;

public partial class Player : CharacterBody2D
{

    private MovementComponent _movementComponent;
    public MovementComponent MovementComponent
    {
        get => _movementComponent;
    }

    public override void _Ready()
    {
        base._Ready();

        _movementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}
