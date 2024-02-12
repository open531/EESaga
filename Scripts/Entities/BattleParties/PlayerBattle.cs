namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public partial class PlayerBattle : BattleParty
{
    public override string PartyName { get; set; } = GameData.Player.PlayerName;
    public override int MoveRange { get; set; } = 3;
    public override int HandCardCount { get; set; } = 4;
    public static new PlayerBattle Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/player_battle.tscn").Instantiate<PlayerBattle>();
}
