using Godot;

/// <summary>
/// Component that handles 8-directional movement for any entity.
/// Can be attached to any CharacterBody2D to provide movement functionality.
/// </summary>
public partial class MovementComponent : Node
{
    [Export] public float Speed = 300.0f;
    
    // Reference to the entity this component is attached to
    private CharacterBody2D _entity;
    
    public override void _Ready()
    {
        // Get the parent entity (should be a CharacterBody2D)
        _entity = GetParent<CharacterBody2D>();
        
        if (_entity == null)
        {
            GD.PrintErr("MovementComponent must be a child of CharacterBody2D");
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (_entity == null) return;
        
        HandleMovement();
    }
    
    /// <summary>
    /// Handles 8-directional movement input and applies it to the entity
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = GetInputVector();
        
        if (inputVector != Vector2.Zero)
        {
            // Normalize diagonal movement to prevent faster movement on diagonals
            inputVector = inputVector.Normalized();
            _entity.Velocity = inputVector * Speed;
        }
        else
        {
            // No input, stop the entity
            _entity.Velocity = Vector2.Zero;
        }
        
        // Apply the movement
        _entity.MoveAndSlide();
    }
    
    /// <summary>
    /// Gets the input vector for movement based on WASD keys
    /// Returns a normalized vector representing the movement direction
    /// </summary>
    private Vector2 GetInputVector()
    {
        Vector2 inputVector = Vector2.Zero;
        
        // Check for movement input (WASD)
        if (Input.IsActionPressed("move_left"))
            inputVector.X -= 1;
        if (Input.IsActionPressed("move_right"))
            inputVector.X += 1;
        if (Input.IsActionPressed("move_up"))
            inputVector.Y -= 1;
        if (Input.IsActionPressed("move_down"))
            inputVector.Y += 1;
            
        return inputVector;
    }
    
    /// <summary>
    /// Public method to set movement speed from external scripts
    /// </summary>
    /// <param name="newSpeed">New movement speed</param>
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
    
    /// <summary>
    /// Public method to get current movement speed
    /// </summary>
    /// <returns>Current movement speed</returns>
    public float GetSpeed()
    {
        return Speed;
    }
}