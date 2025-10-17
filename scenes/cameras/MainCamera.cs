using Godot;
using System;

public partial class MainCamera : Camera2D
{
    private Player TargetPlayer;

    private Func<object> ApplyBehaviorMethod;

    public override void _Ready()
    {
        base._Ready();

        StateMachine.Instance.StateChanged += ChangeBehavior;

        TargetPlayer = Utils.Instance.GetFirstPlayer();

        GD.Print($"Camera2D ready. TargetPlayer: {TargetPlayer}");
    }

    public override void _Process(double delta)
    {
        if (ApplyBehaviorMethod != null)
            ApplyBehaviorMethod();
    }

    public void ChangeBehavior(State oldState, State newState)
    {
        switch (newState)
        {
            case State.FreeRoam:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(FreeRoamBehavior);
                break;
            case State.Battle:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(BattleBehavior);
                break;
            case State.Cutscene:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(CutsceneBehavior);
                break;
            case State.Pause:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(PauseBehavior);
                break;
            case State.GameOver:
                ApplyBehaviorMethod = Utils.Instance.BuildMethod(GameOverBehavior);
                break;
        }
    }


    private int FreeRoamBehavior()
    {
        if (TargetPlayer != null)
            Position = TargetPlayer.GlobalPosition;

        return (int)State.FreeRoam;    
    }

    private int BattleBehavior()
    {
        // Implement battle behavior here
        return (int)State.Battle;  
    }

    private int CutsceneBehavior()
    {
        // Implement cutscene behavior here
        return (int)State.Cutscene;  
    }

    private int PauseBehavior()
    {
        // Implement pause behavior here
        return (int)State.Pause;  
    }

    private int GameOverBehavior()
    {
        // Implement game over behavior here
        return (int)State.GameOver;  
    }
}