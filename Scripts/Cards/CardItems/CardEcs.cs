namespace EESaga.Scripts.Cards.CardItems;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardEcs : CardItem
{
	public int CardNum = 2;
	public static new CardEcs Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardItems/card_ecs.tscn").Instantiate<CardEcs>();

	[Signal] public delegate void EcsGetCardsEventHandler();
	public override void TakeEffect(List<BattlePiece> battlePieces)
	{
		EmitSignal(SignalName.EcsGetCards);
	}
}
