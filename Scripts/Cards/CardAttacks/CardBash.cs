namespace EESaga.Scripts.Cards.CardAttacks;

using EESaga.Scripts.Entities;
using EESaga.Scripts.Entities.BattleEnemies;
using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.Maps;
using Godot;
using System.Collections.Generic;

public partial class CardBash : CardAttack
{

    public static new CardBash Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardAttacks/card_bash.tscn").Instantiate<CardBash>();

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
            if(piece is BattleEnemy enemy)
            {
                enemy.SleepTurns = 1;
            }
            var damageList = new List<List<int>>();
            var damageInfo = new string("");
            var actionInfo = new string($"{Tr("T_USE")} {Tr("C_A_BASH")}\n");
            for (var i = 0; i < AttackTimes; i++)
            {
                damageList.Add(piece.BeAttacked(AttackDamage));
            }
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
            effectInfo.Add(actionInfo);
            effectInfo.Add(damageInfo);
        }
        return effectInfo;
    }
}
