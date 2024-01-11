using Godot;
using System;

public partial class PauseMenu : Control
{
    private MarginContainer _marginContainer;
    private Button _resumeButton;
    private Button _loadButton;
    private Button _saveButton;
    private Button _optionButton;
    private Button _quitButton;

    private PopupPanel _optionMenu;

    public override void _Ready()
    {
        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _resumeButton = GetNode<Button>("MarginContainer/GridContainer/ResumeButton");
        _loadButton = GetNode<Button>("MarginContainer/GridContainer/LoadButton");
        _saveButton = GetNode<Button>("MarginContainer/GridContainer/SaveButton");
        _optionButton = GetNode<Button>("MarginContainer/GridContainer/OptionButton");
        _quitButton = GetNode<Button>("MarginContainer/GridContainer/QuitButton");

        _optionMenu = GetNode<PopupPanel>("OptionMenu");

        _resumeButton.Pressed += OnResumeButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _optionMenu.Hide();

        _marginContainer.Position = new Vector2(0, 360 - _marginContainer.Size.Y);
    }

    private void OnResumeButtonPressed()
    {
        GetTree().Paused = false;
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
