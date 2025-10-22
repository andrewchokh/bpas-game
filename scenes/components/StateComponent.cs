using Godot;
using System;

public partial class StateComponent : Node2D
{
    [Export]
    public Node Node;
    [Export]
    public State State { get; private set; }

    public override void _Ready() => State?.Enter(Node);

    public override void _Process(double delta) => State?.Update(Node, delta);

    public void ChangeState(State newState)
    {
        State.Exit(Node);
        State = newState;
        State.Enter(Node);
    }
}
