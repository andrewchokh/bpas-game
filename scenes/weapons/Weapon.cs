using Godot;

using static EntityList;

public enum WeaponType : int
{
    Melee = 1,
    Ranged = 2,
    Magic = 3,
    Unique = 4
}

public abstract partial class Weapon : Node2D
{
    [Export]
    public WeaponSceneId SceneId;

    [ExportGroup("Weapon Properties")]
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

    public abstract void OnWeaponUsed();
}