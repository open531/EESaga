namespace EESaga.Scripts.Managers;

using Cards;
using Cards.CardDefenses;
using Cards.CardItems;
using Cards.CardSpecials;
using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Maps;
using System.Collections.Generic;
using System.Linq;
using UI;
using Utilities;
using EESaga.Scripts.Cards.CardAttacks;
using System;
using EESaga.Scripts.Data;

public partial class BattleManager : Node
{
    public SceneSwitcher _sceneSwitcher;
    public bool _timerReady;
    private bool _attackPermitted;
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
        _attackPermitted = false;
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

        TurnTo(Pieces[0]);
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
                        var currentParty = CurrentPiece as BattleParty;
                        if (currentParty.Energy > 0)
                        {
                            var target = ConfirmTarget(cell, card.CardTarget);
                            if (target != null)
                            {
                                switch (card)
                                {
                                    case CardAttack cardAttack:
                                        {
                                            switch (card)
                                            {
                                                default:
                                                    if (cardAttack.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardAttack.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("CARD_ATTACK")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardBash cardBash:
                                                    if (cardBash.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardBash.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_A_BASH")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardDoubleBeat cardDoubleBeat:
                                                    if (cardDoubleBeat.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardDoubleBeat.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("CARD_DOUBLE_BEAT")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                            }
                                        }
                                        CardBattle.RemoveCard(cardAttack);
                                        break;
                                    case CardDefense cardDefense:
                                        {
                                            switch (card)
                                            {
                                                default:
                                                    if (cardDefense.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardDefense.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("CARD_DEFENSE")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardConsolidate cardConsolidate:
                                                    if (cardConsolidate.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardConsolidate.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_D_CONSOLIDATE")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                            }
                                        }
                                        CardBattle.RemoveCard(cardDefense);
                                        break;
                                    case CardSpecial cardSpecial:
                                        {
                                            switch (card)
                                            {
                                                case CardCarRush cardCarRush:
                                                    if (cardCarRush.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardCarRush.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_CAR_RUSH")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardCodeBoom cardCodeBoom:
                                                    if (cardCodeBoom.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardCodeBoom.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_CODE_BOOM")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;  
                                                case CardCure cardCure:
                                                    if (cardCure.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardCure.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("CARD_CURE")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardSurvive cardSurvive:
                                                    if (cardSurvive.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardSurvive.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_SURVIVE")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardAttackByDefense cardAttackByDefense:
                                                    if (cardAttackByDefense.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        target.Add(CurrentPiece);
                                                        var cardInfo = cardAttackByDefense.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_SHIELD_STRIKE")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardFury cardFury:
                                                    if (cardFury.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardFury.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_FURY")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                                case CardStruggle cardStruggle:
                                                    PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_S_STRUGGLE")}\n", $"{Tr("T_STRUGGLE")}");
                                                    break;
                                                default:
                                                    if (cardSpecial.TakePartyCost(CurrentPiece as BattleParty))
                                                    {
                                                        var cardInfo = cardSpecial.TakeEffect(target);
                                                        CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                                        PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                                    }
                                                    else
                                                    {
                                                        PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("CARD_SPECIAL")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                                    }
                                                    break;
                                            }
                                        }
                                        if (cardSpecial is CardStruggle)
                                        {
                                            var previewCard = CardBattle.GetNodeOrNull("PreviewCard") as Card;
                                            if (previewCard != null)
                                            {
                                                CardBattle.OperatingCard = null;
                                                CardBattle.ExitPreviewCard();
                                            }
                                        }
                                        else
                                        {
                                            CardBattle.RemoveCard(cardSpecial);
                                        }
                                        break;
                                    case CardItem cardItem:
                                        if (cardItem.TakePartyCost(CurrentPiece as BattleParty))
                                        {
                                            var cardInfo = cardItem.TakeEffect(target);
                                            CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
                                            PieceBattle.UpdateActionInfo(cardInfo[0], cardInfo[1]);
                                            CardBattle.RemoveCard(cardItem);
                                        }
                                        else
                                        {
                                            PieceBattle.UpdateActionInfo($"{Tr("T_USE")}{Tr("C_I_ECS")}\n", $"{Tr("T_ENERGY_NOT_ENOUGH")}");
                                        }
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
                            var index = Parties.IndexOf(currentParty);
                            PieceBattle._stateBarList[index].UpdateStateBar(currentParty);
                        }
                        else
                        {
                            GD.Print("Not enough energy");
                            CardBattle.OperatingCard = null;
                            CardBattle.ExitPreviewCard();
                            PieceBattle.UpdateActionInfo($"{Tr("T_ENERGY_NOT_ENOUGH")}", "");
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
        if (CurrentPiece == battlePiece)
        {
            return;
        }
        battlePiece.Shield = 0;
        if (battlePiece is BattleParty battleParty)
        {
            battleParty.Energy = battleParty.EnergyMax;
            var index = Parties.IndexOf(battleParty);
            PieceBattle._stateBarList[index].UpdateStateBar(battleParty);
            CurrentPiece = battleParty;
            PieceBattle.UpdateActionInfo("", "");
            GD.Print($"{CurrentPiece.PieceName} Turn");
            CardBattle.ShowUI();
            CardBattle.IsMoving = true;
            CardBattle.BattleCards = battleParty.BattleCards;
            CardBattle.UpdateEnergyLabel(battleParty);
            PieceBattle.ShowAccessibleTiles(battleParty.MoveRange);
            PrepareCards(battleParty.HandCardCount);
        }
        else if (battlePiece is BattleEnemy battleEnemy)
        {
            CurrentPiece = battleEnemy;
            if (battleEnemy.SleepTurns > 0)
            {
                GD.Print("SleepTurns");
                battleEnemy.SleepTurns--;
                CardBattle.EmitSignal(CardBattle.SignalName.EndTurn);
                return;
            }
            PieceBattle.UpdateActionInfo("", "");
            GD.Print($"{CurrentPiece.PieceName} Turn");
            CardBattle.HideUI();
            CardBattle.BattleCards = BattleCards.Empty;
            PieceBattle.ShowAccessibleTiles(CurrentPiece.MoveRange, true);
            TakeAction();
        }
    }

    public void PrepareCards(int num, bool append = false)
    {
        if (CurrentPiece is BattleParty battleParty)
        {
            if (battleParty.BattleCards.DeckCards.Count < num)
            {
                foreach (var card in battleParty.BattleCards.DiscardCards)
                {
                    battleParty.BattleCards.DeckCards.Add(card);
                }
                battleParty.BattleCards.DiscardCards.Clear();
            }
            var appendList = new List<CardInfo>();
            var rng = new Godot.RandomNumberGenerator();
            rng.Randomize();
            for (var i = 0; i < num; i++)
            {
                var randomIndex = rng.RandiRange(0, battleParty.BattleCards.DeckCards.Count - 1);
                var card = battleParty.BattleCards.DeckCards[randomIndex];
                battleParty.BattleCards.HandCards.Add(card);
                battleParty.BattleCards.DeckCards.Remove(card);
                appendList.Add(card);
            }
            if (!append)
            {
                CardBattle.BattleCards = battleParty.BattleCards;
            }
            else
            {
                CardBattle.UpdateHandCard(appendList, append);
                CardBattle.UpdateCardPosition();
            }
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
            case Cards.CardTarget.Straight:
                if (targetPiece is BattleEnemy enemys)
                {
                    var TargetEnemies = new List<BattlePiece>();
                    var myCell = PieceBattle.TileMap.LocalToMap(CurrentPiece.GlobalPosition);
                    var dir = GetDirection(myCell, cell.Value);
                    GD.Print(dir);
                    if (dir != null)
                    {
                        while (true)
                        {
                            myCell += dir.Value;
                            if (!PieceBattle.PieceMap.ContainsKey(myCell))
                            {
                                break;
                            }
                            if (PieceBattle.TileMap.IsBoundary((int)Layer.Ground, myCell))
                            {
                                break;
                            }
                            var target = PieceBattle.PieceMap[myCell];
                            if (target is BattleEnemy)
                            {
                                TargetEnemies.Add(target);
                            }
                        }
                        return TargetEnemies;
                    }
                    else
                    {
                        return null;
                    }

                }
                return null;
            case Cards.CardTarget.Square:
                if (targetPiece is BattleEnemy enemyss)
                {
                    var TargetEnemies = GetCloseEnemy(cell.Value);
                    return TargetEnemies;
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

    public Vector2I? GetDirection(Vector2I myCell, Vector2I targetCell)
    {
        var dif = new Vector2I(targetCell.X - myCell.X, targetCell.Y - myCell.Y);
        if (dif.X != 0 && dif.Y != 0)
        {
            return null;
        }

        if (dif.X == 0)
        {
            if (dif.Y > 0)
            {
                return new Vector2I(0, 1);
            }
            else
            {
                return new Vector2I(0, -1);
            }
        }
        else
        {
            if (dif.X > 0)
            {
                return new Vector2I(1, 0);
            }
            else
            {
                return new Vector2I(-1, 0);
            }
        }
    }

    //public void UseCard(Card card, List<BattlePiece> target)
    //{
    //    switch (card)
    //    {
    //        case CardAttack cardAttack:
    //            if (cardAttack.TakePartyCost(CurrentPiece as BattleParty))
    //            {
    //                cardAttack.TakeEffect(target);
    //                CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
    //                CardBattle.RemoveCard(cardAttack);
    //            }
    //            break;
    //        case CardDefense cardDefense:
    //            if (cardDefense.TakePartyCost(CurrentPiece as BattleParty))
    //            {
    //                cardDefense.TakeEffect(target);
    //                CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
    //                CardBattle.RemoveCard(cardDefense);
    //            }
    //            break;
    //        case CardSpecial cardSpecial:
    //            if (cardSpecial.TakePartyCost(CurrentPiece as BattleParty))
    //            {
    //                cardSpecial.TakeEffect(target);
    //                CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
    //                CardBattle.RemoveCard(cardSpecial);
    //            }
    //            break;
    //        case CardItem cardItem:
    //            if (cardItem.TakePartyCost(CurrentPiece as BattleParty))
    //            {
    //                cardItem.TakeEffect(target);
    //                CardBattle.UpdateEnergyLabel(CurrentPiece as BattleParty);
    //                CardBattle.RemoveCard(cardItem);
    //            }
    //            break;
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

    private void TakeAction()
    {
        var enemy = CurrentPiece as BattleEnemy;
        var location = PieceBattle.TileMap.LocalToMap(enemy.GlobalPosition);
        var targets = PieceBattle.GetNearestParty(location);
        if (targets == null)
        {
            _attackPermitted = false;
            DelayTimer.WaitTime = 0f;
            DelayTimer.Start();
            return;
        }
        else
        {
            var attackableCells = GetNearAccessibleCell(location, enemyFight: true);
            if (targets.Count == 0)
            {
                _attackPermitted = false;
                DelayTimer.WaitTime = 0.3f;
                DelayTimer.Start();
                return;
            }
            var primaryTarget = targets[0];
            var primaryCell = PieceBattle.TileMap.LocalToMap(primaryTarget.GlobalPosition);
            if (attackableCells.Contains(primaryCell))
            {
                Destination = location;
                CurrentTarget = primaryTarget;
                CurrentEnemy = enemy;
                _attackPermitted = true;
                DelayTimer.WaitTime = 0.3f;
                DelayTimer.Start();
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
                        var permit = info[3];
                        if (permit == 0)
                        {
                            _attackPermitted = false;
                        }
                        else
                        {
                            _attackPermitted = true;
                        }
                        var wayLength = info[2];
                        Destination = new Vector2I(info[0], info[1]);
                        DelayTimer.WaitTime = wayLength * 0.25f;
                        DelayTimer.Start();
                        break;
                    }
                    else
                    {
                        DelayTimer.WaitTime = 0.3f;
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
        if (cells.Contains(cell) && _attackPermitted)
        {
            enemy.Attack(target);
        }
        else
        {
            enemy.Defend(enemy);
        }
        CardBattle.EmitSignal(CardBattle.SignalName.EndTurn);
    }

    public List<BattlePiece> GetCloseEnemy(Vector2I target)
    {
        var cells = GetNearAccessibleCell(target, partyFight: true);
        cells.Add(target);
        var closeEnemies = new List<BattlePiece>();
        foreach (var cell in cells)
        {
            if (PieceBattle.PieceMap.TryGetValue(cell, out BattlePiece value))
            {
                if (value is BattleEnemy enemy)
                {
                    closeEnemies.Add(enemy);
                }
            }
        }
        return closeEnemies;
    }

    public List<Vector2I> GetNearAccessibleCell(Vector2I dst, bool partyFight = false, bool enemyFight = false, int range = 1)
    {
        var cells = PieceBattle.TileMap.GetUsedCells((int)Layer.Ground);
        var deleteCells = new List<Vector2I>();
        List<Vector2I> accessibleCells = new List<Vector2I>();

        for (int i = -range; i < range + 1; i++)
        {
            accessibleCells.Add(new Vector2I(dst.X - range, dst.Y - i));
            accessibleCells.Add(new Vector2I(dst.X + range, dst.Y - i));
        }
        for (int i = -range + 1; i < range; i++)
        {
            accessibleCells.Add(new Vector2I(dst.X + i, dst.Y - range));
            accessibleCells.Add(new Vector2I(dst.X + i, dst.Y + range));
        }
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

    public List<int>? FindDstAndMove(Vector2I target, bool isTarget = true)
    {
        var info = new List<int>();
        var dst = new Vector2I();
        var dstPath = new List<Vector2I>();
        var cell = PieceBattle.TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var SortedAccessibleCells = new List<Vector2I>();
        for (int i = 1; ; i++)
        {
            SortedAccessibleCells = GetNearAccessibleCell(target, range: i).OrderBy(p => PieceBattle.GetAStarPath(cell, p).Count).ToList();
            if (SortedAccessibleCells.Count == 0)
            {
                continue;
            }
            for (int j = 0; j < SortedAccessibleCells.Count; j++)
            {
                var item = SortedAccessibleCells[j];
                dstPath = PieceBattle.GetAStarPath(cell, item);
                if (dstPath.Count > 0)
                {
                    dst = item;
                    break;
                }
            }
            if (dstPath.Count > 0)
            {
                break;
            }
        }
        if (dstPath.Count == CurrentPiece.MoveRange + 1)
        {
            info.Add(dst.X);
            info.Add(dst.Y);
            info.Add(dstPath.Count);
            if (isTarget)
            {
                info.Add(1);
            }
            else
            {
                info.Add(0);
            }
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
            if (isTarget)
            {
                info.Add(1);
            }
            else
            {
                info.Add(0);
            }
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
