using Godot;

/// <summary>
/// Player character class that extends Entity.
/// Handles player-specific logic and uses MovementComponent for movement.
/// </summary>
public partial class Player : Entity
{
    public override void _Ready()
    {
        // Call base Ready to initialize components
        base._Ready();
        
        // Player-specific initialization can go here
        GD.Print("Player initialized with MovementComponent");
    }
    
    public override void _Process(double delta)
    {
        // Player-specific processing can go here
        // Movement is handled automatically by MovementComponent
    }
}