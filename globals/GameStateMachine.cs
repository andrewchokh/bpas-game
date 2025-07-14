using Godot;
using System;

public partial class GameStateMachine : Node
{
    public static GameStateMachine Instance { get; private set; }

    public enum GameState : int
    {
        FREE_ROAM = 0,
        BATTLE = 1,
        CUTSCENE = 2,
        PAUSE = 3,
        GAME_OVER = 4
    }

    public GameState State
    {
        get => State;
        set
        {
            if (State != value && Enum.IsDefined(typeof(GameState), value))
            {
                EmitSignal(SignalName.StateChanged, (int)State, (int)value);
                State = value;
            }
        }
    }

    [Signal]
    public delegate void StateChangedEventHandler(GameStateMachine OldState, GameStateMachine NewState);

    public override void _Ready()
    {
        Instance = this;
    }
}