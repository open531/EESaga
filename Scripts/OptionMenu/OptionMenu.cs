namespace EESaga.Scripts.OptionMenu;

using Godot;
using Options;

public partial class OptionMenu : Popup
{
    private GameOptions _gameOptions;

    private OptionButton _languageButton;
    private OptionButton _displayModeButton;
    private OptionButton _resolutionButton;
    private OptionButton _frameRateButton;
    private CheckBox _vSyncButton;
    private CheckBox _displayFpsButton;


    public override void _Ready()
    {
        _gameOptions = GetNode<GameOptions>("/root/GameOptions");

        _languageButton = GetNode<OptionButton>("TabContainer/OP_GAME/MarginContainer/GridContainer/LanguageButton");
        _displayModeButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/DisplayModeButton");
        _resolutionButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/ResolutionButton");
        _frameRateButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/FrameRateButton");
        _vSyncButton = GetNode<CheckBox>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/VSyncButton");
        _displayFpsButton = GetNode<CheckBox>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/DisplayFPSButton");

        _languageButton.Selected = (int)_gameOptions.Language;
        _displayModeButton.Selected = (int)_gameOptions.VideoDisplayMode;
        _resolutionButton.Selected = (int)_gameOptions.VideoResolution;
        _frameRateButton.Selected = (int)_gameOptions.VideoFrameRate;
        _vSyncButton.ButtonPressed = _gameOptions.VideoVSync;
        _displayFpsButton.ButtonPressed = _gameOptions.VideoDisplayFps;

        _languageButton.ItemSelected += OnLanguageButtonItemSelected;
        _displayModeButton.ItemSelected += OnDisplayModeButtonItemSelected;
        _resolutionButton.ItemSelected += OnResolutionButtonItemSelected;
        _frameRateButton.ItemSelected += OnFrameRateButtonItemSelected;
        _vSyncButton.Pressed += OnVSyncButtonPressed;
        _displayFpsButton.Pressed += OnDisplayFpsButtonPressed;

        _frameRateButton.Disabled = true;
    }

    private void OnLanguageButtonItemSelected(long id)
    {
        _gameOptions.Language = (OptionData.Language)_languageButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnDisplayModeButtonItemSelected(long id)
    {
        _gameOptions.VideoDisplayMode = (OptionData.DisplayMode)_displayModeButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnResolutionButtonItemSelected(long id)
    {
        _gameOptions.VideoResolution = (OptionData.Resolution)_resolutionButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnFrameRateButtonItemSelected(long id)
    {
        _gameOptions.VideoFrameRate = (OptionData.FrameRate)_frameRateButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnVSyncButtonPressed()
    {
        _gameOptions.VideoVSync = _vSyncButton.ButtonPressed;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnDisplayFpsButtonPressed()
    {
        _gameOptions.VideoDisplayFps = _displayFpsButton.ButtonPressed;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }
}
