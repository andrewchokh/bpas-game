using Godot;
using System;

[GlobalClass]
public abstract partial class EnemyBehavior : Resource
{
    public abstract bool IsProcessEnabled { get; }
    public abstract bool IsPhysicsProcessEnabled { get; }

    public abstract void ExecuteProcess(Enemy enemy, double delta);
    public abstract void ExecutePhysicsProcess(Enemy enemy, double delta);
}
