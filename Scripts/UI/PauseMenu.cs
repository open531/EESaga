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

    private LoadMenu _loadMenu;
    private SaveMenu _saveMenu;
    private OptionMenu _optionMenu;

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

        _loadMenu = GetNode<LoadMenu>("LoadMenu");
        _saveMenu = GetNode<SaveMenu>("SaveMenu");
        _optionMenu = GetNode<OptionMenu>("OptionMenu");

        _resumeButton.Pressed += OnResumeButtonPressed;
        _loadButton.Pressed += OnLoadButtonPressed;
        _saveButton.Pressed += OnSaveButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _marginContainer.Position = new Vector2(0, 360 - _marginContainer.Size.Y);
    }

    private void OnResumeButtonPressed()
    {
        GetTree().Paused = false;
        EmitSignal(SignalName.GameResume);
    }

    private void OnLoadButtonPressed()
    {
        _loadMenu.PopupCentered();
        _loadMenu.UpdateSaveData();
    }

    private void OnSaveButtonPressed()
    {
        _saveMenu.PopupCentered();
        _saveMenu.UpdateSaveData();
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
