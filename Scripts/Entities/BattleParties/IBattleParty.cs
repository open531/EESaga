namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public interface IBattleParty : IBattlePiece
{
    public string PartyName { get; }
    public int EnergyMax { get; set; }
    public int Energy { get; set; }
    public BattleCards BattleCards { get; set; }
    public int HandCardCount { get; set; }
    public void Initialize(Player player);
    public void Initialize(IBattleParty party);
}

public enum PartyType
{
    Null,
    Player,
}
