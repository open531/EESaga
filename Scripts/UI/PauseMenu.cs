namespace EESaga.Scripts.UI;

using Godot;

public partial class PauseMenu : CanvasLayer
{
    private MarginContainer _marginContainer;
    private Button _resumeButton;
    private Button _loadButton;
    private Button _saveButton;
    private Button _optionButton;
    private Button _quitButton;

    private PopupPanel _optionMenu;

    [Signal] public delegate void GameResumeEventHandler();

    public static PauseMenu Instance() => GD.Load<PackedScene>("res://Scenes/UI/pause_menu.tscn").Instantiate<PauseMenu>();

    public override void _Ready()
    {
        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _resumeButton = GetNode<Button>("%ResumeButton");
        _loadButton = GetNode<Button>("%LoadButton");
        _saveButton = GetNode<Button>("%SaveButton");
        _optionButton = GetNode<Button>("%OptionButton");
        _quitButton = GetNode<Button>("%QuitButton");

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
        EmitSignal(SignalName.GameResume);
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
