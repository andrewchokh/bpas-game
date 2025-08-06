using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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

    private int CurrentLevelIndex = 0;
    private Godot.Collections.Array<Room> Rooms = [];

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Subscribes to game state changes, sets up the camera, and sets the initial game state.
    /// </summary>
    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        GenerateLevel();

        SetupCamera();

        GameStateMachine.Instance.State = GameStateMachine.GameState.FREE_ROAM;
    }

    /// <summary>
    /// Callback invoked when the game state changes.
    /// Logs the old and new states.
    /// </summary>
    /// <param name="OldState">Previous game state.</param>
    /// <param name="NewState">Current game state.</param>
    private void OnStateChanged(GameStateMachine.GameState OldState, GameStateMachine.GameState NewState)
    {
        GD.Print($"State changed from {OldState} to {NewState}");
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

    private void GenerateLevel()
    {
        // var Settings = new LevelGeneratorSettings();

        // var Layout = LevelGenerator.Instance.GenerateLevel(Settings);
        // var LevelData = GetCurrentLevelData();

        // for (int y = 0; y < LevelGenerator.MAX_HEIGTH; y++)
        // {
        //     for (int x = 0; x < LevelGenerator.MAX_WIDTH; x++)
        //     {
        //         int RoomIndex = Layout[y, x];

        //         if (RoomIndex == 0) continue;

        //     }
        // }

        const int MaxRooms = 1;
        Godot.Collections.Array<Room> Rooms = [];

        var LevelData = GetCurrentLevelData();

        var EntranceRooms = LevelData[Levels.RoomType.ENTRANCE];
        var EntranceRoom = EntranceRooms[GD.RandRange(0, EntranceRooms.Length - 1)].Instantiate() as EntranceRoom;
        AddChild(EntranceRoom);
        Rooms.Add(EntranceRoom);


        int RoomsPlaced = 0;
        while (RoomsPlaced < MaxRooms)
        {
            var BattleRooms = LevelData[Levels.RoomType.BATTLE];
            var Room = BattleRooms[GD.RandRange(0, BattleRooms.Length - 1)].Instantiate() as BattleRoom;

            Marker2D WayPoint1 = null;
            Marker2D WayPoint2 = null;

            const int MaxTries = 1000;
            int Tries = 0;
            while (Tries < MaxTries)
            {
                Tries++;
                Room = BattleRooms[GD.RandRange(0, BattleRooms.Length - 1)].Instantiate() as BattleRoom;
                WayPoint1 = SelectRandomWayPoint(EntranceRoom);
                WayPoint2 = SelectRandomWayPoint(Room);

                GD.Print(WayPoint1.Name, WayPoint2.Name);

                if (WayPoint1.Name == "North" && WayPoint2.Name == "South")
                {
                    break;
                }
                if (WayPoint1.Name == "South" && WayPoint2.Name == "North")
                {
                    break;
                }
                if (WayPoint1.Name == "West" && WayPoint2.Name == "East")
                {
                    break;
                }
                if (WayPoint1.Name == "East" && WayPoint2.Name == "West")
                {
                    break;
                }
            }

            GD.Print(WayPoint1.GlobalPosition, WayPoint2.GlobalPosition);

            Room.GlobalPosition = WayPoint1.GlobalPosition;
            Room.GlobalPosition -= WayPoint2.GlobalPosition;

            AddChild(Room);

            RoomsPlaced++;
        }
    }

    private Marker2D[] GetCompatibleWayPoints(Room Room1, Room Room2)
    {
        Marker2D WayPoint1 = null;
        Marker2D WayPoint2 = null;

        const int MaxTries = 1000;
        int Tries = 0;
        while (Tries < MaxTries)
        {
            Tries++;
            WayPoint1 = SelectRandomWayPoint(Room1);
            WayPoint2 = SelectRandomWayPoint(Room2);

            GD.Print(WayPoint1.Name, WayPoint2.Name);

            if (WayPoint1.Name == "North" && WayPoint2.Name == "South")
            {
                return [WayPoint1, WayPoint2];
            }
            if (WayPoint1.Name == "South" && WayPoint2.Name == "North")
            {
                return [WayPoint1, WayPoint2];
            }
            if (WayPoint1.Name == "West" && WayPoint2.Name == "East")
            {
                return [WayPoint1, WayPoint2];
            }
            if (WayPoint1.Name == "East" && WayPoint2.Name == "West")
            {
                return [WayPoint1, WayPoint2];
            }
        }

        return [WayPoint1, WayPoint2];
    }

    private Marker2D SelectRandomWayPoint(Room Room)
    {
        return Room.WayPoints[GD.RandRange(0, Room.WayPoints.Length - 1)];
    }

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
