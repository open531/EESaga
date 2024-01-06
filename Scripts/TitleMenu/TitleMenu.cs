using Godot;
using System;

namespace EESaga.Scripts.TitleMenu
{
    public partial class TitleMenu : Control
    {
        private Button _playButton;
        private Button _optionButton;
        private Button _quitButton;
        
        private Popup _optionMenu;

        public override void _Ready()
        {
            _playButton = GetNode<Button>("MarginContainer/GridContainer/PlayButton");
            _optionButton = GetNode<Button>("MarginContainer/GridContainer/OptionButton");
            _quitButton = GetNode<Button>("MarginContainer/GridContainer/QuitButton");
            
            _optionMenu = GetNode<Popup>("OptionMenu");

            _playButton.Pressed += OnPlayButtonPressed;
            _optionButton.Pressed += OnOptionButtonPressed;
            _quitButton.Pressed += OnQuitButtonPressed;

            _optionMenu.Hide();
        }

        private void OnPlayButtonPressed()
        {
            GetTree().ChangeSceneToFile("res://Scenes/open_world.tscn");
        }

        private void OnOptionButtonPressed()
        {
            _optionMenu.PopupCentered();
        }

        private void OnQuitButtonPressed()
        {
            GetTree().Quit();
        }
    }
}
