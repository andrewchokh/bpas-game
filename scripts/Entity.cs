using Godot;

/// <summary>
/// Base class for all entities in the game.
/// Provides common functionality and component access for Players, Enemies, etc.
/// </summary>
public partial class Entity : CharacterBody2D
{
    // Common components that entities might have
    public MovementComponent MovementComponent { get; private set; }
    
    public override void _Ready()
    {
        // Get components if they exist
        MovementComponent = GetNodeOrNull<MovementComponent>("MovementComponent");
        
        if (MovementComponent != null)
        {
            GD.Print($"Entity {Name} initialized with MovementComponent");
        }
    }
}