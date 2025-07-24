using Godot;
using System;

/// <summary>
/// Camera controller that changes behavior based on the game state.
/// Tracks the player during free roam and adapts for other states like battle and cutscene.
/// </summary>
public partial class Camera2d : Camera2D
{
    /// <summary>
    /// The player the camera follows during free roam.
    /// </summary>
    private Player TargetPlayer;

    /// <summary>
    /// Delegate holding the current behavior method to apply each frame.
    /// </summary>
    private Func<object> ApplyBehaviorMethod;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Subscribes to game state changes and initializes the target player.
    /// </summary>
    public override void _Ready()
    {
        base._Ready();

        GameStateMachine.Instance.StateChanged += ChangeBehavior;

        TargetPlayer = Utils.GetFirstPlayer();

        GD.Print($"Camera2D ready. TargetPlayer: {TargetPlayer}");
    }

    /// <summary>
    /// Called every frame.
    /// Executes the current camera behavior method if set.
    /// </summary>
    /// <param name="delta">Time elapsed since last frame.</param>
    public override void _Process(double delta)
    {
        if (ApplyBehaviorMethod != null)
            ApplyBehaviorMethod();
    }

    /// <summary>
    /// Updates the camera behavior based on game state changes.
    /// </summary>
    /// <param name="OldState">The previous game state.</param>
    /// <param name="NewState">The new game state.</param>
    public void ChangeBehavior(GameStateMachine.GameState OldState, GameStateMachine.GameState NewState)
    {
        switch (NewState)
        {
            case GameStateMachine.GameState.FREE_ROAM:
                ApplyBehaviorMethod = Utils.BuildMethod(FreeRoamBehavior);
                break;
            case GameStateMachine.GameState.BATTLE:
                ApplyBehaviorMethod = Utils.BuildMethod(BattleBehavior);
                break;
            case GameStateMachine.GameState.CUTSCENE:
                ApplyBehaviorMethod = Utils.BuildMethod(CutsceneBehavior);
                break;
            case GameStateMachine.GameState.PAUSE:
                ApplyBehaviorMethod = Utils.BuildMethod(PauseBehavior);
                break;
            case GameStateMachine.GameState.GAME_OVER:
                ApplyBehaviorMethod = Utils.BuildMethod(GameOverBehavior);
                break;
        }
    }

    /// <summary>
    /// Camera behavior during free roam.
    /// Follows the player’s position.
    /// </summary>
    /// <returns>Current game state as integer.</returns>
    private int FreeRoamBehavior()
    {
        if (TargetPlayer != null)
            Position = TargetPlayer.GlobalPosition;

        return (int)GameStateMachine.GameState.FREE_ROAM;    
    }

    /// <summary>
    /// Camera behavior during battle.
    /// Placeholder for battle-specific logic.
    /// </summary>
    /// <returns>Current game state as integer.</returns>
    private int BattleBehavior()
    {
        // Implement battle behavior here
        return (int)GameStateMachine.GameState.BATTLE;  
    }

    /// <summary>
    /// Camera behavior during cutscenes.
    /// Placeholder for cutscene-specific logic.
    /// </summary>
    /// <returns>Current game state as integer.</returns>
    private int CutsceneBehavior()
    {
        // Implement cutscene behavior here
        return (int)GameStateMachine.GameState.CUTSCENE;  
    }

    /// <summary>
    /// Camera behavior when the game is paused.
    /// Placeholder for pause-specific logic.
    /// </summary>
    /// <returns>Current game state as integer.</returns>
    private int PauseBehavior()
    {
        // Implement pause behavior here
        return (int)GameStateMachine.GameState.PAUSE;  
    }

    /// <summary>
    /// Camera behavior on game over.
    /// Placeholder for game over-specific logic.
    /// </summary>
    /// <returns>Current game state as integer.</returns>
    private int GameOverBehavior()
    {
        // Implement game over behavior here
        return (int)GameStateMachine.GameState.GAME_OVER;  
    }
}
