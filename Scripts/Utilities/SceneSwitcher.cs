namespace EESaga.Scripts.Utilities;

using EESaga.Scripts.Managers;
using Godot;
using System.Collections.Generic;
using UI;

public partial class SceneSwitcher : Node
{
    public Node Main { get; set; }
    public Stack<Node> Scenes { get; } = new();
    public Node? CurrentScene
    {
        get
        {
            return Scenes.Count > 0 ? Scenes.Peek() : null;
        }
    }

    private Timer _timer = new();

    private TitleMenu _titleMenu = TitleMenu.Instance();
    private BattleManager _battleManager = BattleManager.Instance();

    public override void _Ready()
    {
        Main = GetNodeOrNull<Node>("/root/Main");
        AddChild(_timer);
        if (Main != null)
        {
            PushScene(_titleMenu);
        }

    }

    public void PushScene(Node? newScene)
    {
        if (CurrentScene != null)
        {
            Main.RemoveChild(CurrentScene);
        }
        if (newScene != null)
        {
            Main.AddChild(newScene);
        }
        Scenes.Push(newScene);
    }

    public void PopScene()
    {
        if (CurrentScene != null)
        {
            Main.RemoveChild(CurrentScene);
        }
        if (Scenes.Count > 1)
        {
            Main.AddChild(Scenes.Pop());
        }
    }
}
