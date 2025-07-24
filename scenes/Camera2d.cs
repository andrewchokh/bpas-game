using Godot;
using System;

public partial class Camera2d : Camera2D
{
    private Player TargetPlayer;

    private Func<object> ApplyBehaviorMethod;

    public override void _Ready()
    {
        base._Ready();

        GameStateMachine.Instance.StateChanged += ChangeBehavior;

        TargetPlayer = Utils.GetFirstPlayer();

        GD.Print($"Camera2D ready. TargetPlayer: {TargetPlayer}");
    }

    public override void _Process(double delta)
    {
        if (ApplyBehaviorMethod != null)
            ApplyBehaviorMethod();
    }

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

    private int FreeRoamBehavior()
    {
        if (TargetPlayer != null)
            Position = TargetPlayer.GlobalPosition;

        // Return the current state to maintain the delegate signature
        return (int)GameStateMachine.GameState.FREE_ROAM;    
    }

    private int BattleBehavior()
    {
        // Implement battle behavior here

        // Return the current state to maintain the delegate signature
        return (int)GameStateMachine.GameState.BATTLE;  
    }

    private int CutsceneBehavior()
    {
        // Implement cutscene behavior here

        // Return the current state to maintain the delegate signature
        return (int)GameStateMachine.GameState.CUTSCENE;  
    }

    private int PauseBehavior()
    {
        // Implement pause behavior here

        // Return the current state to maintain the delegate signature
        return (int)GameStateMachine.GameState.PAUSE;  
    }

    private int GameOverBehavior()
    {
        // Implement game over behavior here

        // Return the current state to maintain the delegate signature
        return (int)GameStateMachine.GameState.GAME_OVER;  
    }
}