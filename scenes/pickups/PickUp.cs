using Godot;

using static EntityList;

public partial class PickUp : Area2D
{
    [Export]
    public WeaponSceneId WeaponSceneId;

    private Player _nearestPlayer;

    [Signal]
    public delegate void PickedUpEventHandler();

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
        PickedUp += OnPickedUp;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("interact") && _nearestPlayer != null)
            EmitSignal(SignalName.PickedUp);
    }

    public void OnPickedUp()
    {
        var weaponComponent = _nearestPlayer.WeaponComponent;

        Utils.Instance.RemoveAllChildren(weaponComponent);
        weaponComponent.GiveWeapon(WeaponSceneId);
        QueueFree();
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent HitBox && HitBox.Entity is Player Player)
            _nearestPlayer = Player;
    }

    public void OnAreaExited(Area2D area)
    {
        if (area is HitboxComponent HitBox && HitBox.Entity is Player Player && _nearestPlayer == Player)
            _nearestPlayer = null;
    }
}