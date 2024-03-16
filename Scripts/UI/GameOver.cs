using EESaga.Scripts.UI;
using EESaga.Scripts.Utilities;
using Godot;
using System;

public partial class GameOver : Control
{
	private Button _gameRestartButton;
	private Button _gameQuitButton;
	private SceneSwitcher _sceneSwitcher;

    public static GameOver Instance() => GD.Load<PackedScene>("res://Scenes/UI/game_over.tscn").Instantiate<GameOver>();

    public override void _Ready()
	{
        _gameRestartButton = GetNode<Button>("GameRestartButton");
		_gameQuitButton = GetNode<Button>("GameQuitButton");
		_sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");	

		_gameRestartButton.Pressed += OnGameRestartButtonPressed;
		_gameQuitButton.Pressed += OnGameQuitButtonPressed;
    }

	private void OnGameRestartButtonPressed()
	{
        _sceneSwitcher.PushScene(SceneSwitcher.TitleMenu);
    }
	private void OnGameQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
