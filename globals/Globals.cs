using Godot;
using System;

public partial class Globals : Node
{
    public static Globals Instance { get; private set; }
    public SceneTree Root { get; private set; }

    public override void _EnterTree()
    {
        Root = GetTree();
    }
    public override void _Ready()
    {
        Instance = this;
    }
}