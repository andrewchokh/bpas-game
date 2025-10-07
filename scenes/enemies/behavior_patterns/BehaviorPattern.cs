using Godot;
using System;

public partial class BehaviorPattern : Node2D
{
    public Enemy Enemy;

    public override void _EnterTree()
    {
        // Enemy assignment for BehaviorPattern should be used in _EnterTree. 
        // Otherwise, it may lead to null reference errors.
        Enemy = GetParent<Enemy>();
    }
}
