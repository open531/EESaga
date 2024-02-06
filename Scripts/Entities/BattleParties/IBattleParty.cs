namespace EESaga.Scripts.Entities.BattleParties;

using Godot;
using UI;
using Utilities;

public interface IBattleParty : IBattlePiece
{
    public string PartyName { get; set; }
    public RangedInt Energy { get; set; }
    public BattleCards BattleCards { get; set; }
    public int HandCardCount { get; set; }
    public void Initialize(Player player);
    public void Initialize(IBattleParty party);
}
