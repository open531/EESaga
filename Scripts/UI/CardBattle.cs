namespace EESaga.Scripts.UI;

using Cards;
using Entities;
using Entities.BattleParties;
using Godot;
using System;
using System.Collections.Generic;

public partial class CardBattle : CanvasLayer
{
    private Control _hand;
    private Control _deck;
    private Control _discard;

    private CardDetail _cardDetail;

    private Button _endTurnButton;

    private Label _energyLabel;

    private TextureButton _deckButton;
    private Label _deckCardNum;
    private TextureButton _discardButton;
    private Label _discardCardNum;

    private CardViewer _deckCardViewer;
    private CardViewer _discardCardViewer;

    private BattleCards _battleCards;
    public BattleCards BattleCards
    {
        get => _battleCards;
        set
        {
            _battleCards = value;
            UpdateHandCard();
        }
    }

    private Card _selectedCard;
    public Card SelectedCard
    {
        get => _selectedCard;
        set
        {
            _selectedCard = value;
            _cardDetail.Update(_selectedCard);
        }
    }
    private Card _operatingCard;
    public Card OperatingCard
    {
        get => _operatingCard;
        private set
        {
            _operatingCard = value;
            EmitSignal(SignalName.OperatingCardChanged);
        }
    }

    private bool _isMoving = true;

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
        }
    }

    [Signal] public delegate void OperatingCardChangedEventHandler();
    [Signal] public delegate void CardUpdatedEventHandler();
    [Signal] public delegate void EndTurnEventHandler();

    private const int _maxHandSize = 8;
    private const float _cardX = 64.0f;
    private const float _cardY = 72.0f;
    private const float _cardRotation = 0.1f;
    private const float _cardWidth = 57.0f;
    private const float _cardHeight = 88.0f;

    public static CardBattle Instance() => GD.Load<PackedScene>("res://Scenes/UI/card_battle.tscn").Instantiate<CardBattle>();

    public override void _Ready()
    {
        _hand = GetNode<Control>("Hand");
        _deck = GetNode<Control>("Deck");
        _discard = GetNode<Control>("Discard");

        _cardDetail = GetNode<CardDetail>("CardDetail");

        _endTurnButton = GetNode<Button>("EndTurnButton");

        _energyLabel = GetNode<Label>("EnergyLabel");

        _deckButton = GetNode<TextureButton>("Deck/TextureButton");
        _deckCardNum = GetNode<Label>("Deck/Label");
        _discardButton = GetNode<TextureButton>("Discard/TextureButton");
        _discardCardNum = GetNode<Label>("Discard/Label");

        _deckCardViewer = GetNode<CardViewer>("DeckCardViewer");
        _discardCardViewer = GetNode<CardViewer>("DiscardCardViewer");

        _selectedCard = null;
        OperatingCard = null;

        _deckButton.Pressed += () =>
        {
            _deckCardViewer.DisplayCards(BattleCards.DeckCards);
        };

        _discardButton.Pressed += () =>
        {
            _discardCardViewer.DisplayCards(BattleCards.DiscardCards);
        };

        CardUpdated += OnCardUpdated;
    }

    public void TurnTo(BattlePiece battlePiece)
    {
        UpdateEnergyLabel(battlePiece);
        if (battlePiece is BattleParty party)
        {
            BattleCards = party.BattleCards;
            _deck.Visible = true;
            _discard.Visible = true;
        }
        else
        {
            BattleCards = BattleCards.Empty;
            _deck.Visible = false;
            _discard.Visible = false;
        }
    }

    private void UpdateHandCard()
    {
        var oldCards = _hand.GetChildren();
        foreach (var card in oldCards)
        {
            card.QueueFree();
        }
        foreach (var card in BattleCards.HandCards)
        {
            AddCard(card);
        }
        EmitSignal(SignalName.CardUpdated);
    }

    private void UpdateBattleCardsCount()
    {
        _deckCardNum.Text = BattleCards.DeckCards.Count.ToString();
        _discardCardNum.Text = BattleCards.DiscardCards.Count.ToString();
    }

    private void UpdateCardPosition()
    {
        var cards = _hand.GetChildren();
        if (cards.Count <= _maxHandSize)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var r = (cards.Count * _cardX) / Math.Tan(cards.Count * _cardRotation / 2) / double.Pi;
                var card = cards[i] as Card;
                var targetRotation = (i + 0.5f) * _cardRotation - cards.Count * _cardRotation / 2;
                var targetPosition = new Vector2(
                    (float)((i + 1) * _cardX - cards.Count * _cardX / 2 - _cardWidth * Math.Cos(targetRotation)),
                    (float)(-_cardY - _cardWidth / 2 * Math.Sin(targetRotation) + r * (1 - Math.Cos(targetRotation)))
                );
                var tweenPosition = CreateTween();
                var tweenRotation = CreateTween();
                var tweenScale = CreateTween();
                tweenPosition.TweenProperty(card, "position", targetPosition, 0.15);
                tweenRotation.TweenProperty(card, "rotation", targetRotation, 0.15);
                tweenScale.TweenProperty(card, "scale", Vector2.One, 0.2);
            }
        }
        else
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var r = (_maxHandSize * _cardX) / Math.Tan(_maxHandSize * _cardRotation / 2) / double.Pi;
                var card = cards[i] as Card;
                var targetRotation = (i + 0.5f) * _maxHandSize * _cardRotation / cards.Count - _maxHandSize * _cardRotation / 2;
                var targetPosition = new Vector2(
                     (float)((i + 0.5f) * _maxHandSize * _cardX / cards.Count - _maxHandSize * _cardX / 2 - _cardWidth / 2 * Math.Cos(targetRotation)),
                     (float)(-_cardY - _cardWidth / 2 * Math.Sin(targetRotation) + r * (1 - Math.Cos(targetRotation)))
                );
                var tweenPosition = CreateTween();
                var tweenRotation = CreateTween();
                var tweenScale = CreateTween();
                tweenPosition.TweenProperty(card, "position", targetPosition, 0.15);
                tweenRotation.TweenProperty(card, "rotation", targetRotation, 0.15);
                tweenScale.TweenProperty(card, "scale", Vector2.One, 0.2f);
            }
        }
    }

    private void AddCard(CardInfo card)
    {
        var cardNode = CardFactory.CreateCard(card);
        cardNode.InitializeCard(card);
        cardNode.Position = _deck.Position - _hand.Position;
        cardNode.Scale = Vector2.Zero;
        cardNode.MouseEntered += () =>
        {
            if (OperatingCard != null) return;
            SelectedCard = cardNode;
            PreviewCard(cardNode);
        };
        _hand.AddChild(cardNode);
        EmitSignal(SignalName.CardUpdated);
    }

    public void RemoveCard(Card card)
    {
        if (!_hand.GetChildren().Contains(card)) return;
        var newCard = Card.Instance();
        var previewCard = GetNodeOrNull("PreviewCard") as Card;
        if (card == OperatingCard)
        {
            OperatingCard = null;
        }
        if (previewCard.Parent == card)
        {
            previewCard.QueueFree();
        }
        newCard.Name = "RemovedCard";
        newCard.SetCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget, card.CardRange);
        newCard.Position = card.Position + _hand.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        AddChild(newCard);
        BattleCards.DiscardCards.Add(card.CardInfo);
        _hand.RemoveChild(card);
        var tweenPosition = CreateTween();
        var tweenRotation = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position", _discard.Position, 0.15);
        tweenRotation.TweenProperty(newCard, "rotation", 0.0f, 0.15);
        tweenScale.TweenProperty(newCard, "scale", Vector2.Zero, 0.2);
        tweenScale.TweenCallback(Callable.From(newCard.QueueFree));
        EmitSignal(SignalName.CardUpdated);
    }

    public void RemoveCard(int index)
    {
        if (index < 0 || index >= _hand.GetChildCount()) return;
        var card = _hand.GetChild(index) as Card;
        var newCard = Card.Instance();
        var previewCard = GetNodeOrNull("PreviewCard") as Card;
        if (card == OperatingCard)
        {
            OperatingCard = null;
        }
        if (previewCard.Parent == card)
        {
            previewCard.QueueFree();
        }
        newCard.Name = "RemovedCard";
        newCard.SetCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget, card.CardRange);
        newCard.Position = card.Position + _hand.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        AddChild(newCard);
        BattleCards.DiscardCards.Add(card.CardInfo);
        _hand.RemoveChild(card);
        var tweenPosition = CreateTween();
        var tweenRotation = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position", _discard.Position, 0.15);
        tweenRotation.TweenProperty(newCard, "rotation", 0.0f, 0.15);
        tweenScale.TweenProperty(newCard, "scale", Vector2.Zero, 0.2);
        tweenScale.TweenCallback(Callable.From(newCard.QueueFree));
        EmitSignal(SignalName.CardUpdated);
    }

    private void PreviewCard(Card card)
    {
        if (card == null) return;
        var previewCard = GetNodeOrNull("PreviewCard") as Card;
        if (previewCard != null)
        {
            previewCard.Parent.Visible = true;
            previewCard.Free();
        }
        var newCard = Card.Instance();
        newCard.Name = "PreviewCard";
        newCard.SetCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget, card.CardRange);
        newCard.Position = card.Position + _hand.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        newCard.Parent = card;
        newCard.GuiInput += OperateCard;
        newCard.MouseExited += ExitPreviewCard;
        AddChild(newCard);
        var tweenPosition = CreateTween();
        var tweenRotation = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position",
            new Vector2(card.Position.X - _cardWidth / 2, -_cardHeight) + _hand.Position, 0.05);
        tweenRotation.TweenProperty(newCard, "rotation", 0.0f, 0.05);
        tweenScale.TweenProperty(newCard, "scale", Vector2.One * 2, 0.05);
        card.Visible = false;
    }

    private void ExitPreviewCard()
    {
        var newCard = GetNodeOrNull("PreviewCard") as Card;
        SelectedCard = null;
        var tweenPosition = CreateTween();
        var tweenRotation = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position", newCard.Parent.Position + _hand.Position, 0.05);
        tweenRotation.TweenProperty(newCard, "rotation", newCard.Parent.Rotation, 0.05);
        tweenScale.TweenProperty(newCard, "scale", newCard.Parent.Scale, 0.05);
        tweenScale.TweenCallback(Callable.From(newCard.Free));
        newCard.Parent.Visible = true;
    }

    private void OperateCard(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.IsReleased())
                {
                    if (GetNodeOrNull("PreviewCard") is Card previewCard)
                    {
                        if (OperatingCard != previewCard.Parent)
                        {
                            OperatingCard = previewCard.Parent;
                            previewCard.MouseExited -= ExitPreviewCard;
                        }
                        else
                        {
                            OperatingCard = null;
                            previewCard.MouseExited += ExitPreviewCard;
                        }
                    }
                }
            }
        }
    }

    private void UpdateEnergyLabel(BattlePiece battlePiece)
    {
        if (battlePiece is BattleParty party)
        {
            _energyLabel.Text = $"{party.Energy}/{party.EnergyMax}";
        }
        else
        {
            _energyLabel.Text = "";
        }
    }

    private void OnCardUpdated()
    {
        UpdateCardPosition();
        UpdateBattleCardsCount();
    }

    private void OnEndTurnButtonPressed()
    {
        EmitSignal(SignalName.EndTurn);
    }
}

public class BattleCards
{
    public List<CardInfo> DeckCards { get; set; }
    public List<CardInfo> HandCards { get; set; }
    public List<CardInfo> DiscardCards { get; set; }
    public static BattleCards Empty => new()
    {
        DeckCards = [],
        HandCards = [],
        DiscardCards = [],
    };
}
