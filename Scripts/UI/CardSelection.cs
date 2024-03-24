namespace EESaga.Scripts.UI;

using Cards;
using Godot;
using System.Collections.Generic;

public partial class CardSelection : Panel
{
    private Label _label;
    private Control _cards;
    private CardDetail _cardDetail;

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

    private const int _cardMargin = 120;
    private const float _cardWidth = 57.0f;
    private const float _cardHeight = 88.0f;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _cards = GetNode<Control>("Cards");
        _cardDetail = GetNode<CardDetail>("CardDetail");

        var cardList = new List<CardInfo>() { CardData.CAStrike, CardData.CDDefend, CardData.CSStruggle, CardData.CIECS };
        ShowCards(cardList);
    }

    private void ShowCards(List<CardInfo> cardInfos)
    {
        foreach (var cardInfo in cardInfos)
        {
            var card = CardFactory.CreateCard(cardInfo);
            card.MouseEntered += () =>
            {
                SelectedCard = card;
                PreviewCard(card);
            };
            _cards.AddChild(card);
        }
        var cards = _cards.GetChildren();
        var distance = (640 - _cardWidth - 2 * _cardMargin) / (cards.Count - 1);
        for (var i = 0; i < cards.Count; i++)
        {
            var card = cards[i] as Card;
            card.Position = new Vector2(_cardMargin + i * distance, (360 - _cardHeight) / 2);
        }

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
        var newCard = CardFactory.CreateCard(card.CardInfo);
        newCard.Name = "PreviewCard";
        newCard.Position = card.Position;
        newCard.Rotation = card.Rotation;
        newCard.Scale = card.Scale;
        newCard.Parent = card;
        //newCard.GuiInput += OperateCard;
        newCard.MouseExited += ExitPreviewCard;
        AddChild(newCard);
        var tweenPosition = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position",
            new Vector2(card.Position.X - _cardWidth / 2, card.Position.Y - _cardHeight / 2), 0.05);
        tweenScale.TweenProperty(newCard, "scale", Vector2.One * 2, 0.05);
        card.Visible = false;
    }

    private void ExitPreviewCard()
    {
        var newCard = GetNodeOrNull("PreviewCard") as Card;
        SelectedCard = null;
        var tweenPosition = CreateTween();
        var tweenScale = CreateTween();
        tweenPosition.TweenProperty(newCard, "position", newCard.Parent.Position, 0.05);
        tweenScale.TweenProperty(newCard, "scale", newCard.Parent.Scale, 0.05);
        tweenScale.TweenCallback(Callable.From(newCard.Free));
        newCard.Parent.Visible = true;
    }
}
