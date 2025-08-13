using Godot;

using static EntityList;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerSceneId PlayerSceneId;
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
