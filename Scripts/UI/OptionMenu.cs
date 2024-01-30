namespace EESaga.Scripts.UI;

using Godot;
using Utilities;
using static Data.OptionData;

public partial class OptionMenu : PopupPanel
{
    private GameOptions _gameOptions;

    private TabContainer _tabContainer;

    private TabBar _gameTab;
    private TabBar _videoTab;
    private TabBar _audioTab;

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

        _gameTab = GetNode<TabBar>("TabContainer/OP_GAME");
        _videoTab = GetNode<TabBar>("TabContainer/OP_VIDEO");
        _audioTab = GetNode<TabBar>("TabContainer/OP_AUDIO");

        _languageButton = GetNode<OptionButton>("%LanguageButton");
        _displayModeButton = GetNode<OptionButton>("%DisplayModeButton");
        _resolutionButton = GetNode<OptionButton>("%ResolutionButton");
        _frameRateButton = GetNode<OptionButton>("%FrameRateButton");
        _vSyncButton = GetNode<CheckBox>("%VSyncButton");
        _displayFpsButton = GetNode<CheckBox>("%DisplayFPSButton");
        _volumeSlider = GetNode<Slider>("%VolumeSlider");
        _musicSlider = GetNode<Slider>("%MusicSlider");
        _soundSlider = GetNode<Slider>("%SoundSlider");
        _voiceSlider = GetNode<Slider>("%VoiceSlider");

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

        AboutToPopup += UpdateFocus;
        _tabContainer.TabChanged += (long tab) => UpdateFocus();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_tab_next"))
        {
            _tabContainer.CurrentTab = (_tabContainer.CurrentTab + 1) % _tabContainer.GetChildCount();
            UpdateFocus();
        }
        if (Input.IsActionPressed("ui_tab_prev"))
        {
            _tabContainer.CurrentTab = (_tabContainer.CurrentTab - 1 + _tabContainer.GetChildCount()) % _tabContainer.GetChildCount();
            UpdateFocus();
        }
    }

    public void UpdateFocus()
    {
        switch (_tabContainer.CurrentTab)
        {
            case 0:
                _languageButton.GrabFocus();
                break;
            case 1:
                _displayModeButton.GrabFocus();
                break;
            case 2:
                _volumeSlider.GrabFocus();
                break;
            default:
                break;
        }
    }

    #region Game
    private void OnLanguageButtonItemSelected(long id)
    {
        _gameOptions.GameLanguage = (Language)_languageButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }
    #endregion

    #region Video
    private void OnDisplayModeButtonItemSelected(long id)
    {
        _gameOptions.VideoDisplayMode = (DisplayMode)_displayModeButton.Selected;
        if (_gameOptions.VideoDisplayMode == DisplayMode.Windowed)
        {
            _gameOptions.VideoResolution = Resolution._1280x720;
            _resolutionButton.Selected = (int)_gameOptions.VideoResolution;
            _resolutionButton.Disabled = false;
        }
        else
        {
            _gameOptions.VideoResolution = DisplayServer.ScreenGetSize().X switch
            {
                var x when x >= 2560 => Resolution._2560x1440,
                var x when x >= 1920 => Resolution._1920x1080,
                var x when x >= 1600 => Resolution._1600x900,
                var x when x >= 1366 => Resolution._1366x768,
                var x when x >= 1280 => Resolution._1280x720,
                var x when x >= 1024 => Resolution._1024x576,
                var x when x >= 960 => Resolution._960x540,
                var x when x >= 854 => Resolution._854x480,
                var x when x >= 640 => Resolution._640x360,
                _ => Resolution._640x360,
            };
            _resolutionButton.Selected = (int)_gameOptions.VideoResolution;
            _resolutionButton.Disabled = true;
        }
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnResolutionButtonItemSelected(long id)
    {
        _gameOptions.VideoResolution = (Resolution)_resolutionButton.Selected;
        _gameOptions.ApplyOptions();
        _gameOptions.SaveOptions();
    }

    private void OnFrameRateButtonItemSelected(long id)
    {
        _gameOptions.VideoFrameRate = (FrameRate)_frameRateButton.Selected;
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
