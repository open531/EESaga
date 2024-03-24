namespace EESaga.Scripts.Cards.CardSpecials;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardCure : CardSpecial
{
	public static new CardCure Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_cure.tscn").Instantiate<CardCure>();

	public override List<string> TakeEffect(List<BattlePiece> battlePieces)
	{
        var effectInfo = new List<string>();
        effectInfo.Add($"{Tr("T_USE")}{Tr("C_S_CURE")}\n");
        var cureInfo = new string("");
		foreach (var piece in battlePieces)
		{
			var healthRegeneration = (piece.HealthMax - piece.Health) / 3;
			if (healthRegeneration == 0 && piece.Health != piece.HealthMax) healthRegeneration = 1;
			piece.Health += healthRegeneration;
            GD.Print($"{piece.PieceName} 回复了 {healthRegeneration} 点生命");
            cureInfo += $"{piece.PieceName}{Tr("PIECE_HEALTH")}{Tr("T_RESTORE")}{healthRegeneration}\n";
            cureInfo += $"{piece.PieceName}{Tr("PIECE_HEALTH")} : {piece.Health}\n";
		}
        effectInfo.Add(cureInfo);
        return effectInfo;
    }
}
