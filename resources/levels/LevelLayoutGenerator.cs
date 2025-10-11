using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

using static Ids;

[GlobalClass]
public partial class LevelLayoutGenerator : Resource
{
    public int[,] Layout { get; private set; }
    public Vector2 Cursor { get; private set; }

    public void GenerateLayout(LevelGenerationSettings settings, int level = 1)
    {
        MakeFloorplan(settings, level);
        PlaceRoomsOnFloorplan(settings);
    }

    private void MakeFloorplan(LevelGenerationSettings settings, int level)
    {
        Layout = new int[(int)settings.LayoutSize.Y, (int)settings.LayoutSize.X];

        Cursor = GetCenterPosition();
        MarkRoom(Cursor);

        int roomCount = GD.RandRange(0, 2) + 5 + level * 2;
        int roomsPlaced = 1;

        while (roomsPlaced <= roomCount)
        {
            var freeNeighbors = new List<Vector2>();
            foreach (var (position, isFree) in CheckNeighbors(Cursor))
            {
                if (isFree)
                    freeNeighbors.Add(position);
            }

            if (freeNeighbors.Count == 0)
                break; // no more space

            // Place up to 2 rooms per step
            int roomsPerStep = Math.Min(2, roomCount - roomsPlaced);
            var placedPerStep = new List<Vector2>();

            for (int i = 0; i < roomsPerStep && freeNeighbors.Count > 0; i++)
            {
                int randomIndex = GD.RandRange(0, freeNeighbors.Count - 1);
                var position = freeNeighbors[randomIndex];
                freeNeighbors.RemoveAt(randomIndex);

                MarkRoom(position);
                placedPerStep.Add(position);
                roomsPlaced++;
            }

            Cursor = Utils.Instance.GetRandomElementFromArray(placedPerStep.ToArray());
        }
    }

    private void PlaceRoomsOnFloorplan(LevelGenerationSettings settings)
    {
        Cursor = GetCenterPosition();
        var deadEnds = GetDeadEnds();

        // Place entrance room at center
        Layout[(int)Cursor.Y, (int)Cursor.X] = (int)RoomId.Entrance;

        // Place boss room at a random dead end
        var bossRoomPosition = Utils.Instance.GetRandomElementFromArray(deadEnds.ToArray());
        Layout[(int)bossRoomPosition.Y, (int)bossRoomPosition.X] = (int)RoomId.Boss;
        deadEnds.Remove(bossRoomPosition);

        // Place special rooms
        foreach (var (roomId, count) in settings.SpecialRooms)
        {
            for (int i = 0; i < count; i++)
            {
                var roomPosition = GetRandomRoom(RoomId.Empty);
                if (roomPosition != new Vector2(-1, -1))
                {
                    Layout[(int)roomPosition.Y, (int)roomPosition.X] = (int)roomId;
                }
            }
        }

        // Filling remaining empty rooms with battle rooms
        for (int y = 0; y < Layout.GetLength(0); y++)
        {
            for (int x = 0; x < Layout.GetLength(1); x++)
            {
                if (Layout[y, x] == (int)RoomId.Empty)
                    Layout[y, x] = (int)RoomId.Battle;
            }
        }
    }

    private Dictionary<Vector2, bool> CheckNeighbors(Vector2 position)
    {
        Dictionary<Vector2, bool> neighborsInfo = new();

        try
        {
            for (int y = (int)position.Y - 1; y < (int)position.Y + 2; y++)
            {
                for (int x = (int)position.X - 1; x < (int)position.X + 2; x++)
                {
                    var neighborPosition = new Vector2(x, y);
                    if (neighborPosition == position)
                        continue;

                    // Exclude diagonals
                    if (x == (int)position.X && y == (int)position.Y ||
                    x != (int)position.X && y != (int)position.Y)
                        continue;

                    neighborsInfo[neighborPosition] = IsPositionFree(neighborPosition);
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            // Ignore out of bounds
        }

        return neighborsInfo;
    }

    public List<Vector2> GetDeadEnds()
    {
        var deadEnds = new List<Vector2>();

        for (int y = 0; y < Layout.GetLength(0); y++)
        {
            for (int x = 0; x < Layout.GetLength(1); x++)
            {
                if (Layout[y, x] > 0)
                {
                    int occupiedNeighborCount = 0;
                    foreach (var (position, isFree) in CheckNeighbors(new Vector2(x, y)))
                    {
                        if (!isFree)
                            occupiedNeighborCount++;
                    }

                    if (occupiedNeighborCount == 1)
                        deadEnds.Add(new Vector2(x, y));
                }
            }
        }

        return deadEnds;
    }

    private Vector2 GetRandomRoom(RoomId roomId = 0)
    {
        int width = Layout.GetLength(1);
        int height = Layout.GetLength(0);

        const int maxTries = 1000;
        for (var t = 0; t < maxTries; t++)
        {
            int randomX = GD.RandRange(0, width - 1);
            int randomY = GD.RandRange(0, height - 1);

            if (Layout[randomY, randomX] == 0)
                continue;
            
            if (roomId != 0 && Layout[randomY, randomX] != (int)roomId)
                continue;

            return new Vector2(randomX, randomY);
        }
        
        return new Vector2(-1, -1); // Failed to find a room
    }

    private void MarkRoom(Vector2 position)
    {
        Layout[(int)position.Y, (int)position.X] = (int)RoomId.Empty;
    }

    private bool IsPositionFree(Vector2 position)
    {
        return Layout[(int)position.Y, (int)position.X] == 0;
    }

    private Vector2 GetCenterPosition()
    {
        int width = Layout.GetLength(1);
        int height = Layout.GetLength(0);
        return new Vector2(Mathf.Floor(width / 2f), Mathf.Floor(height / 2f));
    }

    private void PrintLayout()
    {
        for (int y = 0; y < Layout.GetLength(0); y++)
        {
            var str = "";
            for (int x = 0; x < Layout.GetLength(1); x++)
            {
                str += Layout[y, x] + " ";
            }

            GD.Print(str);
        }
    }
    public bool IsPositionOutOfBounds(Vector2 position)
    {
        try
        {
            return Layout[(int)position.Y, (int)position.X] == 0;
        }
        catch (IndexOutOfRangeException)
        {
            GD.Print("Position is out of bounds.");
            return true;
        }
    }

}
