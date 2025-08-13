using Godot;
using System;

public partial class Spear : Weapon
{
    public override void OnWeaponUsed()
    {
        GD.Print("Damage = " + Damage);
    }
}