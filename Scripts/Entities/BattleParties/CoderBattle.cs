using EESaga.Scripts.Data;
using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.Entities;
using EESaga.Scripts.UI;
using Godot;
using System;

public partial class CoderBattle : BattleParty
{
    public static new CoderBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/coder_battle.tscn").Instantiate<CoderBattle>();

    public override void _Ready()
    {
        base._Ready();
        PartyName = SaveData.Coder.PlayerName;
        PieceName = SaveData.Coder.PlayerName;
        MoveRange = 2;
        HandCardCount = 4;
        Health = SaveData.Coder.Health;
        HealthMax = SaveData.Coder.HealthMax;
        Energy = SaveData.Coder.Energy;
        EnergyMax = SaveData.Coder.EnergyMax;
        Agility = SaveData.Coder.Agility;
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
