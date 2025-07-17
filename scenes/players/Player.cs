using Godot;
using System;

public abstract partial class Player : CharacterBody2D
{
    public const string ID = "player";

    [Export]
    public string DisplayName;
    [Export]
    public string Description;

    public MovementComponent MovementComponent { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();

        MovementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}
