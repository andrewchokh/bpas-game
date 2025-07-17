using Godot;
using System;

public abstract partial class Projectile : Area2D
{
    [Export]
    public Weapon Weapon;

    public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}
    
	public void OnAreaEntered(Area2D Area)
    {
        if (Area is HitBoxComponent HitBox)
            HitBox.HealthComponent.TakeDamage(Weapon.Damage);
    }
}
