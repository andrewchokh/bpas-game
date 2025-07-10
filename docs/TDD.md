# 💻 Technical Design Document

## 1. Engine & Tools

- **Engine**: Godot 4.4
- **Language**: C#
- **Version Control**: Git + GitHub
- **Art Tools**: Aseprite
- **Other Tools**: Audacity, jsfxr

## 2. Project Structure

```
project-root/
├── assets/             # Sprites, sounds, fonts, themes, etc.
├── globals/            # Global scripts for Autoload (Singleton)
│   └── GameState.cs    # Game State Manager
├── scenes/             # Godot scenes (.tscn files)
│   ├── components      # Components-related scenes
│   ├── enemies         # Enemies-related scenes
│   ├── players         # Players-related scenes
│   ├── ui              # UI/GUI-related scenes
│   └── game.tdsn       # Main game scene
├── scripts/            # Godot scripts
│   ├── components      # Components-related scripts
│   ├── enemies         # Enemies-related scripts
│   ├── players         # Players-related scripts
│   └── ui              # UI/GUI-related scripts
└── project.godot       # Godot project file
```

## 3. Key Systems

### Game State Manager

- Global script (`GameState.gd`) tracks game state, such as free roam, cutscene, pause, game over, etc.

### Entity System

- Player, Enemy, Weapon, Projectile use different base class
- Each mechanic must be implemented with Component System that can be applied to any entity.
  - Example:
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

- [C# style guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) for code style
  - `snake_case` for directories
- Signals at top of script
- Keep logic modular (one responsibility per script). Use Component System when possible

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

-...

## 7. Build & Export

- Target: Windows 10/11
- Export presets in `export_presets.cfg`
- Builds go into `/builds/` directory

## 8. Known Issues / Tech Debt

- ...
