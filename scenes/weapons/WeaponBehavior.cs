using Godot;
using System;

[GlobalClass]
public abstract partial class WeaponBehavior : Resource
{
    public abstract void Attack(Weapon weapon, Marker2D weaponPoint, Vector2 mousePosition);
}
