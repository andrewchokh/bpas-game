using Godot;
using System;

public partial class OptionsMenu : Control
{
    [Signal]
    public delegate void BackButtonPressedEventHandler();
    [ExportCategory("Buttons")]
    [Export]
    public Button BackToMainMenuButton;

    public override void _Ready()
    {
        BackToMainMenuButton.Pressed += () => EmitSignal(SignalName.BackButtonPressed);
    }
}