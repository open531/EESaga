namespace EESaga.Scripts.UI;

using Godot;

public partial class TitleMenu : Control
{
    private TextureRect _textureRect;
    private MarginContainer _marginContainer;
    private Button _playButton;
    private Button _optionButton;
    private Button _quitButton;

    private Popup _optionMenu;

    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");
        _marginContainer = GetNode<MarginContainer>("MarginContainer");
        _playButton = GetNode<Button>("MarginContainer/GridContainer/PlayButton");
        _optionButton = GetNode<Button>("MarginContainer/GridContainer/OptionButton");
        _quitButton = GetNode<Button>("MarginContainer/GridContainer/QuitButton");

        _optionMenu = GetNode<Popup>("OptionMenu");

        _playButton.Pressed += OnPlayButtonPressed;
        _optionButton.Pressed += OnOptionButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;

        _optionMenu.Hide();

        _marginContainer.Position = (GetViewportRect().Size - GetViewportRect().Size) / 2;
    }

    public override void _Process(double delta)
    {
        var viewport = GetViewport();
        var viewportRect = GetViewportRect();
        var mousePosition = viewport.GetMousePosition();
        var textureRectSize = new Vector2(704, 396);
        if (viewportRect.HasPoint(mousePosition))
        {
            _textureRect.Position = (mousePosition / viewportRect.Size) * (textureRectSize - viewportRect.Size) * -1;
        }
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
