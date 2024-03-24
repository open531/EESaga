namespace EESaga.Scripts.UI;

using Godot;
using Utilities;
using static Data.OptionData;

public partial class OptionMenu : PopupPanel
{
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

    public static OptionMenu Instance() => GD.Load<PackedScene>("res://Scenes/UI/option_menu.tscn").Instantiate<OptionMenu>();

    public override void _Ready()
    {
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

        _languageButton.Selected = (int)GameOptions.GameLanguage;
        _displayModeButton.Selected = (int)GameOptions.VideoDisplayMode;
        _resolutionButton.Selected = (int)GameOptions.VideoResolution;
        _frameRateButton.Selected = (int)GameOptions.VideoFrameRate;
        _vSyncButton.ButtonPressed = GameOptions.VideoVSync;
        _displayFpsButton.ButtonPressed = GameOptions.VideoDisplayFps;
        _volumeSlider.Value = GameOptions.AudioVolume;
        _musicSlider.Value = GameOptions.AudioMusic;
        _soundSlider.Value = GameOptions.AudioSound;
        _voiceSlider.Value = GameOptions.AudioVoice;

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

        //AboutToPopup += UpdateFocus;
        //_tabContainer.TabChanged += (long tab) => UpdateFocus();
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
        GameOptions.GameLanguage = (Language)_languageButton.Selected;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }
    #endregion

    #region Video
    private void OnDisplayModeButtonItemSelected(long id)
    {
        GameOptions.VideoDisplayMode = (DisplayMode)_displayModeButton.Selected;
        if (GameOptions.VideoDisplayMode == DisplayMode.Windowed)
        {
            GameOptions.VideoResolution = Resolution._1280x720;
            _resolutionButton.Selected = (int)GameOptions.VideoResolution;
            _resolutionButton.Disabled = false;
        }
        else
        {
            GameOptions.VideoResolution = DisplayServer.ScreenGetSize().X switch
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
            _resolutionButton.Selected = (int)GameOptions.VideoResolution;
            _resolutionButton.Disabled = true;
        }
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnResolutionButtonItemSelected(long id)
    {
        GameOptions.VideoResolution = (Resolution)_resolutionButton.Selected;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnFrameRateButtonItemSelected(long id)
    {
        GameOptions.VideoFrameRate = (FrameRate)_frameRateButton.Selected;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnVSyncButtonPressed()
    {
        GameOptions.VideoVSync = _vSyncButton.ButtonPressed;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnDisplayFpsButtonPressed()
    {
        GameOptions.VideoDisplayFps = _displayFpsButton.ButtonPressed;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }
    #endregion

    #region Audio
    private void OnVolumeSliderValueChanged(double value)
    {
        GameOptions.AudioVolume = (int)value;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnMusicSliderValueChanged(double value)
    {
        GameOptions.AudioMusic = (int)value;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnSoundSliderValueChanged(double value)
    {
        GameOptions.AudioSound = (int)value;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }

    private void OnVoiceSliderValueChanged(double value)
    {
        GameOptions.AudioVoice = (int)value;
        GameOptions.ApplyOptions();
        GameOptions.SaveOptions();
    }
    #endregion
}
