using EESaga.Scripts.Utilities;
using Godot.Collections;

namespace EESaga.Scripts.Options;

public static class OptionData
{
    public enum Language
    {
        En,
        Ja,
        ZhCn,
        ZhTw,
    }

    public enum DisplayMode
    {
        Windowed,
        Borderless,
        Fullscreen,
    }

    public enum Resolution
    {
        _640x360,
        _854x480,
        _960x540,
        _1024x576,
        _1280x720,
        _1366x768,
        _1600x900,
        _1920x1080,
        _2560x1440,
    }

    public enum FrameRate
    {
        _24fps,
        _30fps,
        _45fps,
        _60fps,
        _90fps,
        _120fps,
        _144fps,
        _240fps,
        _360fps,
    }
}
