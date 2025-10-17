using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class MainMenu : Control
{
    [Export]
    public MarginContainer MarginContainer;
    [Export]
    public OptionsMenu OptionsMenu;
    [Export]
    public CreditsMenu CreditsMenu;
    [ExportCategory("Buttons")]
    [Export]
    public Button PlayButton;
    [Export]
    public Button OptionsButton;
    [Export]
    public Button CreditsButton;
    [Export]
    public Button QuitButton;

    public override void _Ready()
    {
        PlayButton.Pressed += () =>
            GetTree().ChangeSceneToFile("res://scenes/Game.tscn");

        OptionsButton.Pressed += () =>
        {
            MarginContainer.Visible = false;
            OptionsMenu.SetProcess(true);
            OptionsMenu.Visible = true;
        };
        OptionsMenu.BackButtonPressed += () =>
        {
            OptionsMenu.Visible = false;
            OptionsMenu.SetProcess(false);
            MarginContainer.Visible = true;
        };
        CreditsButton.Pressed += () =>
        {
            MarginContainer.Visible = false;
            CreditsMenu.SetProcess(true);
            CreditsMenu.Visible = true;
        };
        CreditsMenu.BackButtonPressed += () =>
        {
            CreditsMenu.Visible = false;
            CreditsMenu.SetProcess(false);
            MarginContainer.Visible = true;
        };
        QuitButton.Pressed += () => Globals.Instance.Root.Quit();
    }
}