namespace EESaga.Scripts.Cards.CardDefenses;

using EESaga.Scripts.Entities;
using Godot;
using System.Collections.Generic;

public partial class CardConsolidate : CardDefense
{
    public static new CardConsolidate Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardDefenses/card_consolidate.tscn").Instantiate<CardConsolidate>();

    public override void InitializeCard(CardInfo cardInfo)
    {
        base.InitializeCard(cardInfo);
    }

    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>();
        effectInfo.Add($"{Tr("T_USE")} {Tr("C_D_CONSOLIDATE")}\n");
        var defenseInfo = new string("");
        foreach (var piece in battlePieces)
        {
            var currentShield = piece.Shield;
            piece.Shield += currentShield;
            GD.Print($"{piece.Name}护盾值增加了{currentShield}");
            defenseInfo += $"{piece.PieceName} {Tr("PIECE_SHIELD")} {Tr("T_INCREASE")} {currentShield}\n";
        }
        effectInfo.Add(defenseInfo);
        return effectInfo;
    }
}
