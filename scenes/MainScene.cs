using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
    private Dictionary<Vector2, Room> _roomsPositions;
    private Vector2 _currentRoomPosition;
    private Area2D[] _lastWaypoints;

    private bool _isWaypointAvailable = true;
    private Timer WaypointCooldownTimer;

    public override void _Ready()
    {
        WaypointCooldownTimer = new Timer();
        WaypointCooldownTimer.WaitTime = 1.0f;
        WaypointCooldownTimer.OneShot = true;
        WaypointCooldownTimer.Timeout += () => _isWaypointAvailable = true;
        AddChild(WaypointCooldownTimer);

        StateMachine.Instance.StateChanged += OnStateChanged;

        SetupCamera();

        StateMachine.Instance.State = State.FreeRoam;

        LayoutGenerator.GenerateLayout(GenerationSettings);
        GenerateLevel();
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

    public void GenerateLevel()
    {
        _currentLevelIndex = 1; // for now, always start at level 1

        _roomsPositions = new Dictionary<Vector2, Room>();
        _currentRoomPosition = new Vector2(-1, -1);

        var layout = LayoutGenerator.Layout;
        var levelRoomScenes = new Dictionary<RoomId, List<PackedScene>>();

        foreach (var roomScene in Levels.RoomScenes)
        {
            var room = roomScene.Instantiate<Room>();

            if ((int)room.Data.Location == _currentLevelIndex)
            {
                if (!levelRoomScenes.ContainsKey(room.Data.Type))
                    levelRoomScenes[room.Data.Type] = new List<PackedScene>();

                levelRoomScenes[room.Data.Type].Add(roomScene);
            }

            room.QueueFree();
        }

        for (int y = 0; y < layout.GetLength(0); y++)
        {
            for (int x = 0; x < layout.GetLength(1); x++)
            {
                var roomId = (RoomId)layout[y, x];

                if (roomId == RoomId.Empty) continue;

                if (!levelRoomScenes.ContainsKey(roomId)) continue;

                var room = Utils.Instance.GetRandomElementFromArray(levelRoomScenes[roomId].ToArray())
                    .Instantiate<Room>();
                AddChild(room);
                HideRoom(room);
                _roomsPositions[new Vector2(x, y)] = room;

                foreach (Waypoint waypoint in room.Waypoints)
                    waypoint.WaypointEntered += ChangeRoom;
            }
        }

        var entranceRoomPair = _roomsPositions.FirstOrDefault(r => r.Value.Data.Type == RoomId.Entrance);
        ShowRoom(entranceRoomPair.Value);

        _currentRoomPosition = entranceRoomPair.Key;
    }

    private void ChangeRoom(Area2D self, Area2D area)
    {
        if (!_isWaypointAvailable) return;

        var waypoint = self as Waypoint;

        var oldRoomPosition = _currentRoomPosition;
            
        var direction = waypoint.GetDirection();
        var newRoomPosition = _currentRoomPosition + direction;

        if (LayoutGenerator.IsPositionOutOfBounds(newRoomPosition))
        {
            GD.Print("No room in that direction.");
            return;
        }

        _currentRoomPosition = newRoomPosition;

        _roomsPositions.Keys.ToList().ForEach(key => GD.Print(key));

        var oldRoom = _roomsPositions[oldRoomPosition];
        var newRoom = _roomsPositions[_currentRoomPosition];

        // CallDeferred is essential due to CollisionObject design
        CallDeferred("HideRoom", oldRoom);
        CallDeferred("ShowRoom", newRoom);

        var newWaypoint = newRoom.Waypoints.FirstOrDefault(wp => (wp as Waypoint).Id == waypoint.GetOppositeWaypointId());

        if (area is HitboxComponent hitboxComponent && hitboxComponent.Entity is Player player)
        {
            player.GlobalPosition = newWaypoint.GlobalPosition;
            _isWaypointAvailable = false;
            WaypointCooldownTimer.Start();
        }

        GD.Print($"Current Room Position: {_currentRoomPosition}");
    }

    private void ShowRoom(Room room)
    {
        room.Visible = true;
        room.ProcessMode = ProcessModeEnum.Inherit;
    }

    private void HideRoom(Room room)
    {
        room.Visible = false;
        room.ProcessMode = ProcessModeEnum.Disabled;
    }
}