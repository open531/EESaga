namespace EESaga.Scripts.Cards.CardSpecials;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardStruggle : CardSpecial
{
	public static new CardStruggle Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_struggle.tscn").Instantiate<CardStruggle>();

	public override List<string> TakeEffect(List<BattlePiece> battlePieces)
	{
        var effectInfo = new List<string>();
        effectInfo.Add($"{Tr("T_USE")} {Tr("C_S_STRUGGLE")}\n");
        var struggleInfo = new string("");
		foreach (var piece in battlePieces)
		{
			var _health = piece.Health;
			if (piece.Health < 5)
				piece.Health = 1;
			else piece.Health -= 5;
            struggleInfo += $"{piece.PieceName} {Tr("PIECE_HEALTH")} {Tr("T_LOST")} {_health - piece.Health}\n";
			GD.Print($"{piece.Name} 失去了 {_health - piece.Health} 点生命");
			piece.Shield += 20;
            struggleInfo += $"{piece.PieceName} {Tr("PIECE_SHIELD")} {Tr("T_INCREASE")}20\n";
			GD.Print($"{piece.Name}护盾值增加了20");
            struggleInfo += $"{piece.PieceName} {Tr("PIECE_HEALTH")} : {piece.Health}\n";
		}
        effectInfo.Add(struggleInfo);
        return effectInfo;
	}
}
