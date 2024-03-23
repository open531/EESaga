namespace EESaga.Scripts.Data;

using Entities;
using Entities.BattleParties;
using Godot;
using System.Collections.Generic;

public partial class SaveData : Node
{
    public static Player Player { get; set; } = new()
    {
        PlayerName = "MAX",
        HealthMax = 100,
        Health = 100,
        EnergyMax = 4,
        Energy = 4,
        Agility = 10,
    };
    public static List<BattleParty> Parties { get; set; }
    public int Floor { get; set; }

    public void Save(string filename)
    {
        var saveData = new ConfigFile();
        var error = saveData.Save("user://" + filename + ".save");
        if (error != Error.Ok)
        {
            GD.PrintErr("Error saving data to file.");
        }
    }
}
