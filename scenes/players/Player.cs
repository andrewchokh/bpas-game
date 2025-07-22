using Godot;
using System;

public abstract partial class Player : CharacterBody2D
{
    public MovementComponent MovementComponent { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public HitBoxComponent HitBoxComponent { get; private set; }
    public InputComponent InputComponent { get; private set; }

    [Export]
    public Ability Ability;
                  
    public override void _Ready()
    {
        base._Ready();

        MovementComponent = GetNode<MovementComponent>("MovementComponent");
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HitBoxComponent = GetNode<HitBoxComponent>("HitBoxComponent");
        InputComponent = GetNode<InputComponent>("InputComponent");
    }
}
