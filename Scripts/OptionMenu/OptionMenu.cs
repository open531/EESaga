using Godot;
using System;

namespace EESaga.Scripts.OptionMenu
{
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
}
