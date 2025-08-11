using System.Linq;
using Godot;
using System.Collections.Generic;

using static LevelsData;

public partial class TestLevel : Node
{
    [Export]
    public PackedScene Camera2DScene;

    private int CurrentLevelIndex = 0;
    private Godot.Collections.Array<Room> Rooms = [];
    private Marker2D[] LastWaypoints;

    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        GenerateLevel(new LevelGeneratorSettings());

        SetupCamera();

        GameStateMachine.Instance.State = GameStateMachine.GameState.FREE_ROAM;
    }

    private void OnStateChanged(GameStateMachine.GameState oldState, GameStateMachine.GameState newState)
    {
        GD.Print($"State changed from {oldState} to {newState}");
    }

    private void SetupCamera()
    {
        var camera2D = Camera2DScene.Instantiate() as Camera2D;
        AddChild(camera2D);

        camera2D.MakeCurrent();
    }

    private void GenerateLevel(LevelGeneratorSettings settings)
    {
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

    private Room GetRandomRoom(RoomType roomType)
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

    private Dictionary<RoomType, PackedScene[]> GetCurrentLevelData()
    {
        switch (CurrentLevelIndex)
        {
            case 0:
                return LevelsData.Instance.LevelScenes[LocationId.FOREST];
            case 1:
                return LevelsData.Instance.LevelScenes[LocationId.DUNGEON];
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

    public Dictionary<RoomType, int> RoomCount = new()
    {
        { RoomType.ENTRANCE, 1 },
        { RoomType.BATTLE, 3 },
        { RoomType.TUNNEL, 1 },
        { RoomType.BOSS, 1 },
        { RoomType.TREASURE, 1 }
    };
}