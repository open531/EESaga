using EESaga.Scripts.Data;
using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.Entities;
using EESaga.Scripts.UI;
using Godot;
using System;

public partial class SignalMasterBattle : BattleParty
{
    public static new SignalMasterBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/signal_master_battle.tscn").Instantiate<SignalMasterBattle>();

    public override void _Ready()
    {
        base._Ready();
        PartyName = SaveData.SignalMaster.PlayerName;
        PieceName = SaveData.SignalMaster.PlayerName;
        MoveRange = 2;
        HandCardCount = 4;
        Health = SaveData.SignalMaster.Health;
        HealthMax = SaveData.SignalMaster.HealthMax;
        Energy = SaveData.SignalMaster.Energy;
        EnergyMax = SaveData.SignalMaster.EnergyMax;
        Agility = SaveData.SignalMaster.Agility;
    }

    public void Initialize(Player player)
    {
        PartyName = player.PlayerName;
        HealthMax = player.HealthMax;
        Health = player.Health;
        EnergyMax = player.EnergyMax;
        Energy = player.Energy;
        Agility = player.Agility;
        BattleCards = player.BattleCards;
    }
}
