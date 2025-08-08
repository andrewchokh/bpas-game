using Godot;
using System;
using System.Collections;

/// <summary>
/// Represents a pickable item in the game world.
/// Can be extended to implement item-specific pickup behavior.
/// </summary>
public abstract partial class PickUp : Area2D
{
    [Export]
    public EntityList.PickUpType ID;
    [Export]
    public PackedScene WeaponScene;

    [Signal]
    public delegate void PickedUpEventHandler();
    private Player NearestPlayer;

    public override void _Ready()
    {
        base._Ready();

        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
        PickedUp += OnPickedUp;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("use") && NearestPlayer != null)
            EmitSignal(SignalName.PickedUp);
    }

    public void OnPickedUp()
    {
        var WeaponComponent = NearestPlayer.GetNode<WeaponComponent>("WeaponComponent");

        Utils.RemoveAllChildren(WeaponComponent);
        WeaponComponent.GiveWeapon(WeaponScene); 
        QueueFree();
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitBoxComponent HitBox && HitBox.Entity is Player Player)
            NearestPlayer = Player;
    }

    public void OnAreaExited(Area2D area)
    {
        if (area is HitBoxComponent HitBox && HitBox.Entity is Player Player && NearestPlayer == Player)
            NearestPlayer = null;
    }
}