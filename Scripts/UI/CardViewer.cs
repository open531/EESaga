namespace EESaga.Scripts.UI;

using Cards;
using Godot;
using System.Collections.Generic;

public partial class CardViewer : PopupPanel
{
    private MarginContainer _marginContainer;
    private ScrollContainer _scrollContainer;
    private GridContainer _gridContainer;

    public static CardViewer Instance() => GD.Load<PackedScene>("res://Scenes/UI/card_viewer.tscn").Instantiate<CardViewer>();

    public override void _Ready()
    {
        _marginContainer = GetNode<MarginContainer>("%MarginContainer");
        _scrollContainer = GetNode<ScrollContainer>("%ScrollContainer");
        _gridContainer = GetNode<GridContainer>("%GridContainer");
    }

    public void DisplayCards(List<CardInfo> cards)
    {
        var currentCards = _gridContainer.GetChildren();
        foreach (var card in currentCards)
        {
            card.QueueFree();
        }
        if (cards != null)
        {
            foreach (var cardInfo in cards)
            {
                var card = Card.Instance();
                card.InitializeCard(cardInfo);
                _gridContainer.AddChild(card);
            }
        }
        PopupCentered();
    }
}
