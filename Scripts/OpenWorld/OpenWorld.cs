using Godot;
using System;

namespace EESaga.Scripts.OpenWorld
{
    public partial class OpenWorld : Node2D
    {
        public override void _Ready()
        {
        }

        public override void _Process(double delta)
        {
            var camera = GetNode<Camera2D>("Player/Camera2D");

            if (Input.IsPhysicalKeyPressed(Key.Pagedown))
            {
                camera.Zoom /= 1.02f;
            }

            if (Input.IsPhysicalKeyPressed(Key.Pageup))
            {
                camera.Zoom *= 1.02f;
            }
        }
    }
}
