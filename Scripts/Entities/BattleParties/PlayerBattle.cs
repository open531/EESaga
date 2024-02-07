namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public partial class PlayerBattle : BattleParty
{
    public static new string PartyName => GameData.Player.PlayerName;
}
