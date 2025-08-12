using Godot;
using Godot.Collections;

using static EntityList;

public partial class WeaponComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public int MaxSlots = 3;

    public Dictionary<int, Weapon> Weapons = new();
    public int SelectedSlot = 0;

    [Signal]
    public delegate void WeaponChangedEventHandler();

    public override void _Ready()
    {
        WeaponChanged += UpdateSelectedWeapon;

        GiveWeapon(WeaponSceneId.ArcaneStaff);
        GiveWeapon(WeaponSceneId.Dagger);
        GiveWeapon(WeaponSceneId.Spear);
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

            if (Weapons[SelectedSlot] == null)
                continue;

            GD.Print($"Switching to weapon slot {i}");
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
        weapon._owner = Player;

        Weapons.Add(GetLastFreeSlot(), weapon);
        EmitSignal(SignalName.WeaponChanged);
    }

    public void DropWeapon(int slot)
    {
        return;
    }

    public void ReplaceWeapon(WeaponSceneId weaponSceneId, int slot)
    {
        return;
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

            PrintInventory();
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
}