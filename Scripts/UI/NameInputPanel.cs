namespace EESaga.Scripts.UI;

using Data;
using EESaga.Scripts.Utilities;
using Godot;

public partial class NameInputPanel : Panel
{
    private Label _table;
    private LineEdit _nameInput;
    private Button _submitButton;
    private SceneSwitcher _sceneSwitcher;

    public static NameInputPanel Instance() => GD.Load<PackedScene>("res://Scenes/UI/name_input_panel.tscn").Instantiate<NameInputPanel>();

    public override void _Ready()
    {
        _table = GetNode<Label>("%Table");
        _nameInput = GetNode<LineEdit>("%NameInput");
        _submitButton = GetNode<Button>("%SubmitButton");
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

        _nameInput.GrabFocus();
        _submitButton.Pressed += OnSubmitButtonPressed;
    }

    public void OnSubmitButtonPressed()
    {
        SaveData.Player.PlayerName = _nameInput.Text;
        _sceneSwitcher.PushScene(SceneSwitcher.BattleManagerScene);
    }
}
