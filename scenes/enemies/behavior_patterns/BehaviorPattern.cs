using Godot;
using System;

public partial class BehaviorPattern : Node2D
{
    public Enemy Enemy;

    public override void _EnterTree()
    {
        Enemy = GetParent<Enemy>();
    }
}
