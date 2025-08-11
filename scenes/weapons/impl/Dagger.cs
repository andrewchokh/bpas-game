using Godot;
using System;

public partial class Dagger : Weapon
{
    public override void OnWeaponUsed()
    {
        base.OnWeaponUsed();

        GD.Print("Damage = " + Damage);
    }
}