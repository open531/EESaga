namespace EESaga.Scripts.Cards;

using Godot;

public partial class CardSpecial : Card
{
    public static new CardSpecial Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card_special.tscn").Instantiate<CardSpecial>();
}
