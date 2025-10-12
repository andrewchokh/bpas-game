using System;
using Godot;
public partial class PauseMenu : Control
{
    [Export]
    public ConfirmationPopup ConfirmationPopup;
    [Export]
    public PanelContainer PanelContainer;
    [ExportCategory("Buttons")]
    [Export]
    public Button ResumeButton;
    [Export]
    public Button BackToMainMenuButton;
    [Export]
    public Button QuitButton;

    public override void _Ready()
    {
        QuitButton.Pressed += () =>
        {
            PanelContainer.Visible = false;
            ConfirmationPopup.ShowPopup("Are you sure you want to quit?",
                onYesAction: () => Globals.Instance.Root.Quit(),
                onNoAction: () => PanelContainer.Visible = true);
        };
        ResumeButton.Pressed += () =>
        {
            Globals.Instance.Root.Paused = false;
            Visible = false;
            GameStateMachine.Instance.State = GameState.FreeRoam;
        };
        BackToMainMenuButton.Pressed += () =>
        {
            PanelContainer.Visible = false;
            ConfirmationPopup.ShowPopup("Are you sure you want to go back to the main menu?",
                onYesAction: () =>
                {
                    Globals.Instance.Root.ChangeSceneToFile(Constants.MainMenuScenePath);
                    Globals.Instance.Root.Paused = false;
                    GameStateMachine.Instance.State = 0;
                },
                onNoAction: () => PanelContainer.Visible = true);
        };
    }
    public override void _Process(double delta) => HandleInput();

    private void HandleInput()
    {
        if (!Input.IsActionJustPressed("pause")) return;

        bool paused = Globals.Instance.Root.Paused;
        Globals.Instance.Root.Paused = !paused;
        Visible = !paused;
        GameStateMachine.Instance.State = paused ? GameState.FreeRoam : GameState.Pause;
    }
}