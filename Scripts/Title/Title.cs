using Godot;
using System;

namespace EESaga.Scripts.Title
{
    public partial class Title : Control
    {
        public override void _Ready()
        {
            var playButton = GetNode<Button>("PlayButton");
            playButton.Pressed += OnPlayButtonPressed;
        }

        private void OnPlayButtonPressed()
        {
            GetTree().ChangeSceneToFile("res://Scenes/open_world.tscn");
        }
    }
}
