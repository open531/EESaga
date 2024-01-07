namespace EESaga.Scripts.OptionMenu;

using Autoload.Options;
using Godot;

public partial class OptionMenu : Popup
{
    private GameOptions _gameOptions;

    private OptionButton _displayModeButton;
    private OptionButton _resolutionButton;
    private CheckBox _vSyncButton;
    private CheckBox _displayFpsButton;


    public override void _Ready()
    {
        _gameOptions = GetNode<GameOptions>("/root/GameOptions");

        _displayModeButton = GetNode<OptionButton>("TabContainer/Video/MarginContainer/GridContainer/DisplayModeButton");
        _resolutionButton = GetNode<OptionButton>("TabContainer/Video/MarginContainer/GridContainer/ResolutionButton");
        _vSyncButton = GetNode<CheckBox>("TabContainer/Video/MarginContainer/GridContainer/VSyncButton");
        _displayFpsButton = GetNode<CheckBox>("TabContainer/Video/MarginContainer/GridContainer/DisplayFPSButton");

        _displayModeButton.Selected = (int)_gameOptions.VideoDisplayMode;
        _resolutionButton.Selected = (int)_gameOptions.VideoResolution;
        _vSyncButton.ButtonPressed = _gameOptions.VideoVSync;
        _displayFpsButton.ButtonPressed = _gameOptions.VideoDisplayFps;

        _displayModeButton.ItemSelected += OnDisplayModeButtonItemSelected;
        _resolutionButton.ItemSelected += OnResolutionButtonItemSelected;
        _vSyncButton.Pressed += OnVSyncButtonPressed;
        _displayFpsButton.Pressed += OnDisplayFpsButtonPressed;
    }

    private void OnDisplayModeButtonItemSelected(long id)
    {
        switch (id)
        {
            case (long)OptionData.DisplayMode.Windowed:
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
                OnResolutionButtonItemSelected(_resolutionButton.Selected);
                break;
            case (long)OptionData.DisplayMode.Borderless:
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
                OnResolutionButtonItemSelected(_resolutionButton.Selected);
                break;
            case (long)OptionData.DisplayMode.Fullscreen:
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
                OnResolutionButtonItemSelected(_resolutionButton.Selected);
                break;
        }
        _gameOptions.VideoDisplayMode = (OptionData.DisplayMode)_displayModeButton.Selected;
    }

    private void OnResolutionButtonItemSelected(long id)
    {
        switch (id)
        {
            case (long)OptionData.Resolution._640x360:
                DisplayServer.WindowSetSize(new Vector2I(640, 360));
                break;
            case (long)OptionData.Resolution._854x480:
                DisplayServer.WindowSetSize(new Vector2I(854, 480));
                break;
            case (long)OptionData.Resolution._960x540:
                DisplayServer.WindowSetSize(new Vector2I(960, 540));
                break;
            case (long)OptionData.Resolution._1024x576:
                DisplayServer.WindowSetSize(new Vector2I(1024, 576));
                break;
            case (long)OptionData.Resolution._1280x720:
                DisplayServer.WindowSetSize(new Vector2I(1280, 720));
                break;
            case (long)OptionData.Resolution._1366x768:
                DisplayServer.WindowSetSize(new Vector2I(1366, 768));
                break;
            case (long)OptionData.Resolution._1600x900:
                DisplayServer.WindowSetSize(new Vector2I(1600, 900));
                break;
            case (long)OptionData.Resolution._1920x1080:
                DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
                break;
            case (long)OptionData.Resolution._2560x1440:
                DisplayServer.WindowSetSize(new Vector2I(2560, 1440));
                break;
        }
        _gameOptions.VideoResolution = (OptionData.Resolution)_resolutionButton.Selected;
    }

    private void OnVSyncButtonPressed()
    {
        DisplayServer.WindowSetVsyncMode(_vSyncButton.ButtonPressed ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
        _gameOptions.VideoVSync = _vSyncButton.ButtonPressed;
    }

    private void OnDisplayFpsButtonPressed()
    {
        _gameOptions.VideoDisplayFps = _displayFpsButton.ButtonPressed;
    }
}
