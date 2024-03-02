namespace EESaga.Scripts.Managers;

using Cards;
using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Maps;
using System.Collections.Generic;
using System.Linq;
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
        CardBattle.EndTurn += () =>
        {
            var index = Pieces.IndexOf(CurrentPiece);
            index = (index + 1) % Pieces.Count;
            TurnTo(Pieces[index]);
        };

        TurnTo(Pieces[Pieces.Count - 1]);
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
                    var card = CardBattle.OperatingCard;
                    if (cell != null &&
                        PieceBattle.TileMap.IsDestination(cell.Value) &&
                        !PieceBattle.CurrentPiece.IsMoving &&
                        CardBattle.IsMoving)
                    {
                        PieceBattle.MoveCurrentPiece(cell.Value);
                    }
                    else if (cell != null &&
                        card != null &&
                        !PieceBattle.CurrentPiece.IsMoving)
                    {
                        var target = ConfirmTarget(cell, card.CardTarget);
                        if (target != null)
                        {
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
                            CheckDeathAndClear(target);
                            if (CardBattle.IsMoving)
                            {
                                CardBattle.IsMoving = false;
                                foreach (var cellUnhandled in PieceBattle.TileMap.GetUsedCells((int)Layer.Ground))
                                {
                                    PieceBattle.TileMap.SetCell((int)Layer.Mark, cellUnhandled);
                                }
                            }
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
            CardBattle.IsMoving = true;
            CurrentPiece = battleParty;
            PieceBattle.ShowAccessibleTiles(battleParty.MoveRange);
            GD.Print(battleParty.MoveRange);
            PrepareCards();
            CardBattle.Visible = true;
        }
        else if (battlePiece is BattleEnemy battleEnemy)
        {
            CurrentPiece = battleEnemy;
            TakeAction();
            var index = Pieces.IndexOf(CurrentPiece);
            index = (index + 1) % Pieces.Count;
            TurnTo(Pieces[index]);
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

    public List<BattlePiece>? ConfirmTarget(Vector2I? cell, CardTarget cardTarget)
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
        if (!PieceBattle.ColorMap.ContainsKey(cell.Value))
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

    public void CheckDeathAndClear(List<BattlePiece> battlePieces)
    {
        if (battlePieces.Count == 0)
        {
            return;
        }
        foreach (var piece in battlePieces)
        {
            if (piece.Health == 0)
            {
                foreach (var item in PieceBattle.PieceMap)
                {
                    if (item.Value == piece)
                    {
                        PieceBattle.ClearHeritage(item.Key);
                    }
                }
                piece.QueueFree();
            }
        }
    }
    private void OnCardBattleOperatingCardChanged()
    {
        if (CardBattle.OperatingCard == null)
        {
            PieceBattle.RecoverEffectTiles();
        }
        else
        {
            PieceBattle.ShowEffectTiles(CardBattle.OperatingCard.CardRange, CardBattle.OperatingCard.CardTarget);
        }
    }

    private void TakeAction()
    {
        GD.Print("Enemy Turn");
        var enemy = CurrentPiece as Slime;
        var location = PieceBattle.TileMap.LocalToMap(enemy.GlobalPosition);
        var targets = PieceBattle.GetNearestParty(location);
        foreach (var target in targets)
        {
            var cell = PieceBattle.TileMap.LocalToMap(target.GlobalPosition);
            var dst = FindDstAndMove(cell);
            if (dst != null)
            {
                var cells = GetNearAccessibleCell(dst.Value);
                if (cells.Contains(cell))
                {
                    for(var i =0; i< enemy.AttackTimes; i++)
                    {
                        target.Health -= enemy.AttackDamage;
                        GD.Print($"{target.Name} 受到了 {enemy.AttackDamage} 点伤害");
                        if(target.Health <= 0)
                        {
                            GD.Print($"{target.Name} 不堪重负似掉了！");
                            break;
                        }
                    }
                }
                break;
            }
        }
    }

    public List<Vector2I> GetNearAccessibleCell(Vector2I dst)
    {
        var cells = PieceBattle.TileMap.GetUsedCells((int)Layer.Ground);
        var deleteCells = new List<Vector2I>();
        List<Vector2I> accessibleCells = new List<Vector2I>() {
        new Vector2I(dst.X - 1, dst.Y),
        new Vector2I(dst.X + 1, dst.Y),
        new Vector2I(dst.X, dst.Y - 1),
        new Vector2I(dst.X, dst.Y + 1),
        new Vector2I(dst.X - 1, dst.Y - 1),
        new Vector2I(dst.X + 1, dst.Y + 1),
        new Vector2I(dst.X - 1, dst.Y + 1),
        new Vector2I(dst.X + 1, dst.Y - 1),
        };
        foreach (var cell in accessibleCells)
        {
            if (PieceBattle.PieceMap[cell]!=null)
            {
                deleteCells.Add(cell);
            }
            else if (PieceBattle.TileMap.IsBoundary((int)Layer.Ground,cell))
            {
                deleteCells.Add(cell);
            }
        }
        foreach(var cell in deleteCells)
        {
            accessibleCells.Remove(cell);
        }
        return accessibleCells;
    }

    public Vector2I? FindDstAndMove(Vector2I target)
    {
        var cell = PieceBattle.TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var SortedAccessibleCells = GetNearAccessibleCell(target).OrderBy(p => PieceBattle.GetAStarPath(p, cell).Count).ToList();
        if (SortedAccessibleCells.Count == 0)
        {
            return null;
        }
        var dst = SortedAccessibleCells[0];
        if (PieceBattle.GetAStarPath(SortedAccessibleCells[0], cell).Count <= CurrentPiece.MoveRange)
        {
            PieceBattle.MoveCurrentPiece(dst);
            return dst;
        }
        else
        {
            return FindDstAndMove(dst);
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