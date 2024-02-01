namespace EESaga.Scripts.Managers;

using Godot;
using Maps;
using System.Collections.Generic;
using UI;

public partial class BattleManager : Node
{
    private static readonly PackedScene _pieceBattleScene = ResourceLoader.Load<PackedScene>("res://Scenes/Maps/PieceBattle.tscn");
    private static readonly PackedScene _cardBattleScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/CardBattle.tscn");
    public PieceBattle PieceBattle { get; set; }
    public CardBattle CardBattle { get; set; }
    public List<BattleCards> PartyBattleCards { get; set; }
    public void PrepareCards(int partyIndex, int cardCount)
    {
        if (PartyBattleCards[partyIndex].DeckCards.Count < cardCount)
        {
            foreach (var card in PartyBattleCards[partyIndex].DiscardCards)
            {
                PartyBattleCards[partyIndex].DeckCards.Add(card);
            }
            PartyBattleCards[partyIndex].DiscardCards.Clear();
        }
        var rng = new RandomNumberGenerator();
        rng.Randomize();
        for (var i = 0; i < cardCount; i++)
        {
            var randomIndex = rng.RandiRange(0, PartyBattleCards[partyIndex].DeckCards.Count - 1);
            PartyBattleCards[partyIndex].HandCards.Add(PartyBattleCards[partyIndex].DeckCards[randomIndex]);
            PartyBattleCards[partyIndex].DeckCards.RemoveAt(randomIndex);
        }
        CardBattle.BattleCards = PartyBattleCards[partyIndex];
    }
}
