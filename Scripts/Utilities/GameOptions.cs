namespace EESaga.Scripts.Utilities;

using Godot;
using static Data.OptionData;

public partial class GameOptions : Node
{
    public static Theme DefaultTheme { get; set; }
    public static FontFile UniFont { get; set; }
    public static FontFile UniFontJp { get; set; }
    public static FontFile Misaki { get; set; }
    public static Resource Cursor { get; set; }
    public static Resource CursorGreen { get; set; }
    public static Resource CursorRed { get; set; }
    public Language GameLanguage { get; set; }
    public DisplayMode VideoDisplayMode { get; set; }
    public Resolution VideoResolution { get; set; }
    public FrameRate VideoFrameRate { get; set; }
    public bool VideoVSync { get; set; }
    public bool VideoDisplayFps { get; set; }
    public int AudioVolume { get; set; }
    public int AudioMusic { get; set; }
    public int AudioSound { get; set; }
    public int AudioVoice { get; set; }

    public override void _Ready()
    {
        DefaultTheme = GD.Load<Theme>("res://Assets/Resources/theme_default.tres");
        UniFont = GD.Load<FontFile>("res://Assets/Fonts/unifont.otf");
        UniFontJp = GD.Load<FontFile>("res://Assets/Fonts/unifont_jp.otf");
        Misaki = GD.Load<FontFile>("res://Assets/Fonts/misaki.ttf");
        Cursor = GD.Load<Resource>("res://Assets/Textures/cursor.png");
        CursorGreen = GD.Load<Resource>("res://Assets/Textures/cursor_green.png");
        CursorRed = GD.Load<Resource>("res://Assets/Textures/cursor_red.png");
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
            Language.En => "en",
            Language.Ja => "ja",
            Language.ZhCn => "zh_CN",
            Language.ZhTw => "zh_TW",
            _ => "en",
        });
        DefaultTheme.DefaultFont = GameLanguage switch
        {
            Language.En => UniFont,
            Language.Ja => UniFontJp,
            Language.ZhCn => UniFont,
            Language.ZhTw => UniFont,
            _ => UniFont,
        };
        DisplayServer.WindowSetMode(VideoDisplayMode switch
        {
            DisplayMode.Windowed => DisplayServer.WindowMode.Windowed,
            DisplayMode.Borderless => DisplayServer.WindowMode.Windowed,
            DisplayMode.Fullscreen => DisplayServer.WindowMode.Fullscreen,
            _ => DisplayServer.WindowMode.Windowed,
        });
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, VideoDisplayMode == DisplayMode.Borderless);
        DisplayServer.WindowSetSize(VideoResolution switch
        {
            Resolution._640x360 => new Vector2I(640, 360),
            Resolution._854x480 => new Vector2I(854, 480),
            Resolution._960x540 => new Vector2I(960, 540),
            Resolution._1024x576 => new Vector2I(1024, 576),
            Resolution._1280x720 => new Vector2I(1280, 720),
            Resolution._1366x768 => new Vector2I(1366, 768),
            Resolution._1600x900 => new Vector2I(1600, 900),
            Resolution._1920x1080 => new Vector2I(1920, 1080),
            Resolution._2560x1440 => new Vector2I(2560, 1440),
            _ => new Vector2I(1280, 720),
        });
        Engine.PhysicsTicksPerSecond = VideoFrameRate switch
        {
            FrameRate._24fps => 24,
            FrameRate._30fps => 30,
            FrameRate._45fps => 45,
            FrameRate._60fps => 60,
            FrameRate._90fps => 90,
            FrameRate._120fps => 120,
            FrameRate._144fps => 144,
            FrameRate._240fps => 240,
            FrameRate._360fps => 360,
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

        GameLanguage = (Language)optionsFile.GetValue("Game", "Language", (int)GameLanguage).AsInt32();
        VideoDisplayMode = (DisplayMode)optionsFile.GetValue("Video", "DisplayMode", (int)VideoDisplayMode).AsInt32();
        VideoResolution = (Resolution)optionsFile.GetValue("Video", "Resolution", (int)VideoResolution).AsInt32();
        VideoFrameRate = (FrameRate)optionsFile.GetValue("Video", "FrameRate", (int)VideoFrameRate).AsInt32();
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
            "en" => Language.En,
            "ja" => Language.Ja,
            "zh_CN" => Language.ZhCn,
            "zh_TW" => Language.ZhTw,
            _ => Language.En,
        };
        VideoDisplayMode = DisplayServer.WindowGetMode() switch
        {
            DisplayServer.WindowMode.Fullscreen => DisplayMode.Fullscreen,
            DisplayServer.WindowMode.ExclusiveFullscreen => DisplayMode.Fullscreen,
            _ => DisplayServer.WindowGetFlag(DisplayServer.WindowFlags.Borderless) ? DisplayMode.Borderless : DisplayMode.Windowed,
        };
        VideoResolution = DisplayServer.WindowGetSize() switch
        {
            var size when size >= new Vector2I(2560, 1440) => Resolution._2560x1440,
            var size when size >= new Vector2I(1920, 1080) => Resolution._1920x1080,
            var size when size >= new Vector2I(1600, 900) => Resolution._1600x900,
            var size when size >= new Vector2I(1366, 768) => Resolution._1366x768,
            var size when size >= new Vector2I(1280, 720) => Resolution._1280x720,
            var size when size >= new Vector2I(1024, 576) => Resolution._1024x576,
            var size when size >= new Vector2I(960, 540) => Resolution._960x540,
            var size when size >= new Vector2I(854, 480) => Resolution._854x480,
            var size when size >= new Vector2I(640, 360) => Resolution._640x360,
            _ => Resolution._640x360,
        };
        VideoFrameRate = Engine.PhysicsTicksPerSecond switch
        {
            var fps when fps <= 24 => FrameRate._24fps,
            var fps when fps <= 30 => FrameRate._30fps,
            var fps when fps <= 45 => FrameRate._45fps,
            var fps when fps <= 60 => FrameRate._60fps,
            var fps when fps <= 90 => FrameRate._90fps,
            var fps when fps <= 120 => FrameRate._120fps,
            var fps when fps <= 144 => FrameRate._144fps,
            var fps when fps <= 240 => FrameRate._240fps,
            var fps when fps <= 360 => FrameRate._360fps,
            _ => FrameRate._360fps,
        };
        VideoVSync = DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled;
        VideoDisplayFps = false;
        AudioVolume = 80;
        AudioMusic = 80;
        AudioSound = 80;
        AudioVoice = 80;
    }

    public static void SetCursor(Resource cursor)
    {
        Input.SetCustomMouseCursor(cursor);
    }
}
