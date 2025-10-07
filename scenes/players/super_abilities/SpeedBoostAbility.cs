using Godot;
using System;

[GlobalClass]
public partial class SpeedBoostAbility : SuperAbility
{
    [Export]
    public float MovementSpeedMultiplier = 2.0f;

    private MovementComponent _movementComponent;
    private float _speedDifference;

    public override void Activate(Player player)
    {
        _movementComponent = player.MovementComponent;
        _speedDifference = (_movementComponent.Speed * MovementSpeedMultiplier) - _movementComponent.Speed;

        _movementComponent.Speed += _speedDifference;
    }

    public override void Deactivate(Player player)
    {
        _movementComponent.Speed -= _speedDifference;
    }
}
