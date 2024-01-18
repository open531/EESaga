namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;
using System;

public partial class CardBattle : Control
{
    private Control _hand;
    private Control _deck;
    private Control _discard;

    private Button _addCardButton;
    private Button _removeCardButton;

    private static readonly PackedScene cardScene = GD.Load<PackedScene>("res://Scenes/UI/card.tscn");

    private const int _maxHandSize = 8;
    private const float _cardX = 64.0f;
    private const float _cardY = 72.0f;
    private const float _cardRotation = 0.1f;

    public override void _Ready()
    {
        _hand = GetNode<Control>("Hand");
        _deck = GetNode<Control>("Deck");
        _discard = GetNode<Control>("Discard");

        _addCardButton = GetNode<Button>("AddCardButton");
        _removeCardButton = GetNode<Button>("RemoveCardButton");

        _addCardButton.Pressed += OnAddCardButtonPressed;
        _removeCardButton.Pressed += OnRemoveCardButtonPressed;

        AddCard(CardType.Attack, "Attack", "Attack", 1, CardTarget.Enemy);
        AddCard(CardType.Defense, "Defense", "Defense", 1, CardTarget.Self);
        AddCard(CardType.Special, "Special", "Special", 1, CardTarget.AllEnemies);
        AddCard(CardType.Item, "Item", "Item", 1, CardTarget.AllAllies);
    }

    private void UpdateCardPosition()
    {
        var cards = _hand.GetChildren();
        if (cards.Count <= _maxHandSize)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var R = (cards.Count * _cardX) / Math.Tan(cards.Count * _cardRotation / 2) / double.Pi;
                var card = cards[i] as Card;
                var targetRotation = (i + 0.5f) * _cardRotation - cards.Count * _cardRotation / 2;
                var targetPosition = new Vector2(
                    (float)((i + 1) * _cardX - cards.Count * _cardX / 2 - 57.0f * Math.Cos(targetRotation)),
                    (float)(-_cardY - 57.0f / 2 * Math.Sin(targetRotation) + R * (1 - Math.Cos(targetRotation)))
                );
                var tweenPosition = CreateTween();
                var tweenRotation = CreateTween();
                var tweenScale = CreateTween();
                tweenPosition.TweenProperty(card, "position", targetPosition, 0.15f);
                tweenRotation.TweenProperty(card, "rotation", targetRotation, 0.15f);
                tweenScale.TweenProperty(card, "scale", Vector2.One, 0.2f);
            }
        }
        else
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var R = (_maxHandSize * _cardX) / Math.Tan(_maxHandSize * _cardRotation / 2) / double.Pi;
                var card = cards[i] as Card;
                var targetRotation = (i + 0.5f) * _maxHandSize * _cardRotation / cards.Count - _maxHandSize * _cardRotation / 2;
                var targetPosition = new Vector2(
                     (float)((i + 0.5f) * _maxHandSize * _cardX / cards.Count - _maxHandSize * _cardX / 2 - 57.0f / 2 * Math.Cos(targetRotation)),
                     (float)(-_cardY - 57.0f / 2 * Math.Sin(targetRotation) + R * (1 - Math.Cos(targetRotation)))
                );
                var tweenPosition = CreateTween();
                var tweenRotation = CreateTween();
                var tweenScale = CreateTween();
                tweenPosition.TweenProperty(card, "position", targetPosition, 0.15f);
                tweenRotation.TweenProperty(card, "rotation", targetRotation, 0.15f);
                tweenScale.TweenProperty(card, "scale", Vector2.One, 0.2f);
            }
        }
    }

    private void AddCard(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget)
    {
        var card = cardScene.Instantiate<Card>();
        card.InitializeCard(cardType, cardName, cardDescription, cardCost, cardTarget);
        card.Position = _deck.Position - _hand.Position;
        card.Scale = Vector2.Zero;
        _hand.AddChild(card);
        UpdateCardPosition();
    }

    private void AddCard(ICard card)
    {
        var cardNode = cardScene.Instantiate<Card>();
        cardNode.InitializeCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget);
        cardNode.Position = _deck.Position - _hand.Position;
        cardNode.Scale = Vector2.Zero;
        _hand.AddChild(cardNode);
        UpdateCardPosition();
    }

    private void RemoveCard(Card card)
    {
        if (!_hand.GetChildren().Contains(card)) return;
        var newCard = cardScene.Instantiate<Card>();
        newCard.InitializeCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget);
        newCard.Position = card.Position + _hand.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        AddChild(newCard);
        _hand.RemoveChild(card);
        var tweenRotation = CreateTween();
        var tweenPosition = CreateTween();
        var tweenScale = CreateTween();
        tweenRotation.TweenProperty(newCard, "rotation", 0.0f, 0.15f);
        tweenPosition.TweenProperty(newCard, "position", _discard.Position, 0.15f);
        tweenScale.TweenProperty(newCard, "scale", Vector2.Zero, 0.2f);
        tweenScale.TweenCallback(Callable.From(newCard.QueueFree));
        UpdateCardPosition();
    }

    private void RemoveCard(int index)
    {
        if (index < 0 || index >= _hand.GetChildCount()) return;
        var card = _hand.GetChild(index) as Card;
        var newCard = cardScene.Instantiate<Card>();
        newCard.InitializeCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget);
        newCard.Position = card.Position + _hand.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        AddChild(newCard);
        _hand.RemoveChild(card);
        var tweenRotation = CreateTween();
        var tweenPosition = CreateTween();
        var tweenScale = CreateTween();
        tweenRotation.TweenProperty(newCard, "rotation", 0.0f, 0.15f);
        tweenPosition.TweenProperty(newCard, "position", _discard.Position, 0.15f);
        tweenScale.TweenProperty(newCard, "scale", Vector2.Zero, 0.2f);
        tweenScale.TweenCallback(Callable.From(newCard.QueueFree));
        UpdateCardPosition();
    }

    private void OnAddCardButtonPressed()
    {
        AddCard(CardType.Attack, "Attack", "Attack", 1, CardTarget.Enemy);
    }

    private void OnRemoveCardButtonPressed()
    {
        RemoveCard(0);
    }
}
