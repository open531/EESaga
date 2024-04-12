namespace EESaga.Scripts.Cards.CardAttacks;

using EESaga.Scripts.Entities;
using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.Maps;
using Godot;
using System.Collections.Generic;

public partial class CardAttack : Card
{
    public int AttackDamage { get; set; }
    public int AttackTimes { get; set; }

    public static new CardAttack Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardAttacks/card_attack.tscn").Instantiate<CardAttack>();

    public override void InitializeCard(CardInfo cardInfo)
    {
        base.InitializeCard(cardInfo);
        AttackDamage = cardInfo.AttackDamage;
        AttackTimes = cardInfo.AttackTimes;
    }

    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>();
        foreach (var piece in battlePieces)
        {
            var damageList = new List<List<int>>();
            var damageInfo = new string("");
            var actionInfo = new string($"{Tr("T_USE")} {Tr("T_ATTACK")}\n");
            var deathInfo = new bool();
            for (var i = 0; i < AttackTimes; i++)
            {
                damageList.Add(piece.BeAttacked(AttackDamage));
            }
            deathInfo = piece.CheckDeath();
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
            effectInfo.Add(actionInfo);
            effectInfo.Add(damageInfo);
        }
        return effectInfo;
    }
}
