# 💻 Technical Design Document

## 1. Engine & Tools

- **Engine**: Godot 4.4
- **Language**: C#
- **Version Control**: Git + GitHub
- **Other Tools**: Aseprite, Audacity, jsfxr

## 2. Project Structure

```
project-root/
├── assets/                     # Sprites, sounds, fonts, themes, etc.
├── globals/                    # Global scripts for Autoload (Singleton)
│   └── GameState.cs            # Game State Manager
├── resources/                  # Godot resources
├── scenes/                     # Godot scenes (.tscn files)
│   ├── components              # Components-related scenes
│   ├── enemies                 # Enemies-related scenes
│   │   └── impl
│   ├── players                 # Players-related scenes
│   │   └── impl
│   ├── ui                      # UI/GUI-related scenes
│   │   └── impl
│   ├── weapons                 # Weapons and projectile-related scenes
│   │   └── impl
│   └── game.tdsn               # Main game scene
└── project.godot               # Godot project file
```

- `impl` stands for "Implementation".

## 3. Key Systems

### Game State Manager

- Global script (`GameState.gd`) tracks game state, such as free roam, cutscene, pause, game over, etc.

### Entity System

- Player, Enemy, Weapon and Projectile use different base class;
  - Implementations of these classes should have their unique class inherited from generic one:
  ```cs
  class Player -> class Bob, class Steve, class Alan...
  ```
- Each mechanic must be implemented with Composition Pattern (aka Component System) that can be applied to any entity:
  ```
  PlayerCharacter.tscn
  ├── MovementComponent
  ├── HealthComponent
  └── InventoryComponent
  ```
- Use signals for triggered effects, such as changed health, death, weapon switch, etc.

### Combat System

- Player can have up to three weapons that he can use and switch between
- Hitbox is a separate component (extends Area2D) that registrates hits. It does NOT have collision with other hitboxes

## 4. Code Style & Conventions

- [C# style guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) for code style:
  - `snake_case` for directories;
  - `PascalCase` for scenes and scripts names.
- Signals at top of script.
- Keep logic modular (one responsibility per script). Use Composition Pattern when possible.

### Patterns

#### Entity Generic Class

```cs
public partial class Entity : Node
{
    // Specify variables that all the entities will share
    // Also, specify variables for all necessary nodes and components that entity has
    // Example:
    // public HealthComponent HealthComponent { get; private set; }

    public override void _Ready()
    {
        // Get values for specified components
        // Example:
        // HealthComponent = GetNode<HealthComponent>("HealthComponent");
    }
}
```

#### Composition (Component)

```cs
public partial class ExampleComponent : Node
{
    // Node of an entity to assign the component
    // Change the type and name depending on context
    // For example, if component is designed for player:
    // public Player Player;
    [Export]
    public Node Entity;

    // Some code...
}
```

#### Singletone

```cs
public partial class Singleton : Node
{
     // One and only instance of the class. Can be accessed across the project
    public static Singleton Instance { get; private set; }

    // Some variables...

    public override void _Ready()
    {
        Instance = this;
    }

    // Some methods...private string _example;
}
```

### Properties

#### Only Getter

```cs
public partial class Example : Node
{
    public String ExampleVar { get; private set; }

    public override void _Ready()
    {
        ExampleVar = ...
    }
}
```

#### Only Setter / Getter and Setter

```cs
public partial class Example : Node
{
    private String _exampleVar;
    public String ExampleVar
    {
        get => _exampleVar;
        set => {
            // Setter logic...
        };
    }

    public override void _Ready()
    {
        ExampleVar = // Assing the value
    }
}
```

## 5. Scene Organization Pattern

- Each major entity = 1 scene + 1 script
- No deeply nested nodes unless necessary
- Example:

```
Player.tscn
├── Sprite2D
├── CollisionShape2D
└── Player.cs
```

## 6. Dependencies / Plugins

- None

## 7. Build & Export

- Target: Windows 10/11
- Export presets in `export_presets.cfg`
- Builds go into `/builds/` directory

## 8. Known Issues / Tech Debt

- None
