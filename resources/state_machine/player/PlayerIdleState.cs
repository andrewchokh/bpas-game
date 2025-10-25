using Godot;
using System;

[GlobalClass]
public partial class PlayerIdleState : PlayerState
{
    public override Player Player { get; set; }

    public override void Enter(Node node) => base.Enter(node);
    public override void Exit(Node node) { }
    public override void Update(Node node, double delta) { }
}
