namespace EESaga.Scripts.Utilities;

using Entities;
using Entities.BattleParties;
using Godot;
using System.Collections.Generic;

public partial class GameData : Node
{
    public static Player Player { get; set; } = new()
    {
        PlayerName = "MAX",
        HealthMax = 100,
        Health = 100,
        EnergyMax = 3,
        Energy = 3,
        Agility = 10,
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
