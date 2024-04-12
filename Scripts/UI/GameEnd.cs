using EESaga.Scripts.UI;
using EESaga.Scripts.Utilities;
using Godot;
using System;
using EESaga.Scripts.Data;

public partial class GameEnd : Control
{
    private Button _gameQuitButton;
    private SceneSwitcher _sceneSwitcher;

    public static GameEnd Instance() => GD.Load<PackedScene>("res://Scenes/UI/game_end.tscn").Instantiate<GameEnd>();

    public override void _Ready()
    {
        _gameQuitButton = GetNode<Button>("GameQuitButton");
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

        _gameQuitButton.Pressed += OnGameQuitButtonPressed;
    }

    private void OnGameQuitButtonPressed()
    {
        _sceneSwitcher.PushScene(SceneSwitcher.TitleMenuScene);
    }
}
