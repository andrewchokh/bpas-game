using Godot;
using System;

public partial class Dagger : Weapon
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void OnWeaponUsed()
    {
        base.OnWeaponUsed();

        GD.Print("Damage = " + Damage);
    }
}