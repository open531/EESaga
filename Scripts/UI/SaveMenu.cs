namespace EESaga.Scripts.UI;

using System;
using Data;
using Godot;

public partial class SaveMenu : PopupPanel
{
    private LineEdit _saveName;
    private Button _saveButton;
    private VBoxContainer _vBoxContainer;
    public static SaveMenu Instance() => GD.Load<PackedScene>("res://Scenes/UI/save_menu.tscn").Instantiate<SaveMenu>();

    public override void _Ready()
    {
        _saveName = GetNode<LineEdit>("%SaveName");
        _saveButton = GetNode<Button>("%SaveButton");
        _vBoxContainer = GetNode<VBoxContainer>("%VBoxContainer");
        _saveButton.Pressed += OnSaveButtonPressed;
    }

    public void UpdateSaveData()
    {
        _saveName.Text = SaveData.Player.PlayerName + DateTime.Now.ToString("yyMMddHHmmss");
        ClearSaveData();
        using var dir = DirAccess.Open("user://");
        if (dir != null)
        {
            dir.ListDirBegin();
            var fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && fileName.Contains(".save"))
                {
                    var button = new Button
                    {
                        Text = fileName,
                    };
                    button.Pressed += () => SaveData.Load(button.Text);
                    _vBoxContainer.AddChild(button);
                }
                fileName = dir.GetNext();
            }
        }
        GD.Print("SaveMenu.UpdateSaveData");
    }

    private void ClearSaveData()
    {
        foreach (Node child in _vBoxContainer.GetChildren())
        {
            if (child is Button button)
            {
                if (button.Text.Contains(".save"))
                    button.QueueFree();
            }
        }
    }

    private void OnSaveButtonPressed()
    {
        if (string.IsNullOrWhiteSpace(_saveName.Text)) return;
        SaveData.Save(_saveName.Text);
        UpdateSaveData();
    }
}
