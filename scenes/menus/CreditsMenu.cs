using Godot;
using System;

public partial class CreditsMenu : Control
{
    [Signal]
    public delegate void BackButtonPressedEventHandler();
    [ExportCategory("Buttons")]
    [Export]
    public Button BackButton;

    public override void _Ready()
    {
        BackButton.Pressed += () =>
        {
            EmitSignal(SignalName.BackButtonPressed);
        };
    }
}