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
    public Vector2I? SelectedCell => PieceBattle.TileMap.SelectedCell;
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

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.Pressed)
                {
                    var cell = PieceBattle.TileMap.SelectedCell;
                    GD.Print(cell);
                    var card = CardBattle.OperatingCard;
                    if (cell != null &&
                        PieceBattle.TileMap.IsDestination(cell.Value) &&
                        !PieceBattle.CurrentPiece.IsMoving)
                    {
                        PieceBattle.MoveCurrentPiece(cell.Value);
                    }
                    else if (cell != null &&
                        card != null &&
                        !PieceBattle.CurrentPiece.IsMoving)
                    {
                        var target = ConfirmTarget(cell,card.CardTarget);
                        if (target != null)
                            switch (card)
                            {
                                case CardAttack cardAttack:
                                    cardAttack.TakeEffect(target);
                                    CardBattle.RemoveCard(cardAttack);
                                    break;
                                case CardDefense cardDefense:
                                    cardDefense.TakeEffect(target);
                                    CardBattle.RemoveCard(cardDefense);
                                    break;
                                case CardSpecial cardSpecial:
                                    cardSpecial.TakeEffect(target);
                                    CardBattle.RemoveCard(cardSpecial);
                                    break;
                                case CardItem cardItem:
                                    cardItem.TakeEffect(target);
                                    CardBattle.RemoveCard(cardItem);
                                    break;
                            }
                    }
                }
            }
        }
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

    public List<BattlePiece>? ConfirmTarget(Vector2I? cell,CardTarget cardTarget)
    {
        if (cell == null)
        {
            return null;
        }
        var targetPiece = PieceBattle.PieceMap[cell.Value];
        if (targetPiece == null)
        {
            return null;
        }
        switch (cardTarget)
        {
            case Cards.CardTarget.Self:
                if (targetPiece == CurrentPiece)
                {
                    return new List<BattlePiece> { CurrentPiece };
                }
                return null;
            case Cards.CardTarget.Enemy:
                if (targetPiece is BattleEnemy enemy)
                {
                    return new List<BattlePiece> { enemy };
                }
                return null;
            case Cards.CardTarget.Ally:
                if (targetPiece is BattleParty && targetPiece != CurrentPiece)
                {
                    return new List<BattlePiece> { targetPiece };
                }
                return null;
            case Cards.CardTarget.AllEnemies:
                if (targetPiece is BattleEnemy)
                {
                    List<BattlePiece> enemies = new List<BattlePiece>();
                    foreach (var piece in Pieces)
                    {
                        if (piece is BattleEnemy)
                        {
                            enemies.Add(piece);
                        }
                    }
                    return enemies;
                }
                return null;
            case Cards.CardTarget.AllAllies:
                if (targetPiece is BattleParty)
                {
                    List<BattlePiece> partis = new List<BattlePiece>();
                    foreach (var piece in Pieces)
                    {
                        if (piece is BattleParty)
                        {
                            partis.Add(piece);
                        }
                    }
                    return partis;
                }
                return null;
            case Cards.CardTarget.All:
                return Pieces;
        }
        return null;
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