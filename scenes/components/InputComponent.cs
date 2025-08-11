using Godot;

public partial class InputComponent : Node2D
{
    [Export]
    public Player Player;

    [Export]
    public WeaponComponent WeaponComponent;

    public override void _Process(double delta)
    {
        HandleWeaponSwitch(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
    }

    private void HandleMovement(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Player.MovementComponent.Move(inputDirection, delta);
    }

    private void HandleWeaponSwitch(double delta)
    {
        if (Input.IsActionJustPressed("switch_weapon"))
            WeaponComponent.SwitchWeapon();
        if (Input.IsActionJustPressed("drop_weapon"))
            WeaponComponent.DropWeapon(WeaponComponent.SelectedIndex);
    }
}