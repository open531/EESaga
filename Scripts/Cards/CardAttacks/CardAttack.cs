namespace EESaga.Scripts.Cards.CardAttacks;

using EESaga.Scripts.Entities;
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

    public override void TakeEffect(List<BattlePiece> battlePieces)
    {
        foreach (var piece in battlePieces)
        {
            for (var i = 0; i < AttackTimes; i++)
            {
                piece.BeAttacked(AttackDamage);
            }
            piece.CheckDeath();
        }
    }
}
