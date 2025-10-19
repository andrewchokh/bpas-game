using Godot;
using Godot.Collections;
using System;

public partial class SpriteComponent : Node2D
{
    [Export]
    public Node2D Entity;

    [ExportGroup("Body Parts")]
    [Export]
    public Sprite2D HeadSprite;
    [Export]
    public Sprite2D BodySprite;
    [Export]
    public Dictionary<string, Sprite2D> ArmSprites;
    [Export]
    public Dictionary<string, Sprite2D> LegSprites;
}
