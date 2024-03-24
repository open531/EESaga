namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class TitleMenu : CanvasLayer
{
    private SceneSwitcher _sceneSwitcher;

    private MarginContainer _marginContainer;
    private Button _playButton;
    private Button _loadButton;
    private Button _optionButton;
    private Button _quitButton;

    private LoadMenu _loadMenu;
    private OptionMenu _optionMenu;

    public static TitleMenu Instance() => GD.Load<PackedScene>("res://Scenes/UI/title_menu.tscn").Instantiate<TitleMenu>();

    public override void _Ready()
    {
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _playButton = GetNode<Button>("%PlayButton");
        _loadButton = GetNode<Button>("%LoadButton");
        _optionButton = GetNode<Button>("%OptionButton");
        _quitButton = GetNode<Button>("%QuitButton");

        _loadMenu = GetNode<LoadMenu>("LoadMenu");
        _optionMenu = GetNode<OptionMenu>("OptionMenu");

        _playButton.Pressed += OnPlayButtonPressed;
        _loadButton.Pressed += OnLoadButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _marginContainer.Position = new Vector2(0, 360 - _marginContainer.Size.Y);

        //_playButton.GrabFocus();
    }

    private void OnPlayButtonPressed()
    {
        _sceneSwitcher.PushScene(SceneSwitcher.BattleManagerScene);
    }

    private void OnLoadButtonPressed()
    {
        _loadMenu.PopupCentered();
        _loadMenu.UpdateSaveData();
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
