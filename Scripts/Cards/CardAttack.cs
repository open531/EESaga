namespace EESaga.Scripts.Cards;

using EESaga.Scripts.Entities;
using EESaga.Scripts.Maps;
using Godot;
using System.Collections.Generic;

public partial class CardAttack : Card
{
    public int AttackDamage { get; set; }
    public int AttackTimes { get; set; }

    public static new CardAttack Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card_attack.tscn").Instantiate<CardAttack>();

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
                var dif = -piece.Shield + AttackDamage;
                var shield = piece.Shield;
                if (piece.Shield != 0)
                {
                    piece.Shield -= AttackDamage;
                }
                if (piece.Shield <= 0)
                {
                    piece.Shield = 0;
                    piece.Health -= dif;
                    GD.Print($"{piece.Name} 护盾值削减了 {shield} 点");
                    GD.Print($"{piece.Name} 受到了 {dif} 点伤害");
                }
                else
                {
                    GD.Print($"{piece.Name} 护盾值削减了 {AttackDamage} 点");
                }
                if (piece.Health == 0)
                {
                    GD.Print($"{piece.Name} 死了");
                    break;
                }
            }
        }
    }
}
