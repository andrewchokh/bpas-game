# Movement Component

This document describes the movement system implemented for the "By Prophesy and Sword" game.

## Overview

The movement system provides 8-directional character movement using WASD controls. It follows a component-based architecture for reusability across different entities.

## Components

### MovementComponent.cs

Located in `scripts/components/MovementComponent.cs`

**Features:**
- 8-directional movement (cardinal and diagonal directions)
- Normalized diagonal movement (prevents faster diagonal movement)
- Configurable movement speed
- WASD key bindings (with arrow key support)
- Automatic velocity management

**Usage:**
1. Add MovementComponent as a child node to any CharacterBody2D
2. Set the desired speed in the inspector or via code
3. The component automatically handles input and movement

**Public Methods:**
- `SetSpeed(float newSpeed)` - Changes the movement speed
- `GetSpeed()` - Returns the current movement speed

### Entity.cs

Located in `scripts/Entity.cs`

Base class for all game entities (Player, Enemy, etc.). Provides:
- Component management
- Common entity functionality
- Reference to MovementComponent (if present)

### Player.cs

Located in `scripts/players/Player.cs`

Player character implementation that extends Entity and uses MovementComponent.

## Controls

| Key | Action |
|-----|---------|
| W / ↑ | Move Up |
| A / ← | Move Left |
| S / ↓ | Move Down |
| D / → | Move Right |
| W+A | Move Up-Left |
| W+D | Move Up-Right |
| S+A | Move Down-Left |
| S+D | Move Down-Right |

## Scene Structure

```
Player.tscn
├── Sprite2D (visual representation)
├── CollisionShape2D (collision detection)
└── MovementComponent (movement logic)
```

## Testing

The MovementTest.cs script provides validation and testing for the movement component:
- Verifies component initialization
- Tests speed setter/getter functionality
- Provides runtime debugging information

To test:
1. Run the Main scene
2. Use WASD keys to test movement
3. Press Enter to display velocity and position debug info

## Implementation Details

### 8-Directional Movement

The movement system supports smooth 8-directional movement by:
1. Reading WASD input to create a Vector2 input direction
2. Normalizing the vector to ensure consistent speed in all directions
3. Applying the normalized vector multiplied by speed to the entity's velocity
4. Using Godot's MoveAndSlide() for physics-based movement

### Performance Considerations

- Input is processed in _PhysicsProcess() for consistent physics timing
- Velocity is set to zero when no input is detected (immediate stopping)
- Component uses GetParent() only once during initialization for efficiency

## Integration with Other Systems

The MovementComponent can be easily extended or combined with other components:
- HealthComponent for entity health management
- InventoryComponent for item management
- AI components for enemy movement
- Animation controllers for movement animations

## Future Enhancements

Potential improvements for the movement system:
- Acceleration/deceleration for smoother movement
- Sprint functionality
- Movement animation integration
- Collision-based movement restrictions
- Pathfinding for AI entities