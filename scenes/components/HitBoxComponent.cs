using Godot;
using System;

/// <summary>
/// Component that represents the hitbox area of an entity.
/// Used for detecting collisions and triggering damage logic.
/// </summary>
public partial class HitBoxComponent : Area2D
{
    /// <summary>
    /// The entity this hitbox component belongs to.
    /// Usually the parent or owner node.
    /// </summary>
    [Export]
    public Node2D Entity;

    /// <summary>
    /// The health component that will receive damage when this hitbox is hit.
    /// </summary>
    [Export]
    public HealthComponent HealthComponent;

    /// <summary>
    /// Emitted when the hitbox is triggered.
    /// Extend or connect with custom behavior as needed.
    /// </summary>
    [Signal]
    public delegate void AreaEventHandler();
}
