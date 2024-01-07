namespace EESaga.Scripts.Autoload.Options;

using Godot;

public partial class GameOptions : Node
{
    public OptionData.DisplayMode VideoDisplayMode { get; set; }
    public OptionData.Resolution VideoResolution { get; set; }
    public bool VideoVSync { get; set; }
    public bool VideoDisplayFps { get; set; }

    public override void _Ready()
    {
        VideoDisplayMode = DisplayServer.WindowGetMode() switch
        {
            DisplayServer.WindowMode.Fullscreen => OptionData.DisplayMode.Fullscreen,
            DisplayServer.WindowMode.ExclusiveFullscreen => OptionData.DisplayMode.Fullscreen,
            _ => DisplayServer.WindowGetFlag(DisplayServer.WindowFlags.Borderless) ? OptionData.DisplayMode.Borderless : OptionData.DisplayMode.Windowed,
        };
        VideoResolution = DisplayServer.WindowGetSize() switch
        {
            var size when size == new Vector2I(640, 360) => OptionData.Resolution._640x360,
            var size when size == new Vector2I(854, 480) => OptionData.Resolution._854x480,
            var size when size == new Vector2I(960, 540) => OptionData.Resolution._960x540,
            var size when size == new Vector2I(1024, 576) => OptionData.Resolution._1024x576,
            var size when size == new Vector2I(1280, 720) => OptionData.Resolution._1280x720,
            var size when size == new Vector2I(1366, 768) => OptionData.Resolution._1366x768,
            var size when size == new Vector2I(1600, 900) => OptionData.Resolution._1600x900,
            var size when size == new Vector2I(1920, 1080) => OptionData.Resolution._1920x1080,
            var size when size == new Vector2I(2560, 1440) => OptionData.Resolution._2560x1440,
            _ => OptionData.Resolution._1280x720,
        };
        VideoVSync = DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled;
        VideoDisplayFps = false;
    }
}
