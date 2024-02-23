namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class NameInputPanel : Panel
{
    private Label _table;
    private LineEdit _nameInput;
    private Button _submitButton;

    public override void _Ready()
    {
        _table = GetNode<Label>("%Table");
        _nameInput = GetNode<LineEdit>("%NameInput");
        _submitButton = GetNode<Button>("%SubmitButton");

        _nameInput.GrabFocus();
        _submitButton.Pressed += OnSubmitButtonPressed;
    }

    public void OnSubmitButtonPressed()
    {
        GameData.Player.PlayerName = _nameInput.Text;
    }
}
