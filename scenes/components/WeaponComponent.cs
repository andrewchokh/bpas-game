using Godot;
using Godot.Collections;

using static EntityList;

public partial class WeaponComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public int MaxSlots = 3;
    [Export]
    public Marker2D WeaponPoint;

    public Dictionary<int, Weapon> Weapons = new();
    public int SelectedSlot { get; private set; } = 0;

    [Signal]
    public delegate void WeaponChangedEventHandler();

    public override void _Ready()
    {
        WeaponChanged += UpdateSelectedWeapon;

        GiveWeapon(WeaponSceneId.ArcaneStaff);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("switch_weapon"))
            SwitchWeapon(SelectedSlot);
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

    public void GiveWeapon(WeaponSceneId weaponSceneId)
    {
        if (Weapons.Count >= MaxSlots)
        {
            GD.PushWarning("Cannot give weapon, max slots reached.");
            return;
        }

        var weapon = EntityList.Instance.WeaponScenes[weaponSceneId].Instantiate<Weapon>();
        AddChild(weapon);
        SetupWeapon(weapon);

        Weapons.Add(GetLastFreeSlot(), weapon);

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

        var pickup = EntityList.Instance.PickUpScenes[Weapons[slot].SceneId].Instantiate<PickUp>();
        GetTree().Root.CallDeferred("add_child", pickup);
        pickup.GlobalPosition = GlobalPosition;

        Weapons[slot].QueueFree();

        EmitSignal(SignalName.WeaponChanged);
    }

    public void ReplaceWeapon(WeaponSceneId weaponSceneId, int slot)
    {
        if (!Weapons.ContainsKey(slot))
        {
            GD.PushError($"No weapon found in slot {slot} to drop.");
            return;
        }

        if (Weapons[slot] is Weapon)
            DropWeapon(slot);

        var weapon = EntityList.Instance.WeaponScenes[weaponSceneId].Instantiate<Weapon>();
        AddChild(weapon);
        SetupWeapon(weapon);

        Weapons[slot] = weapon;

        EmitSignal(SignalName.WeaponChanged);
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

    private int GetLastFreeSlot()
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
        weapon.GlobalPosition = WeaponPoint.GlobalPosition;
        weapon._owner = Player;
    }
}