namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class DebugInfo : Control
{
    private GameOptions _gameOptions;

    private Label _fpsLabel;

    public override void _Ready()
    {
        _gameOptions = GetNode<GameOptions>("/root/GameOptions");

        _fpsLabel = GetNode<Label>("FPSLabel");

        Visible = false;
    }

    public override void _Process(double delta)
    {
        if (_gameOptions.VideoDisplayFps)
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
