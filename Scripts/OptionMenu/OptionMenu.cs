namespace EESaga.Scripts.OptionMenu;

using Godot;

public partial class OptionMenu : Popup
{
    private OptionButton _displayModeButton;
    private OptionButton _resolutionButton;
    private CheckButton _vSyncButton;

    public override void _Ready()
    {
        _displayModeButton = GetNode<OptionButton>("TabContainer/Video/MarginContainer/GridContainer/DisplayModeButton");
        _resolutionButton = GetNode<OptionButton>("TabContainer/Video/MarginContainer/GridContainer/ResolutionButton");
        _vSyncButton = GetNode<CheckButton>("TabContainer/Video/MarginContainer/GridContainer/VSyncButton");

        switch (DisplayServer.WindowGetMode())
        {
            case DisplayServer.WindowMode.Fullscreen:
            case DisplayServer.WindowMode.ExclusiveFullscreen:
                _displayModeButton.Selected = (int)OptionData.DisplayMode.Fullscreen;
                break;
            default:
                if (DisplayServer.WindowGetFlag(DisplayServer.WindowFlags.Borderless))
                {
                    _displayModeButton.Selected = (int)OptionData.DisplayMode.Borderless;
                }
                else
                {
                    _displayModeButton.Selected = (int)OptionData.DisplayMode.Windowed;
                }
                break;
        }
        switch (DisplayServer.WindowGetSize())
        {
            case var size when size == new Vector2I(640, 360):
                _resolutionButton.Selected = (int)OptionData.Resolution._640x360;
                break;
            case var size when size == new Vector2I(854, 480):
                _resolutionButton.Selected = (int)OptionData.Resolution._854x480;
                break;
            case var size when size == new Vector2I(960, 540):
                _resolutionButton.Selected = (int)OptionData.Resolution._960x540;
                break;
            case var size when size == new Vector2I(1024, 576):
                _resolutionButton.Selected = (int)OptionData.Resolution._1024x576;
                break;
            case var size when size == new Vector2I(1280, 720):
                _resolutionButton.Selected = (int)OptionData.Resolution._1280x720;
                break;
            case var size when size == new Vector2I(1366, 768):
                _resolutionButton.Selected = (int)OptionData.Resolution._1366x768;
                break;
            case var size when size == new Vector2I(1600, 900):
                _resolutionButton.Selected = (int)OptionData.Resolution._1600x900;
                break;
            case var size when size == new Vector2I(1920, 1080):
                _resolutionButton.Selected = (int)OptionData.Resolution._1920x1080;
                break;
            case var size when size == new Vector2I(2560, 1440):
                _resolutionButton.Selected = (int)OptionData.Resolution._2560x1440;
                break;
            default:
                _resolutionButton.Selected = (int)OptionData.Resolution._1280x720;
                OnDisplayModeButtonItemSelected(_displayModeButton.Selected);
                break;
        }
        _vSyncButton.ToggleMode = DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled;

        _displayModeButton.ItemSelected += OnDisplayModeButtonItemSelected;
        _resolutionButton.ItemSelected += OnResolutionButtonItemSelected;
        _vSyncButton.Pressed += OnVSyncButtonPressed;
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
    }

    private void OnVSyncButtonPressed()
    {
        DisplayServer.WindowSetVsyncMode(_vSyncButton.ToggleMode ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
    }
}
