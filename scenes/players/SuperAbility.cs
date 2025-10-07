using Godot;
using System;

[GlobalClass]
public abstract partial class SuperAbility : Resource
{
    public abstract void Activate(Player player);
    public abstract void Deactivate(Player player); 
}
