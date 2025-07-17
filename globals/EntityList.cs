using Godot;
using System;
using System.Collections.Generic;
using System.Net;

public partial class EntityList : Node
{
    public static EntityList Instance { get; private set; }

    public enum PlayerType
    {
        Bob
    }

    public enum EnemyType
    {
        Bob
    }

    public enum WeaponType
    {
        Bob
    }

    public enum AblityType
    {
        Bob
    }

    public override void _Ready()
    {
        Instance = this;
    }

    public static readonly Dictionary<PlayerType, PackedScene> PlayerScenes = new()
    {
        { PlayerType.Bob, GD.Load<PackedScene>("res://scenes/players/impl/Bob.tscn") },
    };
}
