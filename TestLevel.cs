using Godot;
using System;

public partial class TestLevel : Node
{
    [Export]
    public PackedScene Camera2DScene;

    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        SetupCamera();

        GameStateMachine.Instance.State = GameStateMachine.GameState.FREE_ROAM;
    }

    private void OnStateChanged(GameStateMachine.GameState oldState, GameStateMachine.GameState newState)
    {
        GD.Print($"State changed from {oldState} to {newState}");
    }

    private void SetupCamera()
    {
        var Camera2D = Camera2DScene.Instantiate() as Camera2D;
        AddChild(Camera2D);

        Camera2D.MakeCurrent();
    }
}