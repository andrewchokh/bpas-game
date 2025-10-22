using Godot;
using System;

[GlobalClass]
public abstract partial class State : Resource
{
    public abstract void Enter(Node node);
    public abstract void Exit(Node node);
    public abstract void Update(Node node, double delta);
}