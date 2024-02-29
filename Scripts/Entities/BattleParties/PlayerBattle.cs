namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public partial class PlayerBattle : BattleParty
{
    public static new PlayerBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/player_battle.tscn").Instantiate<PlayerBattle>();

    public override void _Ready()
    {
        base._Ready();
        PartyName = GameData.Player.PlayerName;
        PieceName = GameData.Player.PlayerName;
        MoveRange = 3;
        HandCardCount = 4;
        Health = GameData.Player.Health;
        HealthMax = GameData.Player.HealthMax;
    }
}
