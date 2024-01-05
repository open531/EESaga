using Godot;
using System;

public partial class Title : Control
{
    public override void _Ready()
    {
        var playButton = GetNode<Button>("PlayButton");
        playButton.Pressed += OnPlayButtonPressed;
    }

    private void OnPlayButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/battle_scene.tscn");
    }
}
