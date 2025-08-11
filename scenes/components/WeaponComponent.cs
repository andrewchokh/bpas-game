using Godot;
using Godot.Collections;

public partial class WeaponComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public Array<PackedScene> Inventory = [];
    public int SelectedIndex { get; private set; } = 0; 

    [Signal]
    public delegate void InventoryChangedEventHandler();

    [Signal]
    public delegate void SelectedItemChangedEventHandler();

    public override void _Ready()
    {
        InventoryChanged += SynchronizeWeapons;
        SelectedItemChanged += SynchronizeWeapons;
        SynchronizeWeapons();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("switch_weapon"))
            SwitchWeapon();
        if (Input.IsActionJustPressed("drop_weapon"))
            DropWeapon(SelectedIndex);
    }

    public void SwitchWeapon()
    {
        ChangeSelectedIndex();

        EmitSignal(SignalName.SelectedItemChanged);
    }

    private void SynchronizeWeapons()
    {
        for (var i = 0; i < Inventory.Count - 1; i++)
        {
            if (Inventory[i] != null)
                break;
            
            ChangeSelectedIndex();
        }

        Utils.Instance.RemoveAllChildren(this);

        var weapon = Inventory[SelectedIndex].Instantiate() as Weapon;

        weapon._owner = Player;

        AddChild(weapon);
    }

    private void ChangeSelectedIndex()
    {
        SelectedIndex++;

        if (SelectedIndex >= Inventory.Count)
            SelectedIndex = 0;
    }

    public void GiveWeapon(PackedScene weaponScene, int index = -1)
    {
        if (index >= 0)
        {
            if (Inventory[index] == null)
                DropWeapon(SelectedIndex);

            Inventory[index] = weaponScene;
        }
        else
        {
            if (Inventory[SelectedIndex] == null)
                DropWeapon(SelectedIndex);

            Inventory[SelectedIndex] = weaponScene;
        }
        
        EmitSignal(SignalName.InventoryChanged);
    }

    public void DropWeapon(int index)
    {
        // Logic for Pick Ups (No Pick Up system implemented yet.)
    }
}