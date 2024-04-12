namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class DebugInfo : Control
{
    private GameOptions _gameOptions;

    private Label _fpsLabel;

    public static DebugInfo Instance() => GD.Load<PackedScene>("res://Scenes/UI/debug_info.tscn").Instantiate<DebugInfo>();

    public override void _Ready()
    {
        _gameOptions = GetNode<GameOptions>("/root/GameOptions");

        _fpsLabel = GetNode<Label>("FPSLabel");

        Visible = false;
    }

    public override void _Process(double delta)
    {
        if (GameOptions.VideoDisplayFps)
        {
            Visible = true;
            _fpsLabel.Text = $"FPS: {Engine.GetFramesPerSecond()}";
        }
        else
        {
            Visible = false;
        }
    }
}
