namespace EESaga.Scripts.UI;

using Data;
using EESaga.Scripts.Utilities;
using Godot;

public partial class LoadMenu : PopupPanel
{
    private SceneSwitcher _sceneSwitcher;
    private VBoxContainer _vBoxContainer;
    public static LoadMenu Instance() => GD.Load<PackedScene>("res://Scenes/UI/load_menu.tscn").Instantiate<LoadMenu>();

    public override void _Ready()
    {
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        _vBoxContainer = GetNode<VBoxContainer>("%VBoxContainer");
        UpdateSaveData();
    }
    public void UpdateSaveData()
    {
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
                    button.Pressed += () =>
                    {
                        SaveData.Load(button.Text);
                        _sceneSwitcher.PushScene(SceneSwitcher.BattleManagerScene);
                    };
                    _vBoxContainer.AddChild(button);
                }
                fileName = dir.GetNext();
            }
        }
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
}
