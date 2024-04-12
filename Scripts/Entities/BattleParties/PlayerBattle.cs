namespace EESaga.Scripts.Entities.BattleParties;

using Data;
using EESaga.Scripts.Cards;
using Godot;

public partial class PlayerBattle : BattleParty
{
    public static new PlayerBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/player_battle.tscn").Instantiate<PlayerBattle>();

    public override void _Ready()
    {
        base._Ready();
        PartyName = SaveData.Player.PlayerName;
        PieceName = SaveData.Player.PlayerName;
        MoveRange = 2;
        HandCardCount = 4;
        Health = SaveData.Player.Health;
        HealthMax = SaveData.Player.HealthMax;
        Energy = SaveData.Player.Energy;
        EnergyMax = SaveData.Player.EnergyMax;
        Agility = SaveData.Player.Agility;
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
