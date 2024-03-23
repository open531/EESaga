using EESaga.Scripts.UI;
using EESaga.Scripts.Utilities;
using Godot;
using System;

public partial class GameWin : Control
{
    private Button _gameNextButton;
    private Button _gameQuitButton;
    private SceneSwitcher _sceneSwitcher;

    public static GameWin Instance() => GD.Load<PackedScene>("res://Scenes/UI/game_win.tscn").Instantiate<GameWin>();

    public override void _Ready()
    {
        _gameNextButton = GetNode<Button>("GameNextButton");
        _gameQuitButton = GetNode<Button>("GameQuitButton");
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

        _gameNextButton.Pressed += OnGameNextButtonPressed;
        _gameQuitButton.Pressed += OnGameQuitButtonPressed;
    }

    private void OnGameNextButtonPressed()
    {
        _sceneSwitcher.PushScene(SceneSwitcher.BattleManager);
    }
    private void OnGameQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
