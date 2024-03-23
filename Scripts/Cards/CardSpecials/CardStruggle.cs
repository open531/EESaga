namespace EESaga.Scripts.Cards.CardSpecials;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardStruggle : CardSpecial
{
	public static new CardStruggle Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_struggle.tscn").Instantiate<CardStruggle>();

	public override void TakeEffect(List<BattlePiece> battlePieces)
	{
		foreach (var piece in battlePieces)
		{
			var _health = piece.Health;
			if (piece.Health < 5)
				piece.Health = 1;
			else piece.Health -= 5;
			GD.Print($"{piece.Name} 失去了 {_health - piece.Health} 点生命");
			piece.Shield += 20;
			GD.Print($"{piece.Name}护盾值增加了20");
		}
	}
}
