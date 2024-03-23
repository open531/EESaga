namespace EESaga.Scripts.Cards.CardItems;

using Godot;

public partial class CardItem : Card
{
    public static new CardItem Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardItems/card_item.tscn").Instantiate<CardItem>();
}
