using Godot;
using Godot.Collections;

using static Ids;

/// <summary>
/// Manages weapon system and inventory for a Player entity.
/// </summary>
public partial class WeaponComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public EntityDatabase Entities;

    [Export]
    public int MaxSlots = 3;
    [Export]
    public Marker2D WeaponPosition;

    public Dictionary<int, Weapon> Weapons = new();
    public int SelectedSlot { get; private set; } = 0;

    [Signal]
    public delegate void WeaponChangedEventHandler();

    public override void _Ready()
    {
        WeaponChanged += UpdateSelectedWeapon;

        // For testing purposes, give the player an initial weapon.
        // GiveWeapon(WeaponId.ArcaneStaff);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("switch_weapon"))
            SwitchWeapon(SelectedSlot);

        if (Input.IsActionJustPressed("use_weapon"))
            Weapons[SelectedSlot]?.UseWeapon(WeaponPosition, GetGlobalMousePosition());
    }

    public void SwitchWeapon(int slot)
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            SelectNextSlot();

            if (!Weapons.ContainsKey(SelectedSlot))
                continue;

            GD.Print($"Switching to weapon slot {SelectedSlot}");
            UpdateSelectedWeapon();
            break;
        }
    }

    public void GiveWeapon(WeaponId weaponId, int slot = -1)
    {
        if (Weapons.Count >= MaxSlots)
        {
            GD.PushWarning("Failed to give weapon: max slots reached.");
            return;
        }

        var weapon = Entities.WeaponScenes[weaponId].Instantiate<Weapon>();
        SetupWeapon(weapon);

        Weapons.Add(slot >= 0 ? slot : GetFirstFreeSlot(), weapon);

        EmitSignal(SignalName.WeaponChanged);
    }

    public void DropWeapon(int slot)
    {
        if (!Weapons.ContainsKey(slot))
        {
            GD.PushError($"No weapon found in slot {slot} to drop.");
            return;
        }

        if (Weapons[slot] is not Weapon)
        {
            GD.PushError($"Slot {slot} does not contain a valid weapon.");
            return;
        }

        var pickup = Entities.PickUpScenes[Weapons[slot].Id].Instantiate<PickUp>();
        GetTree().Root.CallDeferred("add_child", pickup);
        pickup.GlobalPosition = GlobalPosition;

        Weapons[slot].QueueFree();

        EmitSignal(SignalName.WeaponChanged);
    }

    public void ReplaceWeapon(WeaponId weaponId, int slot)
    {
        if (!Weapons.ContainsKey(slot))
        {
            GD.PushError($"No weapon found in slot {slot} to drop.");
            return;
        }

        if (Weapons[slot] is Weapon)
            DropWeapon(slot);

        GiveWeapon(weaponId, slot);
    }

    private void UpdateSelectedWeapon()
    {
        foreach (int key in Weapons.Keys)
        {
            if (key != SelectedSlot)
            {
                Weapons[key].ProcessMode = ProcessModeEnum.Disabled;
                Weapons[key].Visible = false;
                continue;
            }

            Weapons[key].ProcessMode = ProcessModeEnum.Inherit;
            Weapons[key].Visible = true;
            Weapons[key].Rotation = Player.Rotation;
        }

        PrintInventory();
    }

    private void PrintInventory()
    {
        GD.Print("Current Inventory:");
        foreach (var weapon in Weapons)
        {
            GD.Print($"Slot {weapon.Key}: {weapon.Value.Name}");
        }
    }

    private int GetFirstFreeSlot()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            if (!Weapons.ContainsKey(i))
                return i;
        }

        GD.PushWarning("No free slot found in inventory.");
        return -1;
    }

    private void SelectNextSlot()
    {
        SelectedSlot++;

        if (SelectedSlot >= MaxSlots)
            SelectedSlot = 0;
    }

    private void SetupWeapon(Weapon weapon)
    {
        AddChild(weapon);
        weapon.GlobalPosition = WeaponPosition.GlobalPosition;
    }
}