namespace EESaga.Scripts.Cards.CardSpecials;

using EESaga.Scripts.Entities.BattleEnemies;
using EESaga.Scripts.Entities.BattleParties;
using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardAttackByDefense : CardSpecial
{
    public static new CardAttackByDefense Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_attack_by_defense.tscn").Instantiate<CardAttackByDefense>();

    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var attackDamage = new int();
        var piece = new BattlePiece();
        if (battlePieces[0] is BattleParty)
        {
            attackDamage = battlePieces[0].Shield;
            piece = battlePieces[1];
        }
        else
        {
            attackDamage = battlePieces[1].Shield;
            piece = battlePieces[0];
        }

        var effectInfo = new List<string>();
        effectInfo.Add($"{Tr("T_USE")} {Tr("C_S_SHIELD_STRIKE")}\n");

        var damageList = new List<List<int>>();
        var damageInfo = new string("");
        damageList.Add(piece.BeAttacked(attackDamage));
        var deathInfo = piece.CheckDeath();
        foreach (var damage in damageList)
        {
            damageInfo += damage[0] == 0 ? "" : $"{Tr(piece.PieceName)} {Tr("PIECE_SHIELD")} {Tr("T_LOST")} {damage[0]}\n";
            damageInfo += damage[1] == 0 ? "" : $"{Tr(piece.PieceName)} {Tr("PIECE_HEALTH")} {Tr("T_REDUCE")} {damage[1]}\n";
            damageInfo += damage[1] == 0 && damage[0] == 0 ? $"{Tr("T_NO_EFFECT")}\n" : $"{Tr(piece.PieceName)} {Tr("PIECE_HEALTH")} : {damage[2]}\n";
        }
        if (deathInfo)
        {
            damageInfo += $"{Tr(piece.PieceName)} {Tr("T_DECEASED")}\n";
        }
        effectInfo.Add(damageInfo);
        return effectInfo;
    }
}
