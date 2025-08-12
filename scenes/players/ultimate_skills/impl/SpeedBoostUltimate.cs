using Godot;

public partial class SpeedBoostUltimate : UltimateSkill
{
    [Export]
    public float MovementSpeedMultiplier = 2.0f;

    private MovementComponent _movementComponent;

    private float _speedDifference;

    public override void OnAbilityActivated()
    {
        base.OnAbilityActivated();

        _movementComponent = Player.MovementComponent;

        _speedDifference = (_movementComponent.Speed * MovementSpeedMultiplier) - _movementComponent.Speed;

        _movementComponent.Speed += _speedDifference;
    }

    public override void OnDurationTimeout()
    {
        base.OnDurationTimeout();

        _movementComponent.Speed -= _speedDifference;
        
        _movementComponent = null;
    }
}
