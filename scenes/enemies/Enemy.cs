using System;
using Godot;

using static Ids;

/// <summary>
/// Represents an enemy character in the game.
/// </summary>
public partial class Enemy : CharacterBody2D
{
    [Export]
    public EnemyCharacterId EnemyId;

    [ExportGroup("Behavior")]
    [Export]
    public EnemyBehavior[] BehaviorList;
    [ExportSubgroup("Duration")]
    [Export]
    public float MinBehaviorDuration;
    [Export]
    public float MaxBehaviorDuration;

    [ExportCategory("Components")]
    [Export]
    public MovementComponent MovementComponent;
    [Export]
    public HealthComponent HealthComponent;
    [Export]
    public HitboxComponent HitboxComponent;

    private Timer _behaviorTimer;
    private EnemyBehavior _currentBehavior;

    public override void _Ready()
    {
        _behaviorTimer = new Timer();
        AddChild(_behaviorTimer);
        _behaviorTimer.OneShot = true;
        _behaviorTimer.Timeout += OnBehaviorTimerTimeout;

        SetRandomBehavior();
    }

    public override void _Process(double delta)
    {
        if (!_currentBehavior.IsProcessEnabled)
            return;

        _currentBehavior.ExecuteProcess(this, delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_currentBehavior.IsPhysicsProcessEnabled)
            return;

        _currentBehavior.ExecutePhysicsProcess(this, delta);
    }

    public void SetRandomBehavior()
    {
        _currentBehavior = BehaviorList[GD.Randi() % BehaviorList.Length];

        SetDurationTimer();
    }

    public void SetDurationTimer()
    {
        if (MinBehaviorDuration <= 0 || MaxBehaviorDuration <= 0
        || MinBehaviorDuration > MaxBehaviorDuration)
        {
            GD.PushWarning("Invalid behavior duration settings for enemy: " + Name);
            return;
        }

        var duration = (float)GD.RandRange(MinBehaviorDuration, MaxBehaviorDuration);
        _behaviorTimer.WaitTime = duration;
        _behaviorTimer.Start();
    }

    private void OnBehaviorTimerTimeout()
    {
        SetRandomBehavior();
    }
}