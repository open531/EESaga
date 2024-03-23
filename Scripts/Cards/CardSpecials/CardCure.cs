namespace EESaga.Scripts.Cards.CardSpecials;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardCure : CardSpecial
{
	public static new CardCure Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_cure.tscn").Instantiate<CardCure>();

	public override void TakeEffect(List<BattlePiece> battlePieces)
	{
		foreach (var piece in battlePieces)
		{
			var healthRegeneration = (piece.HealthMax - piece.Health) / 3;
			if (healthRegeneration == 0 && piece.Health != piece.HealthMax) healthRegeneration = 1;
			piece.Health += healthRegeneration;
            GD.Print($"{piece.Name} 回复了 {healthRegeneration} 点生命");
		}
	}
}
