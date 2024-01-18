namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;
using System;

public partial class CardBattle : Control
{
    private Control _hand;
    private Control _deck;
    private Control _discard;

    private static PackedScene cardScene = GD.Load<PackedScene>("res://Scenes/UI/card.tscn");

    private const int _maxHandSize = 8;
    private const float _cardX = 64.0f;
    private const float _cardY = 72.0f;
    private const float _cardRotation = 0.1f;

    public override void _Ready()
    {
        _hand = GetNode<Control>("Hand");
        _deck = GetNode<Control>("Deck");
        _discard = GetNode<Control>("Discard");

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
                card.Rotation = (i + 0.5f) * _cardRotation - cards.Count * _cardRotation / 2;
                card.Position = new Vector2(
                    (float)((i + 1) * _cardX - cards.Count * _cardX / 2 - 57.0f * Math.Cos(card.Rotation)),
                    (float)(-_cardY - 57.0f / 2 * Math.Sin(card.Rotation) + R * (1 - Math.Cos(card.Rotation)))
                );
            }
        }
        else
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var R = (_maxHandSize * _cardX) / Math.Tan(_maxHandSize * _cardRotation / 2) / double.Pi;
                var card = cards[i] as Card;
                card.Rotation = (i + 0.5f) * _maxHandSize * _cardRotation / cards.Count - _maxHandSize * _cardRotation / 2;
                card.Position = new Vector2(
                     (float)((i + 0.5f) * _maxHandSize * _cardX / cards.Count - _maxHandSize * _cardX / 2 - 57.0f / 2 * Math.Cos(card.Rotation)),
                     (float)(-_cardY - 57.0f / 2 * Math.Sin(card.Rotation) + R * (1 - Math.Cos(card.Rotation)))
                );
            }
        }
    }

    private void AddCard(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget)
    {
        var card = cardScene.Instantiate<Card>();
        card.InitializeCard(cardType, cardName, cardDescription, cardCost, cardTarget);
        _hand.AddChild(card);
        UpdateCardPosition();
    }

    private void AddCard(ICard card)
    {
        var cardNode = cardScene.Instantiate<Card>();
        cardNode.InitializeCard(card.CardType, card.CardName, card.CardDescription, card.CardCost, card.CardTarget);
        _hand.AddChild(cardNode);
        UpdateCardPosition();
    }
}
