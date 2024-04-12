namespace EESaga.Scripts.Data;

using Cards;
using Entities;
using Entities.BattleParties;
using Godot;
using Godot.Collections;
using Managers;

public partial class SaveData : Node
{
    #region Player
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
        Dancing = true,
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
    #endregion

    #region Coder
    public static Player Coder { get; set; } = new()
    {
        PlayerName = "The Coder",
        HealthMax = 70,
        Health = 70,
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
        Dancing = true,
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
    #endregion  

    #region Hardware Warrior
    public static Player HardwareWarrior { get; set; } = new()
    {
        PlayerName = "Hardware Warrior",
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
        Dancing = true,
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
    #endregion

    #region Signal Master
    public static Player SignalMaster { get; set; } = new()
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
        Dancing = true,
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
    #endregion
    public static Array<BattleParty> Parties { get; set; } = [];
    public static int Floor { get; set; } = 0;

    public static void Save(string filename = "autosave")
    {
        var optionsFile = new ConfigFile();
        #region Player
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
        optionsFile.SetValue("Player", "Dancing", Player.Dancing);
        #endregion

        #region Coder
        optionsFile.SetValue("Coder", "AttackCardNum", Coder.AttackCardNum);
        optionsFile.SetValue("Coder", "BashCardNum", Coder.BashCardNum);
        optionsFile.SetValue("Coder", "DoubleBeatCardNum", Coder.DoubleBeatCardNum);
        optionsFile.SetValue("Coder", "DefendCardNum", Coder.DefendCardNum);
        optionsFile.SetValue("Coder", "ConsolidateCardNum", Coder.ConsolidateCardNum);
        optionsFile.SetValue("Coder", "EcsCardNum", Coder.EcsCardNum);
        optionsFile.SetValue("Coder", "FuryCardNum", Coder.FuryCardNum);
        optionsFile.SetValue("Coder", "USTCardNum", Coder.USTCardNum);
        optionsFile.SetValue("Coder", "ShieldStrikeCardNum", Coder.ShieldStrikeCardNum);
        optionsFile.SetValue("Coder", "SurviveCardNum", Coder.SurviveCardNum);
        optionsFile.SetValue("Coder", "StruggleCardNum", Coder.StruggleCardNum);
        optionsFile.SetValue("Coder", "CureCardNum", Coder.CureCardNum);
        optionsFile.SetValue("Coder", "PlayerName", Coder.PlayerName);
        optionsFile.SetValue("Coder", "HealthMax", Coder.HealthMax);
        optionsFile.SetValue("Coder", "EnergyMax", Coder.EnergyMax);
        optionsFile.SetValue("Coder", "Agility", Coder.Agility);
        optionsFile.SetValue("Coder", "Floor", Floor);
        optionsFile.SetValue("Coder", "Dancing", Coder.Dancing);
        #endregion

        #region Hardware Warrior
        optionsFile.SetValue("HardwareWarrior", "AttackCardNum", HardwareWarrior.AttackCardNum);
        optionsFile.SetValue("HardwareWarrior", "BashCardNum", HardwareWarrior.BashCardNum);
        optionsFile.SetValue("HardwareWarrior", "DoubleBeatCardNum", HardwareWarrior.DoubleBeatCardNum);
        optionsFile.SetValue("HardwareWarrior", "DefendCardNum", HardwareWarrior.DefendCardNum);
        optionsFile.SetValue("HardwareWarrior", "ConsolidateCardNum", HardwareWarrior.ConsolidateCardNum);
        optionsFile.SetValue("HardwareWarrior", "EcsCardNum", HardwareWarrior.EcsCardNum);
        optionsFile.SetValue("HardwareWarrior", "FuryCardNum", HardwareWarrior.FuryCardNum);
        optionsFile.SetValue("HardwareWarrior", "USTCardNum", HardwareWarrior.USTCardNum);
        optionsFile.SetValue("HardwareWarrior", "ShieldStrikeCardNum", HardwareWarrior.ShieldStrikeCardNum);
        optionsFile.SetValue("HardwareWarrior", "SurviveCardNum", HardwareWarrior.SurviveCardNum);
        optionsFile.SetValue("HardwareWarrior", "StruggleCardNum", HardwareWarrior.StruggleCardNum);
        optionsFile.SetValue("HardwareWarrior", "CureCardNum", HardwareWarrior.CureCardNum);
        optionsFile.SetValue("HardwareWarrior", "PlayerName", HardwareWarrior.PlayerName);
        optionsFile.SetValue("HardwareWarrior", "HealthMax", HardwareWarrior.HealthMax);
        optionsFile.SetValue("HardwareWarrior", "EnergyMax", HardwareWarrior.EnergyMax);
        optionsFile.SetValue("HardwareWarrior", "Agility", HardwareWarrior.Agility);
        optionsFile.SetValue("HardwareWarrior", "Floor", Floor);
        optionsFile.SetValue("HardwareWarrior", "Dancing", HardwareWarrior.Dancing);
        #endregion

        #region Signal Master
        optionsFile.SetValue("SignalMaster", "AttackCardNum", SignalMaster.AttackCardNum);
        optionsFile.SetValue("SignalMaster", "BashCardNum", SignalMaster.BashCardNum);
        optionsFile.SetValue("SignalMaster", "DoubleBeatCardNum", SignalMaster.DoubleBeatCardNum);
        optionsFile.SetValue("SignalMaster", "DefendCardNum", SignalMaster.DefendCardNum);
        optionsFile.SetValue("SignalMaster", "ConsolidateCardNum", SignalMaster.ConsolidateCardNum);
        optionsFile.SetValue("SignalMaster", "EcsCardNum", SignalMaster.EcsCardNum);
        optionsFile.SetValue("SignalMaster", "FuryCardNum", SignalMaster.FuryCardNum);
        optionsFile.SetValue("SignalMaster", "USTCardNum", SignalMaster.USTCardNum);
        optionsFile.SetValue("SignalMaster", "ShieldStrikeCardNum", SignalMaster.ShieldStrikeCardNum);
        optionsFile.SetValue("SignalMaster", "SurviveCardNum", SignalMaster.SurviveCardNum);
        optionsFile.SetValue("SignalMaster", "StruggleCardNum", SignalMaster.StruggleCardNum);
        optionsFile.SetValue("SignalMaster", "CureCardNum", SignalMaster.CureCardNum);
        optionsFile.SetValue("SignalMaster", "PlayerName", SignalMaster.PlayerName);
        optionsFile.SetValue("SignalMaster", "HealthMax", SignalMaster.HealthMax);
        optionsFile.SetValue("SignalMaster", "EnergyMax", SignalMaster.EnergyMax);
        optionsFile.SetValue("SignalMaster", "Agility", SignalMaster.Agility);
        optionsFile.SetValue("SignalMaster", "Floor", Floor);
        optionsFile.SetValue("SignalMaster", "Dancing", SignalMaster.Dancing);
        #endregion
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
        # region Player
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
        Player.Dancing = optionsFile.GetValue("Player", "Dancing", false).AsBool();
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
        #endregion

        #region Coder
        AttackCardNum = optionsFile.GetValue("Coder", "AttackCardNum", 0).AsInt32();
        BashCardNum = optionsFile.GetValue("Coder", "BashCardNum", 0).AsInt32();
        DoubleBeatCardNum = optionsFile.GetValue("Coder", "DoubleBeatCardNum", 0).AsInt32();
        DefendCardNum = optionsFile.GetValue("Coder", "DefendCardNum", 0).AsInt32();
        ConsolidateCardNum = optionsFile.GetValue("Coder", "ConsolidateCardNum", 0).AsInt32();
        EcsCardNum = optionsFile.GetValue("Coder", "EcsCardNum", 0).AsInt32();
        FuryCardNum = optionsFile.GetValue("Coder", "FuryCardNum", 0).AsInt32();
        USTCardNum = optionsFile.GetValue("Coder", "USTCardNum", 0).AsInt32();
        ShieldStrikeCardNum = optionsFile.GetValue("Coder", "ShieldStrikeCardNum", 0).AsInt32();
        SurviveCardNum = optionsFile.GetValue("Coder", "SurviveCardNum", 0).AsInt32();
        StruggleCardNum = optionsFile.GetValue("Coder", "StruggleCardNum", 0).AsInt32();
        CureCardNum = optionsFile.GetValue("Coder", "CureCardNum", 0).AsInt32();
        Floor = optionsFile.GetValue("Coder", "Floor", 0).AsInt32();
        Coder.PlayerName = optionsFile.GetValue("Coder", "PlayerName", "").ToString();
        Coder.HealthMax = optionsFile.GetValue("Coder", "HealthMax", 100).AsInt32();
        Coder.EnergyMax = optionsFile.GetValue("Coder", "EnergyMax", 4).AsInt32();
        Coder.Agility = optionsFile.GetValue("Coder", "Agility", 10).AsInt32();
        Coder.Dancing = optionsFile.GetValue("Coder", "Dancing", false).AsBool();
        Coder.BattleCards.DeckCards = [];
        Coder.AddBattleCards(CardData.CAStrike, AttackCardNum);
        Coder.AddBattleCards(CardData.CABash, BashCardNum);
        Coder.AddBattleCards(CardData.CADoubleBeat, DoubleBeatCardNum);
        Coder.AddBattleCards(CardData.CDDefend, DefendCardNum);
        Coder.AddBattleCards(CardData.CDConsolidate, ConsolidateCardNum);
        Coder.AddBattleCards(CardData.CIECS, EcsCardNum);
        Coder.AddBattleCards(CardData.CSFury, FuryCardNum);
        Coder.AddBattleCards(CardData.CIUST, USTCardNum);
        Coder.AddBattleCards(CardData.CSShieldStrike, ShieldStrikeCardNum);
        Coder.AddBattleCards(CardData.CSSurvive, SurviveCardNum);
        Coder.AddBattleCards(CardData.CSStruggle, StruggleCardNum);
        Coder.AddBattleCards(CardData.CSCure, CureCardNum);
        #endregion

        #region Hardware Warrior
        AttackCardNum = optionsFile.GetValue("HardwareWarrior", "AttackCardNum", 0).AsInt32();
        BashCardNum = optionsFile.GetValue("HardwareWarrior", "BashCardNum", 0).AsInt32();
        DoubleBeatCardNum = optionsFile.GetValue("HardwareWarrior", "DoubleBeatCardNum", 0).AsInt32();
        DefendCardNum = optionsFile.GetValue("HardwareWarrior", "DefendCardNum", 0).AsInt32();
        ConsolidateCardNum = optionsFile.GetValue("HardwareWarrior", "ConsolidateCardNum", 0).AsInt32();
        EcsCardNum = optionsFile.GetValue("HardwareWarrior", "EcsCardNum", 0).AsInt32();
        FuryCardNum = optionsFile.GetValue("HardwareWarrior", "FuryCardNum", 0).AsInt32();
        USTCardNum = optionsFile.GetValue("HardwareWarrior", "USTCardNum", 0).AsInt32();
        ShieldStrikeCardNum = optionsFile.GetValue("HardwareWarrior", "ShieldStrikeCardNum", 0).AsInt32();
        SurviveCardNum = optionsFile.GetValue("HardwareWarrior", "SurviveCardNum", 0).AsInt32();
        StruggleCardNum = optionsFile.GetValue("HardwareWarrior", "StruggleCardNum", 0).AsInt32();
        CureCardNum = optionsFile.GetValue("HardwareWarrior", "CureCardNum", 0).AsInt32();
        Floor = optionsFile.GetValue("HardwareWarrior", "Floor", 0).AsInt32();
        HardwareWarrior.PlayerName = optionsFile.GetValue("HardwareWarrior", "PlayerName", "").ToString();
        HardwareWarrior.HealthMax = optionsFile.GetValue("HardwareWarrior", "HealthMax", 100).AsInt32();
        HardwareWarrior.EnergyMax = optionsFile.GetValue("HardwareWarrior", "EnergyMax", 4).AsInt32();
        HardwareWarrior.Agility = optionsFile.GetValue("HardwareWarrior", "Agility", 10).AsInt32();
        HardwareWarrior.Dancing = optionsFile.GetValue("HardwareWarrior", "Dancing", false).AsBool();
        HardwareWarrior.BattleCards.DeckCards = [];
        HardwareWarrior.AddBattleCards(CardData.CAStrike, AttackCardNum);
        HardwareWarrior.AddBattleCards(CardData.CABash, BashCardNum);
        HardwareWarrior.AddBattleCards(CardData.CADoubleBeat, DoubleBeatCardNum);
        HardwareWarrior.AddBattleCards(CardData.CDDefend, DefendCardNum);
        HardwareWarrior.AddBattleCards(CardData.CDConsolidate, ConsolidateCardNum);
        HardwareWarrior.AddBattleCards(CardData.CIECS, EcsCardNum);
        HardwareWarrior.AddBattleCards(CardData.CSFury, FuryCardNum);
        HardwareWarrior.AddBattleCards(CardData.CIUST, USTCardNum);
        HardwareWarrior.AddBattleCards(CardData.CSShieldStrike, ShieldStrikeCardNum);
        HardwareWarrior.AddBattleCards(CardData.CSSurvive, SurviveCardNum);
        HardwareWarrior.AddBattleCards(CardData.CSStruggle, StruggleCardNum);
        HardwareWarrior.AddBattleCards(CardData.CSCure, CureCardNum);
        #endregion

        #region Signal Master
        AttackCardNum = optionsFile.GetValue("SignalMaster", "AttackCardNum", 0).AsInt32();
        BashCardNum = optionsFile.GetValue("SignalMaster", "BashCardNum", 0).AsInt32();
        DoubleBeatCardNum = optionsFile.GetValue("SignalMaster", "DoubleBeatCardNum", 0).AsInt32();
        DefendCardNum = optionsFile.GetValue("SignalMaster", "DefendCardNum", 0).AsInt32();
        ConsolidateCardNum = optionsFile.GetValue("SignalMaster", "ConsolidateCardNum", 0).AsInt32();
        EcsCardNum = optionsFile.GetValue("SignalMaster", "EcsCardNum", 0).AsInt32();
        FuryCardNum = optionsFile.GetValue("SignalMaster", "FuryCardNum", 0).AsInt32();
        USTCardNum = optionsFile.GetValue("SignalMaster", "USTCardNum", 0).AsInt32();
        ShieldStrikeCardNum = optionsFile.GetValue("SignalMaster", "ShieldStrikeCardNum", 0).AsInt32();
        SurviveCardNum = optionsFile.GetValue("SignalMaster", "SurviveCardNum", 0).AsInt32();
        StruggleCardNum = optionsFile.GetValue("SignalMaster", "StruggleCardNum", 0).AsInt32();
        CureCardNum = optionsFile.GetValue("SignalMaster", "CureCardNum", 0).AsInt32();
        Floor = optionsFile.GetValue("SignalMaster", "Floor", 0).AsInt32();
        SignalMaster.PlayerName = optionsFile.GetValue("SignalMaster", "PlayerName", "").ToString();
        SignalMaster.HealthMax = optionsFile.GetValue("SignalMaster", "HealthMax", 100).AsInt32();
        SignalMaster.EnergyMax = optionsFile.GetValue("SignalMaster", "EnergyMax", 4).AsInt32();
        SignalMaster.Agility = optionsFile.GetValue("SignalMaster", "Agility", 10).AsInt32();
        SignalMaster.Dancing = optionsFile.GetValue("SignalMaster", "Dancing", false).AsBool();
        SignalMaster.BattleCards.DeckCards = [];
        SignalMaster.AddBattleCards(CardData.CAStrike, AttackCardNum);
        SignalMaster.AddBattleCards(CardData.CABash, BashCardNum);
        SignalMaster.AddBattleCards(CardData.CADoubleBeat, DoubleBeatCardNum);
        SignalMaster.AddBattleCards(CardData.CDDefend, DefendCardNum);
        SignalMaster.AddBattleCards(CardData.CDConsolidate, ConsolidateCardNum);
        SignalMaster.AddBattleCards(CardData.CIECS, EcsCardNum);
        SignalMaster.AddBattleCards(CardData.CSFury, FuryCardNum);
        SignalMaster.AddBattleCards(CardData.CIUST, USTCardNum);
        SignalMaster.AddBattleCards(CardData.CSShieldStrike, ShieldStrikeCardNum);
        SignalMaster.AddBattleCards(CardData.CSSurvive, SurviveCardNum);
        SignalMaster.AddBattleCards(CardData.CSStruggle, StruggleCardNum);
        SignalMaster.AddBattleCards(CardData.CSCure, CureCardNum);
        #endregion
    }
}
