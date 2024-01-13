namespace EESaga.Scripts.UI;

using Godot;
using System;

public partial class TitleMenu : Control
{
    [Export] public PackedScene PlayScene { get; set; }

    private MarginContainer _marginContainer;
    private Button _playButton;
    private Button _loadButton;
    private Button _optionButton;
    private Button _quitButton;

    private PopupPanel _optionMenu;

    public override void _Ready()
    {
        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _playButton = GetNode<Button>("%PlayButton");
        _loadButton = GetNode<Button>("%LoadButton");
        _optionButton = GetNode<Button>("%OptionButton");
        _quitButton = GetNode<Button>("%QuitButton");

        _optionMenu = GetNode<PopupPanel>("OptionMenu");

        _playButton.Pressed += OnPlayButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _optionMenu.Hide();

        _marginContainer.Position = new Vector2(0, 360 - _marginContainer.Size.Y);
    }

    private void OnPlayButtonPressed()
    {
        if (PlayScene != null)
        {
            GetTree().ChangeSceneToPacked(PlayScene);
        }
    }

    private void OnOptionButtonPressed()
    {
        _optionMenu.PopupCentered();
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
