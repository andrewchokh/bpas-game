using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TestLevel : Node
{
    [Export]
    public PackedScene Camera2DScene;
    [Export]
    public LevelGenerationManager LevelGenerationManager;

    private int CurrentLevelIndex = 0;
    private Godot.Collections.Array<Room> Rooms = [];

    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        GenerateLevel();

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

    private void GenerateLevel()
    {
        var LevelData = GetCurrentLevelData();

        var TestRoomScene = LevelData[Levels.RoomType.ENTRANCE][0];

        LevelGenerationManager.GenerateLevelLayout();

        for (int x = 0; x < LevelGenerationManager.Width; x++)
        {
            for (int y = 0; y < LevelGenerationManager.Height; y++)
            {
                if (LevelGenerationManager.Layout[x, y] == 1) // Assuming 1 indicates a room
                {
                    GD.Print($"Generating room at ({new Vector2(x * 300, y * 300)})");
                    var RoomInstance = TestRoomScene.Instantiate() as Room;
                    RoomInstance.Position = new Vector2(x * 300, y * 300); // Adjust position based on your layout logic
                    Rooms.Add(RoomInstance);
                    AddChild(RoomInstance);
                }
            }
        }

        var Bob = EntityList.PlayerScenes[EntityList.PlayerType.Bob].Instantiate() as Player;

        AddChild(Bob);
    }

    // private void GenerateLevelEntrance(Dictionary<Levels.RoomType, PackedScene[]> LevelData)
    // {
    //     var EntranceRoomScene = LevelData[Levels.RoomType.ENTRANCE][0];

    //     var EntranceRoom = EntranceRoomScene.Instantiate() as EntranceRoom;

    //     Rooms.Add(EntranceRoom);

    //     AddChild(EntranceRoom);
    // }

    private Dictionary<Levels.RoomType, PackedScene[]> GetCurrentLevelData()
    {
        switch (CurrentLevelIndex)
        {
            case 0:
                return Levels.LevelScenes[Levels.LocationType.FOREST];
            case 1:
                return Levels.LevelScenes[Levels.LocationType.DUNGEON];
            default:
                GD.PrintErr("Invalid level index");
                return null;
        }
    }
}
