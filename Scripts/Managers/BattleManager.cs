namespace EESaga.Scripts.Managers;

using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Maps;
using System.Collections.Generic;
using UI;

public partial class BattleManager : Node
{
    public BattleState BattleState
    {
        get
        {
            if (SelectedTile != null)
            {
                return BattleState.Moving;
            }
            else if (SelectedCard != null)
            {
                return BattleState.CardTargeting;
            }
            else
            {
                return BattleState.CardSelecting;
            }
        }
    }
    public PieceBattle PieceBattle { get; set; }
    public CardBattle CardBattle { get; set; }
    public List<IBattlePiece> Pieces => PieceBattle.Pieces;
    public List<IBattleParty> Parties => PieceBattle.Parties;
    public List<IBattleEnemy> Enemies => PieceBattle.Enemies;
    public List<BattleCards> PartyBattleCards { get; set; }
    public Vector2I? SelectedTile => PieceBattle.TileMap.SelectedTile;
    public Card SelectedCard => CardBattle.SelectedCard;
    public IBattlePiece CardTarget { get; set; }

    public static BattleManager Instance() => GD.Load<PackedScene>("res://Scenes/Managers/battle_manager.tscn").Instantiate<BattleManager>();

    public override void _Ready()
    {
        PieceBattle = GetNode<PieceBattle>("PieceBattle");
        CardBattle = GetNode<CardBattle>("CardBattle");
    }

    public void Initialize(Room room)
    {
        PieceBattle.Initialize(room.TileMap);
    }

    public void TurnTo(IBattlePiece battlePiece)
    {
        battlePiece.Shield = 0;
        if (battlePiece is BattleParty battleParty)
        {
            var partyIndex = Parties.IndexOf(battleParty);
            PrepareCards(partyIndex, battleParty.HandCardCount);
        }
        else if (battlePiece is BattleEnemy battleEnemy)
        {
        }
    }

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

public enum BattleState
{
    None,
    Moving,
    CardSelecting,
    CardTargeting,
}