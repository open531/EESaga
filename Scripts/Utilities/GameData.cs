namespace EESaga.Scripts.Utilities;

using Entities;
using Godot;

public partial class GameData : Node
{
    public Player Player { get; set; }
    public void SaveData(string filename)
    {
        var saveData = new ConfigFile();
        var error = saveData.Save("user://" + filename + ".save");
        if (error != Error.Ok)
        {
            GD.PrintErr("Error saving data to file.");
        }
    }
}
