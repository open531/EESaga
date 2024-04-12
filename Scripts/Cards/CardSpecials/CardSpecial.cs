namespace EESaga.Scripts.Cards.CardSpecials;

using Godot;

public partial class CardSpecial : Card
{
	public static new CardSpecial Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_special.tscn").Instantiate<CardSpecial>();
}
