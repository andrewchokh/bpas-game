using Godot;
using System;

[GlobalClass]
public abstract partial class PlayerState : State
{
    public abstract Player Player { get; set; }

    public override void Enter(Node node) => Player = node as Player;
    public override void Exit(Node node) { }
    public override void Update(Node node, double delta) { }
}