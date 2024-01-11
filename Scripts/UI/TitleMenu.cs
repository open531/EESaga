namespace EESaga.Scripts.UI;

using Godot;

public partial class TitleMenu : Control
{
    private MarginContainer _marginContainer;
    private Button _playButton;
    private Button _loadButton;
    private Button _optionButton;
    private Button _quitButton;

    private PopupPanel _optionMenu;

    public override void _Ready()
    {
        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _playButton = GetNode<Button>("MarginContainer/GridContainer/PlayButton");
        _loadButton = GetNode<Button>("MarginContainer/GridContainer/LoadButton");
        _optionButton = GetNode<Button>("MarginContainer/GridContainer/OptionButton");
        _quitButton = GetNode<Button>("MarginContainer/GridContainer/QuitButton");

        _optionMenu = GetNode<PopupPanel>("OptionMenu");

        _playButton.Pressed += OnPlayButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _optionMenu.Hide();

        _marginContainer.Position = new Vector2(0, 360 - _marginContainer.Size.Y);
    }

    private void OnPlayButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/open_world.tscn");
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
