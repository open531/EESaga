namespace EESaga.Scripts.Managers;

using Cards;
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
            if (SelectedCell != null)
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
    public List<BattlePiece> Pieces => PieceBattle.Pieces;
    public List<BattleParty> Parties => PieceBattle.Parties;
    public List<BattleEnemy> Enemies => PieceBattle.Enemies;
    public BattlePiece CurrentPiece
    {
        get => PieceBattle.CurrentPiece;
        set => PieceBattle.CurrentPiece = value;
    }
    public Vector2I? SelectedCell => PieceBattle.SelectedCell;
    public Card SelectedCard => CardBattle.SelectedCard;
    public BattlePiece CardTarget { get; set; }

    public static BattleManager Instance() => GD.Load<PackedScene>("res://Scenes/Managers/battle_manager.tscn").Instantiate<BattleManager>();

    public override void _Ready()
    {
        PieceBattle = GetNode<PieceBattle>("PieceBattle");
        CardBattle = GetNode<CardBattle>("CardBattle");

        CardBattle.OperatingCardChanged += OnCardBattleOperatingCardChanged;

        TurnTo(Parties[0]);
    }


    public void Initialize(Room room)
    {
        PieceBattle.Initialize(room.TileMap);
    }

    public void TurnTo(BattlePiece battlePiece)
    {
        battlePiece.Shield = 0;
        if (battlePiece is BattleParty battleParty)
        {
            CurrentPiece = battleParty;
            PieceBattle.ShowAccessibleTiles(battleParty.MoveRange);
            GD.Print(battleParty.MoveRange);
            PrepareCards();
        }
        else if (battlePiece is BattleEnemy battleEnemy)
        {
        }
    }

    public void PrepareCards()
    {
        if (CurrentPiece is BattleParty battleParty)
        {
            if (battleParty.BattleCards.DeckCards.Count < battleParty.HandCardCount)
            {
                foreach (var card in battleParty.BattleCards.DiscardCards)
                {
                    battleParty.BattleCards.DeckCards.Add(card);
                }
                battleParty.BattleCards.DiscardCards.Clear();
            }
            var rng = new RandomNumberGenerator();
            rng.Randomize();
            for (var i = 0; i < battleParty.HandCardCount; i++)
            {
                var randomIndex = rng.RandiRange(0, battleParty.BattleCards.DeckCards.Count - 1);
                battleParty.BattleCards.HandCards.Add(battleParty.BattleCards.DeckCards[randomIndex]);
                battleParty.BattleCards.DeckCards.RemoveAt(randomIndex);
            }
            CardBattle.BattleCards = battleParty.BattleCards;
        }
    }

    private void OnCardBattleOperatingCardChanged()
    {
        if (CardBattle.OperatingCard == null)
        {
            PieceBattle.RecoverAttackTiles();
        }
        else
        {
            PieceBattle.ShowAttackTiles(CardBattle.OperatingCard.CardRange, CardBattle.OperatingCard.CardTarget);
        }
    }
}

public enum BattleState
{
    None,
    Moving,
    CardSelecting,
    CardTargeting,
}