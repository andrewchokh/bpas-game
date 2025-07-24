using Godot;
using System;

public abstract partial class Weapon : Node2D
{
    public enum WeaponType : int
    {
        MELEE = 1,
        RANGED = 2,
        MAGIC = 3,
        UNIQUE = 4
    }

    [Export]
    public WeaponType Type;
    [Export]
    public int Damage;
    [Export]
    public float CritChance;
    [Export]
    public float Speed;
    [Export]
    public int Uses = -1; // -1 means infinite uses

    [Signal]
    public delegate void WeaponUsedEventHandler();

    public override void _Ready()
    {
        base._Ready();
        
        WeaponUsed += OnWeaponUsed;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use_weapon"))
        {
            if (Uses != -1)
                Uses--;

            EmitSignal(SignalName.WeaponUsed);
            GD.Print("ATTACK");

            if (Uses == 0)
                QueueFree();
        }
    }
    
    public virtual void OnWeaponUsed()
    {
        return; // Placeholder for weapon usage logic;
    }
}