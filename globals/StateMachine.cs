using Godot;
using System;

public enum State : int
{
    FreeRoam = 1,
    Battle = 2,
    Cutscene = 3,
    Pause = 4,
    GameOver = 5
}

/// <summary>
/// Manages game states for the entire game.
/// </summary>
public partial class StateMachine : Node
{
    public static StateMachine Instance { get; private set; }

    private State _state;
    public State State
    {
        get => _state;
        set
        {
            if (_state != value && Enum.IsDefined(typeof(State), value))
            {
                EmitSignal(SignalName.StateChanged, (int)_state, (int)value);
                _state = value;
            }
        }
    }

    [Signal]
    public delegate void StateChangedEventHandler(State oldState, State newState);

    public override void _Ready()
    {
        Instance = this;
    }
}
