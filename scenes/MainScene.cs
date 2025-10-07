using Godot;
using System.Linq;
using System.Collections.Generic;

using static LevelsData;
using System;

public partial class MainScene : Node
{
    [Export]
    public PackedScene MainCameraScene;

    private int _currentLevelIndex = 0;
    private Godot.Collections.Array<Room> _rooms = [];
    private Marker2D[] _lastWaypoints;

    public override void _Ready()
    {
        GameStateMachine.Instance.StateChanged += OnStateChanged;

        GenerateLevel(new LevelGeneratorSettings());

        SetupCamera();

        GameStateMachine.Instance.State = GameState.FreeRoam;
    }

    private void OnStateChanged(GameState oldState, GameState newState)
    {
        GD.Print($"State changed from {oldState} to {newState}");
    }

    private void SetupCamera()
    {
        var camera2D = MainCameraScene.Instantiate() as Camera2D;
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

                if (_rooms.Count == 0)
                    PlaceRoom(room, Vector2.Zero, Vector2.Zero);
                else
                {
                    int tries = 0;
                    while (tries < settings.MaxTries)
                    {
                        if (tries > 0) room = GetRandomRoom(roomType);

                        try
                        {
                            ConnectRooms(_rooms.Last(), room);
                            break;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                        finally
                        {
                            tries++;
                        }
                    }
                }
                    
                _rooms.Add(room);
            }
        }
    }

    private void PlaceRoom(Room room, Vector2 position, Vector2 offset)
    {
        room.GlobalPosition = position;
        room.GlobalPosition -= offset;

        AddChild(room);
    }

    private void ConnectRooms(Room room1, Room room2, int maxTries = 10)
    {
        Marker2D[] waypoints = [];

        int tries = 0;
        while (tries < maxTries)
        {
            waypoints = GetCompatibleWaypoints(room1, room2);

            if (waypoints.Length == 2) break;

            tries++;
        }

        PlaceRoom(room2, waypoints[0].GlobalPosition, waypoints[1].GlobalPosition);
    }

    private Room GetRandomRoom(RoomType roomType)
    {
        var levelData = GetCurrentLevelData();
        var rooms = levelData[roomType];

        if (rooms.Length == 0) return null;

        return rooms[GD.RandRange(0, rooms.Length - 1)].Instantiate() as Room;
    }

    private RoomType GetRoomType(Room room)
    {
        switch (room)
        {
            case EntranceRoom:
                return RoomType.ENTRANCE;
            case BattleRoom:
                return RoomType.BATTLE;
            case TunnelRoom:
                return RoomType.TUNNEL;
        }

        return 0;
    }

    private Marker2D[] GetCompatibleWaypoints(Room room1, Room room2, int maxTries = 10)
    {
        Marker2D Waypoint1 = null;
        Marker2D Waypoint2 = null;

        int tries = 0;
        while (tries < maxTries)
        {
            tries++;

            Waypoint1 = room1.SelectRandomWaypoint();
            Waypoint2 = room2.SelectRandomWaypoint();

            _lastWaypoints = [Waypoint1, Waypoint2];

            GD.Print($"{tries} / {maxTries}: Trying to connect {Waypoint1.Name} with {Waypoint2.Name}");

            switch (Waypoint1.Name)
            {
                case Constants.NorthWaypointName:
                    if (Waypoint2.Name == Constants.SouthWaypointName)
                    {
                        GD.Print("Connected " + Waypoint1.Name + " with " + Waypoint2.Name);
                        break;
                    }
                    continue;
                case Constants.SouthWaypointName:
                    if (Waypoint2.Name == Constants.NorthWaypointName)
                    {
                        GD.Print("Connected " + Waypoint1.Name + " with " + Waypoint2.Name);
                        break;
                    }
                    continue;
                case Constants.WestWaypointName:
                    if (Waypoint2.Name == Constants.EastWaypointName)
                    {
                        GD.Print("Connected " + Waypoint1.Name + " with " + Waypoint2.Name);
                        break;
                    }
                    continue;
                case Constants.EastWaypointName:
                    if (Waypoint2.Name == Constants.WestWaypointName)
                    {
                        GD.Print("Connected " + Waypoint1.Name + " with " + Waypoint2.Name);
                        break;
                    }
                    continue;
                default:
                    GD.PrintErr("Unknown waypoint name: " + Waypoint1.Name);
                    continue;
            }

            room2.Waypoints = room2.Waypoints
                .Where(w => w.Name != Waypoint2.Name)
                .ToArray();


            return _lastWaypoints;
        }

        return [];
    }

    private Dictionary<RoomType, PackedScene[]> GetCurrentLevelData()
    {
        switch (_currentLevelIndex)
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
    public int MaxTries { get; set; } = 1000;

    public Dictionary<RoomType, int> RoomCount = new()
    {
        { RoomType.ENTRANCE, 1 },
        { RoomType.BATTLE, 2 },
        { RoomType.TUNNEL, 0 },
        { RoomType.BOSS, 0 },
        { RoomType.TREASURE, 0 }
    };
}