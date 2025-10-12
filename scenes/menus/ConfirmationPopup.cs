using System;
using Godot;

public partial class ConfirmationPopup : Control
{
    [Export]
    public Label MessageLabel;
    [ExportCategory("Buttons")]
    [Export]
    public Button YesButton;
    [Export]
    public Button NoButton;

    private Action _onYesAction;
    private Action _onNoAction;

    public override void _Ready()
    {
        Visible = false;
        YesButton.Pressed += () =>
        {
            Visible = false;
            _onYesAction?.Invoke();
        };
        NoButton.Pressed += () =>
        {
            Visible = false;
            _onNoAction?.Invoke();
        };
    }
    public void ShowPopup(string message, Action onYesAction = null, Action onNoAction = null)
    {
        MessageLabel.Text = message;
        _onYesAction = onYesAction;
        _onNoAction = onNoAction;

        Visible = true;
    }
}