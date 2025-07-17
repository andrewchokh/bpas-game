using Godot;
using System;

public partial class GameStateMachine : Node
{
    public static GameStateMachine Instance { get; private set; }

    public enum GameState : int
    {
        FREE_ROAM = 1,
        BATTLE = 2,
        CUTSCENE = 3,
        PAUSE = 4,
        GAME_OVER = 5
    }

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
    public delegate void StateChangedEventHandler(GameState OldState, GameState NewState);

    public override void _Ready()
    {
        Instance = this;
    }
}