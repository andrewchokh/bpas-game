using Godot;
using System;

public partial class PickUp : Area2D
{
    [Export]
    public Node2D Entity;

    private bool _isPlayerHitBoxOverlap;

    public override void _Ready()
    {
        base._Ready();

        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_isPlayerHitBoxOverlap && Input.IsActionJustPressed("use"))
        {
            // Implement logic to pick up the item
            
            QueueFree();
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitBoxComponent hitBox && hitBox.Entity.IsInGroup("Players"))
            _isPlayerHitBoxOverlap = true;
    }
    
    public void OnAreaExited(Area2D area)
    {
        if (area is HitBoxComponent hitBox && hitBox.Entity.IsInGroup("Players"))
            _isPlayerHitBoxOverlap = false;
    }
}
