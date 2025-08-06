using Godot;
using System;
using System.Collections.Generic;

using static Godot.Mathf;

public partial class LevelGenerator : Node
{
    public static LevelGenerator Instance { get; private set; }

    public const int MAX_WIDTH = 64;
    public const int MAX_HEIGTH = 64;

    public int[,] Layout;

    public override void _Ready()
    {
        Instance = this;
    }

    public int[,] GenerateLevel(LevelGeneratorSettings Settings)
    {
        Layout = new int[MAX_HEIGTH, MAX_WIDTH];

        GD.Print(Settings.RoomsCount);

        var PointerPosition = new Vector2(Floor(MAX_HEIGTH / 2), Floor(MAX_WIDTH / 2));
        var LastPointerPosition = PointerPosition;

        Vector2[] Directions = [Vector2.Left, Vector2.Right, Vector2.Up, Vector2.Down];

        var AvaliableRooms = Settings.RoomsCount;

        int RoomCount = GetMaxRoomCount(Settings);
        int RoomsPlaced = 0;

        while (RoomsPlaced < RoomCount)
        {
            var Direction = Directions[GD.RandRange(0, Directions.Length - 1)];

            GD.Print($"The direction is: {Direction}");

            if (!IsPositionFree(PointerPosition + Direction))
            {
                GD.Print($"No free position at {PointerPosition + Direction}");
                PointerPosition = LastPointerPosition;
                continue;
            }

            LastPointerPosition = PointerPosition;
            PointerPosition += Direction;

            GD.Print($"Moved at {PointerPosition}");

            Layout[(int)PointerPosition.Y, (int)PointerPosition.X] = GetNextRoomIndex(AvaliableRooms);

            GD.Print($"Placed room at {(int)PointerPosition.Y}, {(int)PointerPosition.X}");

            RoomsPlaced++;
        }

        PrintMatrix();
        return Layout;
    }

    private int GetMaxRoomCount(LevelGeneratorSettings Settings)
    {
        GD.Print(Settings.RoomsCount);
        int RoomCount = 0;
        foreach (int Count in Settings.RoomsCount.Values)
        {
            RoomCount += Count;
        }

        return RoomCount;
    }

    private void PlaceRoom(Vector2 Position, Levels.RoomType RoomIndex)
    {
        Layout[(int)Position.Y, (int)Position.X] = (int)RoomIndex;
    }

    private bool IsPositionFree(Vector2 Position)
    {
        try
        {
            var NewPositionValue = Layout[(int)Position.Y, (int)Position.X];

            return NewPositionValue == 0 ? true : false;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    private int GetNextRoomIndex(Dictionary<Levels.RoomType, int> RoomsCount)
    {
        foreach (var Item in RoomsCount)
        {
            GD.Print(Item);
            if (Item.Value != 0)
            {
                GD.Print($"Apply {Item.Key}");
                RoomsCount[Item.Key]--;
                GD.Print((int)Item.Key);
                return (int)Item.Key;
            }
        }

        return 0;
    }
    
    private void PrintMatrix()
    {
        for (int y = 0; y < MAX_HEIGTH; y++)
        {
            string row = "";
            for (int x = 0; x < MAX_WIDTH; x++)
            {
                row += Layout[y, x] != 0 ? $"[{Layout[y, x]}]" : "[ ]";
            }
            GD.Print(row);
        }
    }
}

public class LevelGeneratorSettings
{
    public string Seed;
    public Dictionary<Levels.RoomType, int> RoomsCount = new()
    {
        { Levels.RoomType.ENTRANCE, 1 },
        { Levels.RoomType.BATTLE, 3 },
        { Levels.RoomType.BOSS, 1 }
        // Warning: Do NOT specify tunnel rooms count
    };
}