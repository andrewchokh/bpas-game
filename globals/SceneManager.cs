using Godot;
using System;

public partial class SceneManager : Node
{
    [Signal]
    public delegate void Scene2DChangedEventHandler(Node2D oldScene, Node2D newScene);
    [Signal]
    public delegate void ControlSceneChangedEventHandler(Control oldScene, Control newScene);
    private Node2D current2DScene;
    private Control currentControlScene;

    public enum SceneSwap
    {
        DeleteScene,
        HideScene,
        RemoveScene
    }

    public static SceneManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void Change2DScene(Node2D newScene, SceneSwap swapType) =>
         ChangeScene(newScene, ref current2DScene, swapType,
              (oldScene, newScene) => EmitSignal(SignalName.Scene2DChanged, oldScene, newScene));

    public void ChangeControlScene(Control newScene, SceneSwap swapType) =>
       ChangeScene(newScene, ref currentControlScene, swapType,
           (oldScene, newScene) => EmitSignal(SignalName.ControlSceneChanged, oldScene, newScene));

    private void ChangeScene<T>(T newScene, ref T currentScene, SceneSwap swapType, Action<T, T> onChanged)
        where T : CanvasItem
    {
        T oldScene = currentScene;

        if (currentScene == null)
            return;

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

        if (newScene == null)
        {
            currentScene = null;
            onChanged?.Invoke(oldScene, currentScene);
            return;
        }

        Globals.Instance.Root.GetRoot().AddChild(newScene);
        currentScene = newScene;

        onChanged?.Invoke(oldScene, currentScene);
    }
}