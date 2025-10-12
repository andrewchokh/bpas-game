using Godot;
using System;

public partial class GameOverMenu : Control
{
    [Export]
    public ConfirmationPopup ConfirmationPopup;
    [Export]
    public PanelContainer PanelContainer;
    [ExportCategory("Buttons")]
    [Export]
    public Button RetryButton;
    [Export]
    public Button BackToMainMenuButton;
    [Export]
    public Button QuitButton;

    public override void _Ready()
    {
        var player = Utils.Instance.GetFirstPlayer();

        if (player == null)
        {
            GD.PrintErr("Player not found in the scene tree.");
            return;
        }

        player.HealthComponent.EntityDestroyed += () =>
        {
            Visible = true;
            Globals.Instance.Root.Paused = true;
            GameStateMachine.Instance.State = GameState.GameOver;
        };
        RetryButton.Pressed += () =>
        {
            Globals.Instance.Root.ReloadCurrentScene();
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
        QuitButton.Pressed += () =>
        {
            PanelContainer.Visible = false;
            ConfirmationPopup.ShowPopup("Are you sure you want to quit?",
                onYesAction: () => Globals.Instance.Root.Quit(),
                onNoAction: () => PanelContainer.Visible = true);
        };
    }
}