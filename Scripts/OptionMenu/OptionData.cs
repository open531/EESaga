using Godot;
using System;

namespace EESaga.Scripts.OptionMenu
{
    public static class OptionData
    {
        public enum DisplayMode
        {
            Windowed,
            Borderless,
            Fullscreen,
        }

        public enum Resolution
        {
            _640x360,
            _1280x720,
            _1366x768,
            _1600x900,
            _1920x1080,
            _2560x1440,
        }
    }
}
