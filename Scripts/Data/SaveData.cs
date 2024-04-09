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
        AttackCardNum = 6,
        BashCardNum = 0,
        DoubleBeatCardNum = 0,
        DefendCardNum = 6,
        ConsolidateCardNum = 0,
        EcsCardNum = 0,
        FuryCardNum = 0,
        USTCardNum = 0,
        ShieldStrikeCardNum = 0,
        SurviveCardNum = 0,
        StruggleCardNum = 0,
        CureCardNum = 0,
        BattleCards = new()
        {
            DeckCards = [
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CDDefend,
                CardData.CDDefend,
                CardData.CDDefend,
                CardData.CDDefend,
                CardData.CDDefend,
                ],
            HandCards = [],
            DiscardCards = []
        }
    };

    public static Array<BattleParty> Parties { get; set; } = [];
    public static int Floor { get; set; } = 0;

    public static void Save(string filename = "autosave")
    {
        var optionsFile = new ConfigFile();
        optionsFile.SetValue("Player", "AttackCardNum", Player.AttackCardNum);
        optionsFile.SetValue("Player", "BashCardNum", Player.BashCardNum);
        optionsFile.SetValue("Player", "DoubleBeatCardNum", Player.DoubleBeatCardNum);
        optionsFile.SetValue("Player", "DefendCardNum", Player.DefendCardNum);
        optionsFile.SetValue("Player", "ConsolidateCardNum", Player.ConsolidateCardNum);
        optionsFile.SetValue("Player", "EcsCardNum", Player.EcsCardNum);
        optionsFile.SetValue("Player", "FuryCardNum", Player.FuryCardNum);
        optionsFile.SetValue("Player", "USTCardNum", Player.USTCardNum);
        optionsFile.SetValue("Player", "ShieldStrikeCardNum", Player.ShieldStrikeCardNum);
        optionsFile.SetValue("Player", "SurviveCardNum", Player.SurviveCardNum);
        optionsFile.SetValue("Player", "StruggleCardNum", Player.StruggleCardNum);
        optionsFile.SetValue("Player", "CureCardNum", Player.CureCardNum);
        optionsFile.SetValue("Player", "PlayerName", Player.PlayerName);
        optionsFile.SetValue("Player", "HealthMax", Player.HealthMax);
        optionsFile.SetValue("Player", "EnergyMax", Player.EnergyMax);
        optionsFile.SetValue("Player", "Agility", Player.Agility);
        optionsFile.SetValue("Player", "Floor", Floor);
        var error = optionsFile.Save("user://" + filename + ".save");
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to save options: {error}");
        }
    }

    public static void Load(string filename = "autosave")
    {
        var optionsFile = new ConfigFile();
        var error = optionsFile.Load("user://" + filename);
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to load options: {error}");
            return;
        }
        var AttackCardNum = optionsFile.GetValue("Player", "AttackCardNum", 0).AsInt32();
        var BashCardNum = optionsFile.GetValue("Player", "BashCardNum", 0).AsInt32();
        var DoubleBeatCardNum = optionsFile.GetValue("Player", "DoubleBeatCardNum", 0).AsInt32();
        var DefendCardNum = optionsFile.GetValue("Player", "DefendCardNum", 0).AsInt32();
        var ConsolidateCardNum = optionsFile.GetValue("Player", "ConsolidateCardNum", 0).AsInt32();
        var EcsCardNum = optionsFile.GetValue("Player", "EcsCardNum", 0).AsInt32();
        var FuryCardNum = optionsFile.GetValue("Player", "FuryCardNum", 0).AsInt32();
        var USTCardNum = optionsFile.GetValue("Player", "USTCardNum", 0).AsInt32();
        var ShieldStrikeCardNum = optionsFile.GetValue("Player", "ShieldStrikeCardNum", 0).AsInt32();
        var SurviveCardNum = optionsFile.GetValue("Player", "SurviveCardNum", 0).AsInt32();
        var StruggleCardNum = optionsFile.GetValue("Player", "StruggleCardNum", 0).AsInt32();
        var CureCardNum = optionsFile.GetValue("Player", "CureCardNum", 0).AsInt32();
        Floor = optionsFile.GetValue("Player", "Floor", 0).AsInt32();
        Player.PlayerName = optionsFile.GetValue("Player", "PlayerName", "").ToString();
        Player.HealthMax = optionsFile.GetValue("Player", "HealthMax", 100).AsInt32();
        Player.EnergyMax = optionsFile.GetValue("Player", "EnergyMax", 4).AsInt32();
        Player.Agility = optionsFile.GetValue("Player", "Agility", 10).AsInt32();
        Player.BattleCards.DeckCards = [];
        Player.AddBattleCards(CardData.CAStrike, AttackCardNum);
        Player.AddBattleCards(CardData.CABash, BashCardNum);
        Player.AddBattleCards(CardData.CADoubleBeat, DoubleBeatCardNum);
        Player.AddBattleCards(CardData.CDDefend, DefendCardNum);
        Player.AddBattleCards(CardData.CDConsolidate, ConsolidateCardNum);
        Player.AddBattleCards(CardData.CIECS, EcsCardNum);
        Player.AddBattleCards(CardData.CSFury, FuryCardNum);
        Player.AddBattleCards(CardData.CIUST, USTCardNum);
        Player.AddBattleCards(CardData.CSShieldStrike, ShieldStrikeCardNum);
        Player.AddBattleCards(CardData.CSSurvive, SurviveCardNum);
        Player.AddBattleCards(CardData.CSStruggle, StruggleCardNum);
        Player.AddBattleCards(CardData.CSCure, CureCardNum);
    }
}
