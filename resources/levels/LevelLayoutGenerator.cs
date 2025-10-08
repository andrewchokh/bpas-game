using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

[GlobalClass]
public partial class LevelLayoutGenerator : Resource
{
    public int[,] GenerateLayout(LevelGenerationSettings settings, int level = 1)
    {
        int width = (int)settings.LayoutSize.X;
        int height = (int)settings.LayoutSize.Y;
        int[,] layout = new int[height, width];

        Vector2 cursor = new(Mathf.Floor(width / 2f), Mathf.Floor(height / 2f));
        PlaceRoom(layout, cursor);
        
        int roomCount = GD.RandRange(0, 2) + 5 + level * 2;
        int roomsPlaced = 1;

        while (roomsPlaced <= roomCount)
        {
            var freeNeighbors = new List<Vector2>();
            foreach (var (position, isFree) in CheckNeighbors(layout, cursor))
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
                var pos = freeNeighbors[randomIndex];
                freeNeighbors.RemoveAt(randomIndex);

                PlaceRoom(layout, pos);
                placedPerStep.Add(pos);
                roomsPlaced++;
            }
            cursor = Utils.Instance.GetRandomElementFromArray(placedPerStep.ToArray());
        }

        PrintLayout(layout);
        return layout;
    }

    private Dictionary<Vector2, bool> CheckNeighbors(int[,] layout, Vector2 position)
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

                    neighborsInfo[neighborPosition] = IsPositionFree(layout, neighborPosition);
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            // Ignore out of bounds
        }

        return neighborsInfo;
    }

    private void PlaceRoom(int[,] layout, Vector2 position)
    {
        layout[(int)position.Y, (int)position.X] = 1;
    }

    private bool IsPositionFree(int[,] layout, Vector2 position)
    {
        return layout[(int)position.Y, (int)position.X] == 0;
    }

    private void PrintLayout(int[,] layout)
    {
        for (int y = 0; y < layout.GetLength(0); y++)
        {
            var str = "";
            for (int x = 0; x < layout.GetLength(1); x++)
            {
                str += layout[y, x] + " ";
            }

            GD.Print(str);
        }
    }
}
