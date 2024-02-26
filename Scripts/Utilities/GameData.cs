namespace EESaga.Scripts.Utilities;

using Entities;
using Entities.BattleParties;
using Godot;
using System.Collections.Generic;

public partial class GameData : Node
{
    public static Player Player { get; set; } = new Player()
    {
        PlayerName = "MAX",
        HealthMax = 100,
        Health = 100,
    };
    public static List<IBattleParty> Parties { get; set; }
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
