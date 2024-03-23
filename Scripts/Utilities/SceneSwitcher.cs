namespace EESaga.Scripts.Utilities;

using Godot;
using Managers;
using System.Collections.Generic;
using UI;

public partial class SceneSwitcher : Node
{
    public Node Main { get; set; }
    public Stack<Node> Scenes { get; } = new();
    public Node? CurrentScene;

    private Timer _timer = new();

    public static TitleMenu TitleMenu => TitleMenu.Instance();
    public static BattleManager BattleManager => BattleManager.Instance();
    public static GameOver GameOver => GameOver.Instance();
    public static GameWin GameWin => GameWin.Instance();

    public override void _Ready()
    {
        Main = GetNodeOrNull<Node>("/root/Main");
        AddChild(_timer);
        if (Main != null)
        {
            PushScene(TitleMenu);
        }
    }

    public void PushScene(Node? newScene, bool deleteCurrent = false)
    {
        GD.Print($"CurrentScene: {CurrentScene?.Name}, newScene: {newScene?.Name}");
        if (CurrentScene != null)
        {
            GD.Print($"Removing {CurrentScene.Name}");
            Main.RemoveChild(CurrentScene);
            GD.Print($"Removed {CurrentScene.Name}");

        }
        if (newScene != null)
        {
            GD.Print($"Adding {newScene.Name}");
            Main.AddChild(newScene);
            GD.Print($"Added {newScene.Name}");
        }
        Scenes.Push(newScene);
        if (CurrentScene != null && deleteCurrent)
        {
            GD.Print($"Freeing {CurrentScene.Name}");
            CurrentScene.QueueFree();
            GD.Print($"Freed {CurrentScene.Name}");
        }
        CurrentScene = newScene;
    }

    public void PopScene()
    {
        if (CurrentScene != null)
        {
            var scene = Scenes.Pop();
            GD.Print($"Popping {scene.Name}");
            Main.RemoveChild(scene);
            scene.Free();
        }
        if (Scenes.Count > 0)
        {
            Main.AddChild(Scenes.Peek());
            CurrentScene = Scenes.Peek();
        }
        else
        {
            CurrentScene = null;
        }
    }
}
