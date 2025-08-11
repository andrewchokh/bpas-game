using Godot;

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
    public int Uses = -1;

    [Signal]
    public delegate void WeaponUsedEventHandler();

    public Player _owner { get; set; } 

    public override void _Ready()
    {
        WeaponUsed += OnWeaponUsed;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        var currentOwner = GetParent().GetParent() as Player;

        if (currentOwner == null || currentOwner != _owner)
            return;

        if (Input.IsActionJustPressed("use_weapon"))
        {
            if (Uses != -1)
                Uses--;

            EmitSignal(SignalName.WeaponUsed);

            if (Uses == 0)
                QueueFree();
        }
    }

    public virtual void OnWeaponUsed()
    {
        // Placeholder for weapon usage logic.
    }
}