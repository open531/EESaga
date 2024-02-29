namespace EESaga.Scripts.Cards;

using Godot;

public partial class CardItem : Card
{
    public static new CardItem Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card_item.tscn").Instantiate<CardItem>();
}
