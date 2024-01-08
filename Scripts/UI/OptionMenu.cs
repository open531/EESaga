namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class OptionMenu : Popup
{
    private GameOptions _gameOptions;

    private TabContainer _tabContainer;

    private OptionButton _languageButton;
    private OptionButton _displayModeButton;
    private OptionButton _resolutionButton;
    private OptionButton _frameRateButton;
    private CheckBox _vSyncButton;
    private CheckBox _displayFpsButton;
    private Slider _volumeSlider;
    private Slider _musicSlider;
    private Slider _soundSlider;
    private Slider _voiceSlider;


    public override void _Ready()
    {
        _gameOptions = GetNode<GameOptions>("/root/GameOptions");

        _tabContainer = GetNode<TabContainer>("TabContainer");

        _languageButton = GetNode<OptionButton>("TabContainer/OP_GAME/MarginContainer/GridContainer/LanguageButton");
        _displayModeButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/DisplayModeButton");
        _resolutionButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/ResolutionButton");
        _frameRateButton = GetNode<OptionButton>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/FrameRateButton");
        _vSyncButton = GetNode<CheckBox>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/VSyncButton");
        _displayFpsButton = GetNode<CheckBox>("TabContainer/OP_VIDEO/MarginContainer/GridContainer/DisplayFPSButton");
        _volumeSlider = GetNode<Slider>("TabContainer/OP_AUDIO/MarginContainer/GridContainer/VolumeSlider");
        _musicSlider = GetNode<Slider>("TabContainer/OP_AUDIO/MarginContainer/GridContainer/MusicSlider");
        _soundSlider = GetNode<Slider>("TabContainer/OP_AUDIO/MarginContainer/GridContainer/SoundSlider");
        _voiceSlider = GetNode<Slider>("TabContainer/OP_AUDIO/MarginContainer/GridContainer/VoiceSlider");

        _tabContainer.CurrentTab = 0;

        _languageButton.Selected = (int)_gameOptions.GameLanguage;
        _displayModeButton.Selected = (int)_gameOptions.VideoDisplayMode;
        _resolutionButton.Selected = (int)_gameOptions.VideoResolution;
        _frameRateButton.Selected = (int)_gameOptions.VideoFrameRate;
        _vSyncButton.ButtonPressed = _gameOptions.VideoVSync;
        _displayFpsButton.ButtonPressed = _gameOptions.VideoDisplayFps;
        _volumeSlider.Value = _gameOptions.AudioVolume;
        _musicSlider.Value = _gameOptions.AudioMusic;
        _soundSlider.Value = _gameOptions.AudioSound;
        _voiceSlider.Value = _gameOptions.AudioVoice;

        _languageButton.ItemSelected += OnLanguageButtonItemSelected;
        _displayModeButton.ItemSelected += OnDisplayModeButtonItemSelected;
        _resolutionButton.ItemSelected += OnResolutionButtonItemSelected;
        _frameRateButton.ItemSelected += OnFrameRateButtonItemSelected;
        _vSyncButton.Pressed += OnVSyncButtonPressed;
        _displayFpsButton.Pressed += OnDisplayFpsButtonPressed;
        _volumeSlider.ValueChanged += OnVolumeSliderValueChanged;
        _musicSlider.ValueChanged += OnMusicSliderValueChanged;
        _soundSlider.ValueChanged += OnSoundSliderValueChanged;
        _voiceSlider.ValueChanged += OnVoiceSliderValueChanged;

        _frameRateButton.Disabled = true;
    }

    #region Game
    private void OnLanguageButtonItemSelected(long id)
    {
        _gameOptions.GameLanguage = (OptionData.Language)_languageButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }
    #endregion

    #region Video
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
    #endregion

    #region Audio
    private void OnVolumeSliderValueChanged(double value)
    {
        _gameOptions.AudioVolume = (int)value;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnMusicSliderValueChanged(double value)
    {
        _gameOptions.AudioMusic = (int)value;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnSoundSliderValueChanged(double value)
    {
        _gameOptions.AudioSound = (int)value;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnVoiceSliderValueChanged(double value)
    {
        _gameOptions.AudioVoice = (int)value;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }
    #endregion
}
