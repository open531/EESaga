namespace EESaga.Scripts.Utilities;

using Godot;

public partial class GameOptions : Node
{
    public Theme DefaultTheme { get; set; }
    public FontFile UniFont { get; set; }
    public FontFile UniFontJp { get; set; }
    public OptionData.Language GameLanguage { get; set; }
    public OptionData.DisplayMode VideoDisplayMode { get; set; }
    public OptionData.Resolution VideoResolution { get; set; }
    public OptionData.FrameRate VideoFrameRate { get; set; }
    public bool VideoVSync { get; set; }
    public bool VideoDisplayFps { get; set; }
    public int AudioVolume { get; set; }
    public int AudioMusic { get; set; }
    public int AudioSound { get; set; }
    public int AudioVoice { get; set; }

    public override void _Ready()
    {
        DefaultTheme = ResourceLoader.Load<Theme>("res://Assets/Themes/default.tres");
        UniFont = ResourceLoader.Load<FontFile>("res://Assets/Fonts/unifont.otf");
        UniFontJp = ResourceLoader.Load<FontFile>("res://Assets/Fonts/unifont_jp.otf");
        if (!LoadOptions())
        {
            GenerateOptions();
        }
        ApplyOptions();
    }

    public void ApplyOptions()
    {
        TranslationServer.SetLocale(GameLanguage switch
        {
            OptionData.Language.En => "en",
            OptionData.Language.Ja => "ja",
            OptionData.Language.ZhCn => "zh_CN",
            OptionData.Language.ZhTw => "zh_TW",
            _ => "en",
        });
        DefaultTheme.DefaultFont = GameLanguage switch
        {
            OptionData.Language.En => UniFont,
            OptionData.Language.Ja => UniFontJp,
            OptionData.Language.ZhCn => UniFont,
            OptionData.Language.ZhTw => UniFont,
            _ => UniFont,
        };
        DisplayServer.WindowSetMode(VideoDisplayMode switch
        {
            OptionData.DisplayMode.Windowed => DisplayServer.WindowMode.Windowed,
            OptionData.DisplayMode.Borderless => DisplayServer.WindowMode.Windowed,
            OptionData.DisplayMode.Fullscreen => DisplayServer.WindowMode.Fullscreen,
            _ => DisplayServer.WindowMode.Windowed,
        });
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, VideoDisplayMode == OptionData.DisplayMode.Borderless);
        DisplayServer.WindowSetSize(VideoResolution switch
        {
            OptionData.Resolution._640x360 => new Vector2I(640, 360),
            OptionData.Resolution._854x480 => new Vector2I(854, 480),
            OptionData.Resolution._960x540 => new Vector2I(960, 540),
            OptionData.Resolution._1024x576 => new Vector2I(1024, 576),
            OptionData.Resolution._1280x720 => new Vector2I(1280, 720),
            OptionData.Resolution._1366x768 => new Vector2I(1366, 768),
            OptionData.Resolution._1600x900 => new Vector2I(1600, 900),
            OptionData.Resolution._1920x1080 => new Vector2I(1920, 1080),
            OptionData.Resolution._2560x1440 => new Vector2I(2560, 1440),
            _ => new Vector2I(1280, 720),
        });
        Engine.PhysicsTicksPerSecond = VideoFrameRate switch
        {
            OptionData.FrameRate._24fps => 24,
            OptionData.FrameRate._30fps => 30,
            OptionData.FrameRate._45fps => 45,
            OptionData.FrameRate._60fps => 60,
            OptionData.FrameRate._90fps => 90,
            OptionData.FrameRate._120fps => 120,
            OptionData.FrameRate._144fps => 144,
            OptionData.FrameRate._240fps => 240,
            OptionData.FrameRate._360fps => 360,
            _ => 60,
        };
        DisplayServer.WindowSetVsyncMode(VideoVSync ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
    }

    public void SaveOptions()
    {
        var optionsFile = new ConfigFile();
        optionsFile.SetValue("Game", "Language", (int)GameLanguage);
        optionsFile.SetValue("Video", "DisplayMode", (int)VideoDisplayMode);
        optionsFile.SetValue("Video", "Resolution", (int)VideoResolution);
        optionsFile.SetValue("Video", "FrameRate", (int)VideoFrameRate);
        optionsFile.SetValue("Video", "VSync", VideoVSync);
        optionsFile.SetValue("Video", "DisplayFps", VideoDisplayFps);
        optionsFile.SetValue("Audio", "Volume", AudioVolume);
        optionsFile.SetValue("Audio", "Music", AudioMusic);
        optionsFile.SetValue("Audio", "Sound", AudioSound);
        optionsFile.SetValue("Audio", "Voice", AudioVoice);
        var error = optionsFile.Save("user://options.cfg");
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to save options: {error}");
        }
    }

    public bool LoadOptions()
    {
        var optionsFile = new ConfigFile();
        var error = optionsFile.Load("user://options.cfg");
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to load options: {error}");
            return false;
        }

        GameLanguage = (OptionData.Language)optionsFile.GetValue("Game", "Language", (int)GameLanguage).AsInt32();
        VideoDisplayMode = (OptionData.DisplayMode)optionsFile.GetValue("Video", "DisplayMode", (int)VideoDisplayMode).AsInt32();
        VideoResolution = (OptionData.Resolution)optionsFile.GetValue("Video", "Resolution", (int)VideoResolution).AsInt32();
        VideoFrameRate = (OptionData.FrameRate)optionsFile.GetValue("Video", "FrameRate", (int)VideoFrameRate).AsInt32();
        VideoVSync = optionsFile.GetValue("Video", "VSync", VideoVSync).AsBool();
        VideoDisplayFps = optionsFile.GetValue("Video", "DisplayFps", VideoDisplayFps).AsBool();
        AudioVolume = optionsFile.GetValue("Audio", "Volume", AudioVolume).AsInt32();
        AudioMusic = optionsFile.GetValue("Audio", "Music", AudioMusic).AsInt32();
        AudioSound = optionsFile.GetValue("Audio", "Sound", AudioSound).AsInt32();
        AudioVoice = optionsFile.GetValue("Audio", "Voice", AudioVoice).AsInt32();

        return true;
    }

    public void GenerateOptions()
    {
        GameLanguage = OS.GetLocale() switch
        {
            "en" => OptionData.Language.En,
            "ja" => OptionData.Language.Ja,
            "zh_CN" => OptionData.Language.ZhCn,
            "zh_TW" => OptionData.Language.ZhTw,
            _ => OptionData.Language.En,
        };
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
        VideoFrameRate = Engine.PhysicsTicksPerSecond switch
        {
            24 => OptionData.FrameRate._24fps,
            30 => OptionData.FrameRate._30fps,
            45 => OptionData.FrameRate._45fps,
            60 => OptionData.FrameRate._60fps,
            90 => OptionData.FrameRate._90fps,
            120 => OptionData.FrameRate._120fps,
            144 => OptionData.FrameRate._144fps,
            240 => OptionData.FrameRate._240fps,
            360 => OptionData.FrameRate._360fps,
            _ => OptionData.FrameRate._60fps,
        };
        VideoVSync = DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled;
        VideoDisplayFps = false;
        AudioVolume = 80;
        AudioMusic = 80;
        AudioSound = 80;
        AudioVoice = 80;
    }
}
