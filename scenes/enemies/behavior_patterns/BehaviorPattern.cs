using Godot;
using System;

public partial class BehaviorPattern : Node
{
    public Enemy Enemy;

    public override void _Ready()
    {
        Enemy = GetParent<Enemy>();
    }
}
