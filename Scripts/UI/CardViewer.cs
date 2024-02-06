namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;
using System.Collections.Generic;

public partial class CardViewer : PopupPanel
{
    private MarginContainer _marginContainer;
    private ScrollContainer _scrollContainer;
    private GridContainer _gridContainer;

    private static readonly PackedScene _cardScene = GD.Load<PackedScene>("res://Scenes/UI/card.tscn");

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
        foreach (var cardInfo in cards)
        {
            var card = _cardScene.Instantiate<Card>();
            card.InitializeCard(cardInfo);
            _gridContainer.AddChild(card);
        }
        PopupCentered();
    }
}
