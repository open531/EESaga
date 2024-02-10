namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public partial class BattleParty : BattlePiece, IBattleParty
{
    public string PartyName { get; private set; }
    public RangedInt Energy { get; set; }
    public BattleCards BattleCards { get; set; }
    public int HandCardCount { get; set; }

    public static new BattleParty Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/battle_party.tscn").Instantiate<BattleParty>();

    public void Initialize(Player player)
    {
        PartyName = player.PlayerName;
        Level = player.Level;
        Health = player.Health;
        Shield = player.Shield;
        Agility = player.Agility;
        Energy = player.Energy;
        BattleCards = player.BattleCards;
    }

    public void Initialize(IBattleParty party)
    {
        PartyName = party.PartyName;
        Level = party.Level;
        Health = party.Health;
        Shield = party.Shield;
        Agility = party.Agility;
        Energy = party.Energy;
        BattleCards = party.BattleCards;
    }
}
