using Godot;
using System;

public partial class LevelTileMapLayers : Node2D
{
    [Export]
    public TileMapLayer FloorTileMapLayer;
    [Export]
    public TileMapLayer WallsTileMapLayer;
}
