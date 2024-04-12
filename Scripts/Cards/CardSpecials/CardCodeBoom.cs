using EESaga.Scripts.Cards.CardSpecials;
using EESaga.Scripts.Entities;
using Godot;
using System;
using System.Collections.Generic;

public partial class CardCodeBoom : CardSpecial
{
    public static int attackDamage = 8;
    public static new CardCodeBoom Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_code_boom.tscn").Instantiate<CardCodeBoom>();

    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>
        {
            $"{Tr("T_USE")} {Tr("C_S_CODE_BOOM")}\n"
        };
        var damageList = new List<List<int>>();
        var damageInfo = new string("");
        foreach (var piece in battlePieces)
        {
            damageList.Add(piece.BeAttacked(attackDamage));
            var deathInfo = piece.CheckDeath();
            foreach (var damage in damageList)
            {
                damageInfo += damage[0] == 0
                    ? ""
                    : $"{Tr(piece.PieceName)} {Tr("PIECE_SHIELD")} {Tr("T_LOST")} {damage[0]}\n";
                damageInfo += damage[1] == 0
                    ? ""
                    : $"{Tr(piece.PieceName)} {Tr("PIECE_HEALTH")} {Tr("T_REDUCE")} {damage[1]}\n";
                damageInfo += damage[1] == 0 && damage[0] == 0
                    ? $"{Tr("T_NO_EFFECT")}\n"
                    : $"{Tr(piece.PieceName)} {Tr("PIECE_HEALTH")} : {damage[2]}\n";
            }

            if (deathInfo)
            {
                damageInfo += $"{Tr(piece.PieceName)} {Tr("T_DECEASED")}\n";
            }
        }
        effectInfo.Add(damageInfo);
        return effectInfo;
    }
}
