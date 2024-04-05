namespace EESaga.Scripts.Utilities;

using Godot;
using Managers;
using System.Collections.Generic;
using UI;

public partial class SceneSwitcher : Node
{
    public Main MainScene { get; set; }
    public Stack<Node> Scenes { get; } = new();
    public Node? CurrentScene;
    public bool AllowPause => CurrentScene is not TitleMenu;

    private Timer _timer = new();

    public static TitleMenu TitleMenuScene => TitleMenu.Instance();
    public static BattleManager BattleManagerScene => BattleManager.Instance();
    public static GameOver GameOverScene => GameOver.Instance();
    public static GameWin GameWinScene => GameWin.Instance();
    public static NameInputPanel NameInputPanelScene => NameInputPanel.Instance();

    public override void _Ready()
    {
        MainScene = GetNodeOrNull<Main>("/root/Main");
        AddChild(_timer);
        if (MainScene != null)
        {
            MainScene.GamePause += OnGamePause;
            MainScene.GameResume += OnGameResume;
            PushScene(TitleMenuScene);
        }
    }

    public void PushScene(Node? newScene, bool deleteCurrent = false)
    {
        GD.Print($"CurrentScene: {CurrentScene?.Name}, newScene: {newScene?.Name}");
        if (CurrentScene != null)
        {
            GD.Print($"Removing {CurrentScene.Name}");
            MainScene.RemoveChild(CurrentScene);
            GD.Print($"Removed {CurrentScene.Name}");

        }
        if (newScene != null)
        {
            GD.Print($"Adding {newScene.Name}");
            MainScene.AddChild(newScene);
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
            MainScene.RemoveChild(scene);
            scene.Free();
        }
        if (Scenes.Count > 0)
        {
            MainScene.AddChild(Scenes.Peek());
            CurrentScene = Scenes.Peek();
        }
        else
        {
            CurrentScene = null;
        }
    }

    private void OnGamePause()
    {
        if (CurrentScene is BattleManager battleManager)
        {
            battleManager.PieceBattle._pieceDetail.Hide();
            battleManager.CardBattle.Hide();
        }
    }

    private void OnGameResume()
    {
        if (CurrentScene is BattleManager battleManager)
        {
            battleManager.PieceBattle._pieceDetail.Show();
            battleManager.CardBattle.Show();
        }
    }
}
