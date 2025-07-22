using Godot;
using System;

using System.Collections.Generic;

using static Godot.Mathf;

public partial class LevelGenerationManager : Node
{
    [Export] public int Width = 10;
    [Export] public int Height = 10;
    [Export] public int RoomCount = 10; // Total number of rooms

    public readonly Vector2 UpDirection = new Vector2(0, -1);
    public readonly Vector2 DownDirection = new Vector2(0, 1);
    public readonly Vector2 LeftDirection = new Vector2(-1, 0);
    public readonly Vector2 RightDirection = new Vector2(1, 0);

    public int[,] Layout { get; private set; } // 2D array to represent the level layout

    private Vector2 PointerPosition;
    private Vector2 LastPointerPosition;

    public int[,] GenerateLevelLayout()
    {
        Layout = new int[Width, Height];
        PointerPosition = new Vector2(Round(Width / 2), Round(Height / 2));

        int RoomsPlaced = 0;
        int Tries = 0; // Prevent infinite loops
        while (RoomsPlaced < RoomCount)
        {
            var Direction = GetRandomDirection();

            GD.Print($"Pointer: {PointerPosition}, Direction: {Direction}");

            if (!IsPositionFree(PointerPosition + Direction))
            {
                PointerPosition = LastPointerPosition;
                Tries++;

                if (Tries > 100) // If we can't place a room after many tries, break to avoid infinite loop
                {
                    GD.PrintErr("Too many tries, breaking out of the loop.");
                    break;
                }

                continue;
            }

            GD.Print($"Placing room at: {PointerPosition + Direction}");

            LastPointerPosition = PointerPosition;

            MovePointer(Direction);
            PlaceRoom();
            RoomsPlaced++;
        }

        return Layout;
    }

    private void MovePointer(Vector2 Direction)
    {
        try
        {
            PointerPosition += Direction;
        }
        catch (Exception e)
        {
            GD.PrintErr($"Error moving pointer: {e.Message}");
        }

    }

    private void PlaceRoom()
    {
        try
        {
            if (Layout[(int)PointerPosition.X, (int)PointerPosition.Y] == 1)
                return;

            Layout[(int)PointerPosition.X, (int)PointerPosition.Y] = 1;
        }
        catch (IndexOutOfRangeException e)
        {
            GD.PrintErr($"Error moving pointer: {e.Message}. Index out of range at position: {PointerPosition}");
        }

    }

    private bool IsPositionFree(Vector2 Position)
    {
        try
        {
            var NewPositionValue = Layout[(int)Position.X, (int)Position.Y];

            return NewPositionValue == 1 ? false : true;
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
    }

    private bool IsSurroundingPositionsFree()
    {
        return true;
    }

    private Vector2 GetRandomDirection()
    {
        Vector2[] Directions = { UpDirection, DownDirection, LeftDirection, RightDirection };
        return Directions[GD.RandRange(0, Directions.Length - 1)];
    }
}

