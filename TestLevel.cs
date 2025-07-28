using Godot;
using System;

/// <summary>
/// Example level node that initializes the game state machine and sets up the camera.
/// </summary>
public partial class TestLevel : Node
{
    /// <summary>
    /// PackedScene reference to the Camera2D scene to instantiate.
    /// </summary>
    [Export]
    public PackedScene Camera2DScene;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Subscribes to game state changes, sets up the camera, and sets the initial game state.
    /// </summary>
    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        SetupCamera();

        GameStateMachine.Instance.State = GameStateMachine.GameState.FREE_ROAM;
    }

    /// <summary>
    /// Callback invoked when the game state changes.
    /// Logs the old and new states.
    /// </summary>
    /// <param name="oldState">Previous game state.</param>
    /// <param name="newState">Current game state.</param>
    private void OnStateChanged(GameStateMachine.GameState oldState, GameStateMachine.GameState newState)
    {
        GD.Print($"State changed from {oldState} to {newState}");
    }

    /// <summary>
    /// Instantiates and adds the Camera2D node to the scene, and makes it the current camera.
    /// </summary>
    private void SetupCamera()
    {
        var Camera2D = Camera2DScene.Instantiate() as Camera2D;
        AddChild(Camera2D);

        Camera2D.MakeCurrent();
    }
}