using Godot;
using System;

public partial class MainCamera : Camera2D
{
    private Player TargetPlayer;

    private Func<object> ApplyBehaviorMethod;

    public override void _Ready()
    {
        base._Ready();

        GameStateMachine.Instance.StateChanged += ChangeBehavior;

        TargetPlayer = Utils.Instance.GetFirstPlayer();

        GD.Print($"Camera2D ready. TargetPlayer: {TargetPlayer}");
    }

    public override void _Process(double delta)
    {
        if (ApplyBehaviorMethod != null)
            ApplyBehaviorMethod();
    }

    public void ChangeBehavior(GameStateMachine.GameState oldState, GameStateMachine.GameState newState)
    {
        switch (newState)
        {
            case GameStateMachine.GameState.FREE_ROAM:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(FreeRoamBehavior);
                break;
            case GameStateMachine.GameState.BATTLE:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(BattleBehavior);
                break;
            case GameStateMachine.GameState.CUTSCENE:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(CutsceneBehavior);
                break;
            case GameStateMachine.GameState.PAUSE:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(PauseBehavior);
                break;
            case GameStateMachine.GameState.GAME_OVER:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(GameOverBehavior);
                break;
        }
    }

    private int FreeRoamBehavior()
    {
        if (TargetPlayer != null)
            Position = TargetPlayer.GlobalPosition;

        return (int)GameStateMachine.GameState.FREE_ROAM;    
    }

    private int BattleBehavior()
    {
        // Implement battle behavior here
        return (int)GameStateMachine.GameState.BATTLE;  
    }

    private int CutsceneBehavior()
    {
        // Implement cutscene behavior here
        return (int)GameStateMachine.GameState.CUTSCENE;  
    }

    private int PauseBehavior()
    {
        // Implement pause behavior here
        return (int)GameStateMachine.GameState.PAUSE;  
    }

    private int GameOverBehavior()
    {
        // Implement game over behavior here
        return (int)GameStateMachine.GameState.GAME_OVER;  
    }
}