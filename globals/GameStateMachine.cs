using Godot;
using System;

/// <summary>
/// Singleton that manages the current game state and notifies listeners on changes.
/// </summary>
public partial class GameStateMachine : Node
{
    /// <summary>
    /// Singleton instance of the GameStateMachine.
    /// </summary>
    public static GameStateMachine Instance { get; private set; }

    /// <summary>
    /// Enum representing the possible game states.
    /// </summary>
    public enum GameState : int
    {
        FREE_ROAM = 1,
        BATTLE = 2,
        CUTSCENE = 3,
        PAUSE = 4,
        GAME_OVER = 5
    }

    private GameState _state;

    /// <summary>
    /// Current game state.
    /// Setting this will emit a signal if the state changes.
    /// </summary>
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

    /// <summary>
    /// Signal emitted when the game state changes.
    /// Provides the old and new game states.
    /// </summary>
    [Signal]
    public delegate void StateChangedEventHandler(GameState OldState, GameState NewState);

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Initializes the singleton instance.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }
}
