using Godot;
using Godot.Collections;
using System;

using static Ids;

public partial class MainScene : Node
{
    [Export]
    public PackedScene MainCameraScene;

    [Export]
    public LevelDatabase Levels;
    [Export]
    public LevelGenerationSettings GenerationSettings;
    [Export]
    public LevelLayoutGenerator LayoutGenerator;

    private int _currentLevelIndex = 0;
    private Godot.Collections.Array<Room> _rooms = [];
    private Marker2D[] _lastWaypoints;

    public override void _Ready()
    {
        // StateMachine.Instance.StateChanged += OnStateChanged;

        // GenerateLevel(GenerationSettings);

        // SetupCamera();

        // StateMachine.Instance.State = State.FreeRoam;

        LayoutGenerator.GenerateLayout(GenerationSettings);
    }

    private void OnStateChanged(State oldState, State newState)
    {
        GD.Print($"State changed from {oldState} to {newState}");
    }

    private void SetupCamera()
    {
        var camera2D = MainCameraScene.Instantiate() as Camera2D;
        AddChild(camera2D);

        camera2D.MakeCurrent();
    }

    public void GenerateLevel(LevelGenerationSettings settings)
    {

    }
}