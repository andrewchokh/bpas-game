using Godot;

public partial class AbilityAcceleration : Ability
{
    [Export]
    public float MovementSpeedMultiplier = 2.0f;

    private float _speedDifference;

    public override void OnAbilityActivated()
    {
        base.OnAbilityActivated();

        _speedDifference = (Player.MovementComponent.Speed * MovementSpeedMultiplier) - Player.MovementComponent.Speed;

        Player.MovementComponent.Speed += _speedDifference;
    }
    
    public override void OnDurationTimeout()
    {
        base.OnDurationTimeout();

        Player.MovementComponent.Speed -= _speedDifference;
    }
}
