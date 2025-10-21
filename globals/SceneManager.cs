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

    public void Change2DScene(Node2D newScene, SceneSwap swapType)
    {
        Node2D oldScene = current2DScene;

        if (current2DScene != null)
        {
            switch (swapType)
            {
                case SceneSwap.DeleteScene:
                    current2DScene.QueueFree();
                    current2DScene = null;
                    break;
                case SceneSwap.HideScene:
                    current2DScene.Hide();
                    break;
                case SceneSwap.RemoveScene:
                    current2DScene.Visible = false;
                    current2DScene.SetProcess(false);
                    current2DScene.GetParent()?.RemoveChild(current2DScene);
                    break;
            }
        }
        if (newScene != null)
        {
            Globals.Instance.Root.GetRoot().AddChild(newScene);
            Globals.Instance.Root.CurrentScene = newScene;
            current2DScene = newScene;
        }
        else
            current2DScene = null;

        EmitSignal(SignalName.Scene2DChanged, oldScene, current2DScene);
    }

    public void ChangeControlScene(Control newScene, SceneSwap swapType)
    {
        Control oldScene = currentControlScene;

        if (currentControlScene != null)
        {
            switch (swapType)
            {
                case SceneSwap.DeleteScene:
                    currentControlScene.QueueFree();
                    currentControlScene = null;
                    break;
                case SceneSwap.HideScene:
                    currentControlScene.Hide();
                    break;
                case SceneSwap.RemoveScene:
                    currentControlScene.Visible = false;
                    currentControlScene.SetProcess(false);
                    currentControlScene.GetParent()?.RemoveChild(currentControlScene);
                    break;
            }
        }

        if (newScene != null)
        {
            Globals.Instance.Root.GetRoot().AddChild(newScene);
            currentControlScene = newScene;
        }
        else
            currentControlScene = null;

        EmitSignal(SignalName.ControlSceneChanged, oldScene, currentControlScene);
    }
}