using Godot;
using System;

public partial class WeaponComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public Godot.Collections.Array<PackedScene> Inventory = [];

    private int SelectedIndex = 0;

    [Signal]
    public delegate void InventoryChangedEventHandler();

    [Signal]
    public delegate void SelectedItemChangedEventHandler();

    public override void _Ready()
    {
        base._Ready();

        InventoryChanged += SynchronizeWeapons;
        SelectedItemChanged += SynchronizeWeapons;
        SynchronizeWeapons();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("switch_weapon"))
            SwitchWeapon();
        if (Input.IsActionJustPressed("drop_weapon"))
            DropWeapon(SelectedIndex);
    }

    private void SwitchWeapon()
    {
        ChangeSelectedIndex();

        EmitSignal(SignalName.SelectedItemChanged);
    }

    private void SynchronizeWeapons()
    {
        while (Inventory[SelectedIndex] == null)
            ChangeSelectedIndex();

        Utils.Instance.RemoveAllChildren(this);

        AddChild(Inventory[SelectedIndex].Instantiate());
    }

    private void ChangeSelectedIndex()
    {
        SelectedIndex++;

        if (SelectedIndex >= Inventory.Count)
            SelectedIndex = 0;
    }

    public void GiveWeapon(PackedScene WeaponScene)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = WeaponScene;
                EmitSignal(SignalName.InventoryChanged);
                return;
            }
        }

        DropWeapon(SelectedIndex);
        Inventory[SelectedIndex] = WeaponScene;

        EmitSignal(SignalName.InventoryChanged);
    }

    public void DropWeapon(int Index)
    {
        // Logic for Pick Ups (No Pick Up system implemented yet.)
        if (Input.IsActionJustPressed("drop_weapon") && Index >= 0 && Index < Inventory.Count && Inventory[Index] != null)
            GD.Print("Drop weapon action triggered, but no pick up system implemented yet.");
        return;
    }
}