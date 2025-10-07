using System;
using Godot;
using Godot.Collections;

using static EntityList;

public partial class Enemy : CharacterBody2D
{
    [Export]
    public EnemyCharacterId EnemySceneId;

    [ExportGroup("Behavior Patterns")]
    [Export]
    public PackedScene[] BehaviorPatternScenes;
    [Export]
    public Timer BehaviorPatternTimer;
    [ExportSubgroup("Patterns Duration")]
    [Export]
    public float MinPatternDuration;
    [Export]
    public float MaxPatternDuration;

    [ExportCategory("Components")]
    [Export]
    public MovementComponent MovementComponent;
    [Export]
    public HealthComponent HealthComponent;
    [Export]
    public HitboxComponent HitboxComponent;

    private Array<BehaviorPattern> _behaviorPatterns = [];
    private BehaviorPattern _currentBehaviorPattern;

    public override void _Ready()
    {
        BehaviorPatternTimer.Timeout += SelectRandomPattern;

        SetupBehavorPatterns();

        SelectRandomPattern();
    }

    private void SetupBehavorPatterns()
    {
        if (BehaviorPatternScenes.Length == 0)
        {
            GD.PushWarning("BehaviorPatterns is not set for Enemy.");
            return;
        }

        foreach (var patternScene in BehaviorPatternScenes)
        {
            if (patternScene == null)
            {
                GD.PushWarning("One of the BehaviorPatterns is null.");
                continue;
            }

            var behaviorPattern = patternScene.Instantiate<BehaviorPattern>();

            _behaviorPatterns.Add(behaviorPattern);
            AddChild(behaviorPattern);
        }
    }

    private void SetupBahaviorDuration()
    {
        if (MinPatternDuration <= 0 || MaxPatternDuration <= 0)
        {
            GD.PushWarning("No pattern duration will be applied.");
            return;
        }

        if (MaxPatternDuration <= MinPatternDuration)
        {
            GD.PushError("MaxPatternDuration must be greater than MinPatternDuration.");
            return;
        }

        var duration = GD.RandRange(MinPatternDuration, MaxPatternDuration);

        BehaviorPatternTimer.WaitTime = duration;
        BehaviorPatternTimer.Start();
    }

    private void SelectRandomPattern()
    {
        if (_behaviorPatterns.Count == 0)
        {
            GD.PushWarning("No behavior patterns available.");
            return;
        }

        var index = GD.RandRange(0, _behaviorPatterns.Count - 1);
        _currentBehaviorPattern = _behaviorPatterns[index];

        GD.Print($"Selected behavior pattern: {_currentBehaviorPattern.Name}");

        ApplyPattern(index);

        SetupBahaviorDuration();

        GD.Print($"Behavior pattern duration set to: {BehaviorPatternTimer.WaitTime} seconds");
    }

    private void ApplyPattern(int index)
    {
        if (index < 0 || index >= _behaviorPatterns.Count)
        {
            GD.PushError($"Invalid behavior pattern index: {index}");
            return;
        }

        _currentBehaviorPattern.ProcessMode = ProcessModeEnum.Inherit;

        foreach (var pattern in _behaviorPatterns)
        {
            if (pattern != _currentBehaviorPattern)
                pattern.ProcessMode = ProcessModeEnum.Disabled;
        }
    }
}