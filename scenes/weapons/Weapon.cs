using Godot;

public enum WeaponType : int
{
    Melee = 1,
    Ranged = 2,
    Magic = 3,
    Unique = 4
}

/// <summary>
/// Represents a weapon in the game.
/// </summary>
public partial class Weapon : Node2D
{
    [Export]
    public WeaponId Id;
    [Export]
    public WeaponBehavior Behavior;

    [ExportGroup("Properties")]
    [Export]
    public WeaponType Type;
    [Export]
    public int Damage = 1;
    [Export]
    public float CritChance = 0.1f;
    [Export]
    public float Cooldown = 1.0f; // in seconds
    [Export]
    public int Uses = -1; // -1 for infinite uses

    public void UseWeapon(Marker2D weaponPoint, Vector2 mousePosition)
    {
        if (Uses != -1)
            Uses--;

        Behavior.Attack(this, weaponPoint, mousePosition);

        if (Uses == 0)
            QueueFree();
    }
}