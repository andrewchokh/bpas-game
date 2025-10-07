using Godot;

/// <summary>
/// Represents a player character in the game.
/// </summary>
public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerCharacterId PlayerSceneId;
    [Export]
    public PackedScene UltimateSkillScene;

    [ExportCategory("Components")]
    [Export]
    public MovementComponent MovementComponent;
    [Export]
    public HealthComponent HealthComponent;
    [Export]
    public HitboxComponent HitboxComponent;
    [Export]
    public PlayerMovementController PlayerMovementController;
    [Export]
    public WeaponComponent WeaponComponent;

    public override void _Ready()
    {
        SetupUltimateSkill();
    }

    private void SetupUltimateSkill()
    {
        if (UltimateSkillScene == null)
        {
            GD.PushWarning("UltimateSkillScene is not set for Player.");
            return;
        }

        var ultimateSkill = UltimateSkillScene.Instantiate<UltimateSkill>();

        AddChild(ultimateSkill);
    }
}
