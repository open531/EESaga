namespace EESaga.Scripts.Data;

using Cards;
using Entities;
using Entities.BattleParties;
using Godot;
using Godot.Collections;
using Managers;

public partial class SaveData : Node
{
    public static Player Player { get; set; } = new()
    {
        PlayerName = "Yang Zhang",
        HealthMax = 100,
        Health = 100,
        EnergyMax = 4,
        Energy = 4,
        Agility = 10,
        BattleCards = new()
        {
            DeckCards =
            [
                CardData.CDDefend,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CAStrike,
            ],
            HandCards = [],
            DiscardCards =[]
        }
    };

    public static Array<BattleParty> Parties { get; set; } = [];
    public static int Floor { get; set; } = 0;

    public static void Save(string filename)
    {
        var saveGame = FileAccess.Open($"user://{filename}.save", FileAccess.ModeFlags.Write);
        var saveData = new Dictionary<string, Variant>
        {
            { "Player", Player },
            { "Parties", Parties },
            { "Floor", Floor },
        };
        var jsonString = Json.Stringify(saveData);
        GD.Print(saveGame);
        GD.Print(jsonString);
        saveGame.StoreLine(jsonString);
    }

    public static void Load(string filename)
    {
        if (!FileAccess.FileExists($"user://{filename}.save")) return;
        using var saveGame = FileAccess.Open($"user://{filename}.save", FileAccess.ModeFlags.Read);
        var jsonString = saveGame.GetLine();
        var json = new Json();
        var parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
            return;
        }
        var saveData = new Dictionary<string, Variant>((Dictionary)json.Data);
        Player = (Player)saveData["Player"];
        Parties = (Array<BattleParty>)saveData["Parties"];
        Floor = (int)saveData["Floor"];
    }
}
