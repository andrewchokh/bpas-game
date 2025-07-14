using Godot;

/// <summary>
/// Test script to verify movement component functionality.
/// This can be run to validate that the movement mechanics work correctly.
/// </summary>
public partial class MovementTest : Node
{
    private Player _player;
    private MovementComponent _movementComponent;
    
    public override void _Ready()
    {
        GD.Print("=== Movement Component Test ===");
        
        // Find the player in the scene
        _player = GetNode<Player>("../Player");
        if (_player == null)
        {
            GD.PrintErr("Player not found in scene!");
            return;
        }
        
        _movementComponent = _player.MovementComponent;
        if (_movementComponent == null)
        {
            GD.PrintErr("MovementComponent not found on Player!");
            return;
        }
        
        // Test component initialization
        TestComponentInitialization();
        
        // Print instructions
        PrintTestInstructions();
    }
    
    private void TestComponentInitialization()
    {
        GD.Print("✓ Player found and initialized");
        GD.Print("✓ MovementComponent found and attached");
        GD.Print($"✓ Movement speed set to: {_movementComponent.Speed}");
        
        // Test speed setter/getter
        float originalSpeed = _movementComponent.GetSpeed();
        _movementComponent.SetSpeed(250.0f);
        if (_movementComponent.GetSpeed() == 250.0f)
        {
            GD.Print("✓ Speed setter/getter working correctly");
        }
        else
        {
            GD.PrintErr("✗ Speed setter/getter failed");
        }
        
        // Restore original speed
        _movementComponent.SetSpeed(originalSpeed);
    }
    
    private void PrintTestInstructions()
    {
        GD.Print("\n=== Movement Test Instructions ===");
        GD.Print("Use WASD keys to test 8-directional movement:");
        GD.Print("  W - Move Up");
        GD.Print("  A - Move Left");
        GD.Print("  S - Move Down");
        GD.Print("  D - Move Right");
        GD.Print("  Diagonal movement: W+A, W+D, S+A, S+D");
        GD.Print("Expected behavior:");
        GD.Print("  - Smooth movement in all 8 directions");
        GD.Print("  - Diagonal movement at same speed as cardinal directions");
        GD.Print("  - Immediate stop when no keys are pressed");
        GD.Print("=====================================\n");
    }
    
    public override void _Process(double delta)
    {
        // Optional: Monitor player velocity for debugging
        if (_player != null && Input.IsActionJustPressed("ui_accept"))
        {
            GD.Print($"Player velocity: {_player.Velocity}");
            GD.Print($"Player global position: {_player.GlobalPosition}");
        }
    }
}