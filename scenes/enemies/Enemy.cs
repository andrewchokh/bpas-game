using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    public HealthComponent HealthComponent { get; private set; }
    public HitBoxComponent HitBoxComponent { get; private set; }

    public Ability Ability { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HitBoxComponent = GetNode<HitBoxComponent>("HitBoxComponent");
    }
}
