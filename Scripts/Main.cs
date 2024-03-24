namespace EESaga.Scripts;

using Godot;
using UI;
using Utilities;

public partial class Main : Node
{
    private SceneSwitcher _sceneSwitcher;
    private PauseMenu _pauseMenu = PauseMenu.Instance();

    [Signal] public delegate void GamePauseEventHandler();
    [Signal] public delegate void GameResumeEventHandler();

    public override void _Ready()
    {
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        _pauseMenu.GameResume += OnGameResume;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.Escape)
            {
                if (_sceneSwitcher.AllowPause)
                {
                    if (!GetTree().Paused)
                    {
                        GetTree().Paused = true;
                        AddChild(_pauseMenu);
                        EmitSignal(SignalName.GamePause);
                    }
                }
            }
        }
    }

    private void OnGameResume()
    {
        GetTree().Paused = false;
        RemoveChild(_pauseMenu);
        EmitSignal(SignalName.GameResume);
    }
}
