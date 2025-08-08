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
    private Marker2D[] LastWaypoints;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Subscribes to game state changes, sets up the camera, and sets the initial game state.
    /// </summary>
    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        GenerateLevel(new LevelGeneratorSettings());

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

    private void GenerateLevel(LevelGeneratorSettings settings)
    {
        // var levelData = GetCurrentLevelData();

        // var EntranceRooms = LevelData[Levels.RoomType.ENTRANCE];
        // var EntranceRoom = EntranceRooms[GD.RandRange(0, EntranceRooms.Length - 1)].Instantiate() as EntranceRoom;

        // PlaceRoom(EntranceRoom, Vector2.Zero, Vector2.Zero);

        foreach (var roomType in settings.RoomCount.Keys)
        {
            for (int i = 0; i < settings.RoomCount[roomType]; i++)
            {
                var room = GetRandomRoom(roomType);
                if (room == null) continue;

                if (Rooms.Count == 0)
                    PlaceRoom(room, Vector2.Zero, Vector2.Zero);
                else
                    ConnectRooms(Rooms.Last(), room);
            }
        }


        // while (Rooms.Count < settings.MaxRoomCount)
        // {
        //     var BattleRooms = LevelData[Levels.RoomType.BATTLE];
        //     var Room = BattleRooms[GD.RandRange(0, BattleRooms.Length - 1)].Instantiate() as BattleRoom;

        //     Marker2D[] Waypoints = [];
        //     int tries = 0;
        //     while (tries < settings.MaxTries)
        //     {
        //         Waypoints = GetCompatibleWaypoints(Rooms.Last(), Room);

        //         if (Waypoints.Length > 0) break;
        //     }

        //     PlaceRoom(Room, Waypoints[0].GlobalPosition, Waypoints[1].GlobalPosition);
        // }
    }

    private void PlaceRoom(Room room, Vector2 position, Vector2 offset)
    {
        room.GlobalPosition = position;
        room.GlobalPosition -= offset;

        AddChild(room);
        Rooms.Add(room);
    }
    
    private void ConnectRooms(Room room1, Room room2, int maxTries = 100)
    {
        Marker2D[] Waypoints = [];

        int tries = 0;
        while (tries < maxTries)
        {
            Waypoints = GetCompatibleWaypoints(room1, room2);
            if (Waypoints.Length > 0) break;
            tries++;
        }

        GD.Print(Waypoints);

        PlaceRoom(room2, Waypoints[0].GlobalPosition, Waypoints[1].GlobalPosition);
    }

    private Room GetRandomRoom(Levels.RoomType roomType)
    {
        var levelData = GetCurrentLevelData();
        var rooms = levelData[roomType];

        if (rooms.Length == 0) return null;

        return rooms[GD.RandRange(0, rooms.Length - 1)].Instantiate() as Room;
    }

    private Marker2D[] GetCompatibleWaypoints(Room room1, Room room2, int maxTries = 100)
    {
        Marker2D Waypoint1 = null;
        Marker2D Waypoint2 = null;

        int tries = 0;
        while (tries < maxTries)
        {
            tries++;

            Waypoint1 = room1.SelectRandomWaypoint();
            Waypoint2 = room2.SelectRandomWaypoint();

            LastWaypoints = [Waypoint1, Waypoint2];

            GD.Print(Waypoint1.Name, Waypoint2.Name);

            if (Waypoint1.Name == "North" && Waypoint2.Name != "South") continue;
            else if (Waypoint1.Name == "South" && Waypoint2.Name != "North") continue;
            else if (Waypoint1.Name == "West" && Waypoint2.Name != "East") continue;
            else if (Waypoint1.Name == "East" && Waypoint2.Name != "West") continue;

            room2.Waypoints = room2.Waypoints
                .Where(w => w.Name != Waypoint2.Name)
                .ToArray();

            return LastWaypoints;
        }

        return [];
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

public partial class LevelGeneratorSettings
{
    public int MaxRoomCount { get; set; } = 5;
    public int MaxTries { get; set; } = 1000;
    public Dictionary<Levels.RoomType, int> RoomCount = new()
    {
        { Levels.RoomType.ENTRANCE, 1 },
        { Levels.RoomType.BATTLE, 3 },
        { Levels.RoomType.TUNNEL, 1 },
        { Levels.RoomType.BOSS, 1 },
        { Levels.RoomType.TREASURE, 1 }
    };
}