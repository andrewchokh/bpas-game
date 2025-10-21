using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager Instance { get; private set; }

    public Node2D Current2DScene { get; private set; }
    public Control CurrentControlScene { get; private set; }

    public enum SceneSwap
    {
        DeleteScene,
        HideScene,
        RemoveScene
    }

    public override void _Ready()
    {
        Instance = this;
    }

    public void Change2DScene(PackedScene packedScene, SceneSwap swapType) =>
        Current2DScene = ChangeScene(packedScene, Current2DScene, swapType);

    public void ChangeControlScene(PackedScene packedScene, SceneSwap swapType) =>
        CurrentControlScene = ChangeScene(packedScene, CurrentControlScene, swapType);

    private T ChangeScene<T>(PackedScene packedScene, T currentScene, SceneSwap swapType)
        where T : CanvasItem
    {
        if (currentScene != null)
        {
            switch (swapType)
            {
                case SceneSwap.DeleteScene:
                    currentScene.QueueFree();
                    break;
                case SceneSwap.HideScene:
                    currentScene.Hide();
                    break;
                case SceneSwap.RemoveScene:
                    Globals.Instance.Root.GetRoot().RemoveChild(currentScene);
                    break;
            }
        }

        if (packedScene == null)
            return null;

        T newScene = packedScene.Instantiate<T>();
        Globals.Instance.Root.GetRoot().AddChild(newScene);
        return newScene;
    }
}