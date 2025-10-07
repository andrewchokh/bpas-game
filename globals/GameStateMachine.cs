using Godot;
using System;

public enum GameState : int
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
public partial class GameStateMachine : Node
{
    public static GameStateMachine Instance { get; private set; }

    private GameState _state;
    public GameState State
    {
        get => _state;
        set
        {
            if (_state != value && Enum.IsDefined(typeof(GameState), value))
            {
                EmitSignal(SignalName.StateChanged, (int)_state, (int)value);
                _state = value;
            }
        }
    }

    [Signal]
    public delegate void StateChangedEventHandler(GameState oldState, GameState newState);

    public override void _Ready()
    {
        Instance = this;
    }
}
