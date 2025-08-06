using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Singleton that stores and manages references to entity scenes used in the game.
/// Provides mappings from enum types to PackedScenes for easy instantiation.
/// </summary>
public partial class EntityList : Node
{
    /// <summary>
    /// Singleton instance of the EntityList.
    /// </summary>
    public static EntityList Instance { get; private set; }

    /// <summary>
    /// Enum representing the different player types available.
    /// </summary>
    public enum PlayerType
    {
        Bob
    }

    /// <summary>
    /// Enum representing the different enemy types available.
    /// </summary>
    public enum EnemyType
    {
        Bob
    }

    /// <summary>
    /// Enum representing the different weapon types available.
    /// </summary>
    public enum WeaponType
    {
        ArcaneStaff
    }

    /// <summary>
    /// Enum representing the different ability types available.
    /// </summary>
    public enum AbilityType
    {
        AbilityAcceleration
    }

    public enum PickUpType
    {
        ArcaneStaff,
        Dagger,
        Spear 
    }

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Initializes the singleton instance.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }

    /// <summary>
    /// Dictionary mapping player types to their respective PackedScene resources.
    /// </summary>
    public static readonly Dictionary<PlayerType, PackedScene> PlayerScenes = new()
    {
        { PlayerType.Bob, GD.Load<PackedScene>("res://scenes/players/impl/Bob.tscn") },
    };
}
