using EESaga.Scripts.Data;
using EESaga.Scripts.Entities;
using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.UI;
using Godot;
using System;

public partial class HardwareWarriorBattle : BattleParty
{
    public static new HardwareWarriorBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/hardware_warrior_battle.tscn").Instantiate<HardwareWarriorBattle>();

    public override void _Ready()
    {
        base._Ready();
        PartyName = SaveData.HardwareWarrior.PlayerName;
        PieceName = SaveData.HardwareWarrior.PlayerName;
        MoveRange = 2;
        HandCardCount = 4;
        Health = SaveData.HardwareWarrior.Health;
        HealthMax = SaveData.HardwareWarrior.HealthMax;
        Energy = SaveData.HardwareWarrior.Energy;
        EnergyMax = SaveData.HardwareWarrior.EnergyMax;
        Agility = SaveData.HardwareWarrior.Agility;
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
