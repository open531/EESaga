namespace EESaga.Scripts.Managers;

using Cards;
using Cards.CardAttacks;
using Cards.CardDefenses;
using Cards.CardItems;
using Cards.CardSpecials;
using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Godot.Collections;
using Maps;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UI;
using Utilities;

public partial class BattleManager : Node
{
    public SceneSwitcher _sceneSwitcher;
    public bool _timerReady;
    public bool IsGameOver { get; set; }
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
        set
        {
            PieceBattle.CurrentPiece = value;
        }
    }
    public Vector2I? SelectedCell => PieceBattle.TileMap.SelectedCell;
    public Card SelectedCard => CardBattle.SelectedCard;
    public BattlePiece CardTarget { get; set; }
    public Timer DelayTimer { get; set; }
    public Vector2I Destination { get; set; }
    public Vector2I TargetCell { get; set; }
    public BattleEnemy CurrentEnemy { get; set; }
    public BattlePiece CurrentTarget { get; set; }

    [Signal] public delegate void TakenActionEventHandler();
    [Signal] public delegate void TimeOutEventHandler();

	public static BattleManager Instance() => GD.Load<PackedScene>("res://Scenes/Managers/battle_manager.tscn").Instantiate<BattleManager>();

    public override void _Ready()
    {
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        PieceBattle = GetNode<PieceBattle>("PieceBattle");
        CardBattle = GetNode<CardBattle>("CardBattle");
        DelayTimer = GetNode<Timer>("DelayTimer");
        IsGameOver = false;
        _timerReady = false;

        CardBattle.OperatingCardChanged += OnCardBattleOperatingCardChanged;

        DelayTimer.Timeout += OnTimeOut;
        CardBattle.EndTurn += () =>
        {
            var index = Pieces.IndexOf(CurrentPiece);
            if (Pieces.Count > 0)
            {
                index = (index + 1) % Pieces.Count;
                GD.Print($"index: {index}");
                TurnTo(Pieces[index]);
            }
        };
        CardBattle.BattleManager = this;
        PieceBattle.BattleManager = this;

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
                                    {
                                        switch (card)
                                        {
                                            case CardCure cardCure:
                                                cardCure.TakeEffect(target);
                                                break;
                                            default:
                                                cardSpecial.TakeEffect(target);
                                                break;
                                        }
                                    }
                                    CardBattle.RemoveCard(cardSpecial);
                                    break;
                                case CardItem cardItem:
                                    cardItem.TakeEffect(target);
                                    CardBattle.RemoveCard(cardItem);
                                    break;
                            }
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

    public async void TurnTo(BattlePiece battlePiece)
    {
        if (CurrentPiece == battlePiece)
        {
            return;
        }
        battlePiece.Shield = 0;
        if (battlePiece is BattleParty battleParty)
        {
            CurrentPiece = battleParty;
            GD.Print($"{CurrentPiece.PieceName} Turn");
            CardBattle.ShowUI();
            CardBattle.IsMoving = true;
            CardBattle.BattleCards = battleParty.BattleCards;
            CardBattle.UpdateEnergyLabel(battleParty);
            PieceBattle.ShowAccessibleTiles(battleParty.MoveRange);
            PrepareCards();
        }
        else if (battlePiece is BattleEnemy battleEnemy)
        {
            CurrentPiece = battleEnemy;
            GD.Print($"{CurrentPiece.PieceName} Turn");
            CardBattle.HideUI();
            CardBattle.BattleCards = BattleCards.Empty;
            PieceBattle.ShowAccessibleTiles(CurrentPiece.MoveRange, true);
            TakeAction();
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
            var rng = new Godot.RandomNumberGenerator();
            rng.Randomize();
            for (var i = 0; i < battleParty.HandCardCount; i++)
            {
                var randomIndex = rng.RandiRange(0, battleParty.BattleCards.DeckCards.Count - 1);
                var card = battleParty.BattleCards.DeckCards[randomIndex];
                battleParty.BattleCards.HandCards.Add(card);
                battleParty.BattleCards.DeckCards.Remove(card);
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
					return [CurrentPiece];
				}
				return null;
			case Cards.CardTarget.Enemy:
				if (targetPiece is BattleEnemy enemy)
				{
					return [enemy];
				}
				return null;
			case Cards.CardTarget.Ally:
				if (targetPiece is BattleParty && targetPiece != CurrentPiece)
				{
					return [targetPiece];
				}
				return null;
			case Cards.CardTarget.AllEnemies:
				if (targetPiece is BattleEnemy)
				{
					List<BattlePiece> enemies = [];
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
					List<BattlePiece> parties = [];
					foreach (var piece in Pieces)
					{
						if (piece is BattleParty)
						{
							parties.Add(piece);
						}
					}
					return parties;
				}
				return null;
			case Cards.CardTarget.All:
				return Pieces;
		}
		return null;
	}

	public void UseCard(Card card, List<BattlePiece> target)
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
	}

    //public void CheckDeathAndClear(List<BattlePiece> battlePieces)
    //{
    //    if (battlePieces.Count == 0)
    //    {
    //        return;
    //    }
    //    foreach (var piece in battlePieces)
    //    {
    //        if (piece.Health == 0)
    //        {
    //            foreach (var item in PieceBattle.PieceMap)
    //            {
    //                if (item.Value == piece)
    //                {
    //                    PieceBattle.ClearByCell(item.Key);
    //                }
    //            }
    //            piece.QueueFree();
    //        }
    //    }
    //}
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

    private async void TakeAction()
    {
        var enemy = CurrentPiece as BattleEnemy;
        var location = PieceBattle.TileMap.LocalToMap(enemy.GlobalPosition);
        var targets = PieceBattle.GetNearestParty(location);
        if (targets == null)
        {
            DelayTimer.WaitTime = 0f;
            DelayTimer.Start();
            return;
        }
        else
        {
            var attackableCells = GetNearAccessibleCell(location, enemyFight: true);
            if (targets.Count == 0)
            {
                DelayTimer.WaitTime = 0f;
                DelayTimer.Start();
                return;
            }
            var primaryTarget = targets[0];
            var primaryCell = PieceBattle.TileMap.LocalToMap(primaryTarget.GlobalPosition);
            if (attackableCells.Contains(primaryCell))
            {
                DelayTimer.WaitTime = 0.2f;
                DelayTimer.Start();
                enemy.Attack(primaryTarget);
                return;
            }
            else
            {
                foreach (var target in targets)
                {
                    CurrentEnemy = enemy;
                    CurrentTarget = target;
                    TargetCell = PieceBattle.TileMap.LocalToMap(target.GlobalPosition);
                    var info = FindDstAndMove(TargetCell);
                    if (info != null)
                    {
                        var wayLength = info[2];
                        Destination = new Vector2I(info[0], info[1]);
                        DelayTimer.WaitTime = wayLength * 0.4f;
                        DelayTimer.Start();
                    }
                }
                return;
            }
        }
    }

    public void OnTimeOut()
    {
        DelayTimer.Stop();
        var dst = Destination;
        var cell = TargetCell;
        var target = CurrentTarget as BattleParty;
        var enemy = CurrentEnemy;
        var cells = GetNearAccessibleCell(dst, enemyFight: true);
        if (cells.Contains(cell))
        {
            enemy.Attack(target);
        }
        else
        {
            enemy.Defend(enemy);
        }
        CardBattle.EmitSignal(CardBattle.SignalName.EndTurn);
    }

	public List<Vector2I> GetNearAccessibleCell(Vector2I dst, bool partyFight = false, bool enemyFight = false)
	{
		var cells = PieceBattle.TileMap.GetUsedCells((int)Layer.Ground);
		var deleteCells = new List<Vector2I>();
		List<Vector2I> accessibleCells = [
		new Vector2I(dst.X - 1, dst.Y),
			new Vector2I(dst.X + 1, dst.Y),
			new Vector2I(dst.X, dst.Y - 1),
			new Vector2I(dst.X, dst.Y + 1),
			new Vector2I(dst.X - 1, dst.Y - 1),
			new Vector2I(dst.X + 1, dst.Y + 1),
			new Vector2I(dst.X - 1, dst.Y + 1),
			new Vector2I(dst.X + 1, dst.Y - 1),
		];
		foreach (var cell in accessibleCells)
		{
			if (PieceBattle.TileMap.IsBoundary((int)Layer.Ground, cell))
			{
				deleteCells.Add(cell);
			}
			else if (!PieceBattle.PieceMap.TryGetValue(cell, out BattlePiece value))
			{
				deleteCells.Add(cell);
			}
			else if (value != null)
			{
				if (partyFight && value is BattleEnemy) { }
				else if (enemyFight && value is BattleParty) { }
				else { deleteCells.Add(cell); }
			}
		}
		foreach (var cell in deleteCells)
		{
			accessibleCells.Remove(cell);
		}
		return accessibleCells;
	}

    public Array<int>? FindDstAndMove(Vector2I target, bool isTarget = true)
    {
        var info = new Array<int>();
        var cell = PieceBattle.TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var SortedAccessibleCells = GetNearAccessibleCell(target).OrderBy(p => PieceBattle.GetAStarPath(cell, p).Count).ToList();
        if (SortedAccessibleCells.Count == 0)
        {
            return null;
        }
        var dst = SortedAccessibleCells[0];
        var dstPath = PieceBattle.GetAStarPath(cell, dst);
        var path = new Array<Vector2I>(dstPath);
        if (dstPath.Count == CurrentPiece.MoveRange + 1)
        {
            info.Add(dst.X);
            info.Add(dst.Y);
            info.Add(dstPath.Count);
            PieceBattle.MoveCurrentPiece(dst);
            return info;
        }
        else if (dstPath.Count < CurrentPiece.MoveRange + 1)
        {
            if (!isTarget)
            {
                var newList = SortedAccessibleCells.OrderBy(p => PieceBattle.GetManhattanDistance(target, dst)).ToList();
                foreach (var item in newList)
                {
                    if (item != dst)
                    {
                        dst = item;
                        break;
                    }
                }
            }
            var secondDstPath = PieceBattle.GetAStarPath(cell, dst);
            PieceBattle.MoveCurrentPiece(dst);
            info.Add(dst.X);
            info.Add(dst.Y);
            info.Add(secondDstPath.Count);
            return info;
        }
        else
        {
            return FindDstAndMove(dst, false);
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
