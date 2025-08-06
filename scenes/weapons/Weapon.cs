using Godot;
using System;

/// <summary>
/// Abstract base class for weapons.
/// Manages weapon type, damage, usage, and firing logic.
/// </summary>
public abstract partial class Weapon : Node2D
{
    /// <summary>
    /// Enum representing the type of weapon.
    /// </summary>
    public enum WeaponType : int
    {
        MELEE = 1,
        RANGED = 2,
        MAGIC = 3,
        UNIQUE = 4
    }

    /// <summary>
    /// The type of the weapon (melee, ranged, magic, or unique).
    /// </summary>
    [Export]
    public WeaponType Type;

    /// <summary>
    /// Damage dealt by the weapon per use.
    /// </summary>
    [Export]
    public int Damage;

    /// <summary>
    /// Critical hit chance of the weapon (0.0 to 1.0).
    /// </summary>
    [Export]
    public float CritChance;

    /// <summary>
    /// Attack speed or cooldown factor for the weapon.
    /// </summary>
    [Export]
    public float Speed;

    /// <summary>
    /// Number of uses left before the weapon is destroyed.
    /// -1 means infinite uses.
    /// </summary>
    [Export]
    public int Uses = -1;

    /// <summary>
    /// Emitted when the weapon is used.
    /// </summary>
    [Signal]
    public delegate void WeaponUsedEventHandler();

    public Player _Owner { get; set; } 

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Connects the WeaponUsed signal to its handler.
    /// </summary>

    public override void _Ready()
    {
        base._Ready();
        WeaponUsed += OnWeaponUsed;
    }

    /// <summary>
    /// Called every frame.
    /// Checks for input to use the weapon, decreases uses, emits signal, and frees the node if uses run out.
    /// </summary>
    /// <param name="delta">Time elapsed since last frame.</param>
    public override void _Process(double delta)
    {
        base._Process(delta);

        var CurrentOwner = GetParent().GetParent() as Player;

        if (CurrentOwner == null || CurrentOwner != _Owner)
            return;

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

    /// <summary>
    /// Handler for weapon usage logic.
    /// Override in subclasses to implement specific weapon behavior.
    /// </summary>
    public virtual void OnWeaponUsed()
    {
        // Placeholder for weapon usage logic.
    }
}