namespace EESaga.Scripts.OpenWorld;

using Godot;

public partial class OpenWorld : Node2D
{
    private Camera2D _camera2D;

    public override void _Ready()
    {
        _camera2D = GetNode<Camera2D>("Player/Camera2D");
    }

    public override void _Process(double delta)
    {
        if (Input.IsPhysicalKeyPressed(Key.Pagedown))
        {
            _camera2D.Zoom /= 1.02f;
        }

        if (Input.IsPhysicalKeyPressed(Key.Pageup))
        {
            _camera2D.Zoom *= 1.02f;
        }
    }
}
