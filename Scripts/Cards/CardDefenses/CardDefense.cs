namespace EESaga.Scripts.Cards.CardDefenses;

using EESaga.Scripts.Entities;
using Godot;
using System.Collections.Generic;

public partial class CardDefense : Card
{
    public int DefenseValue { get; set; }

    public static new CardDefense Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardDefenses/card_defense.tscn").Instantiate<CardDefense>();

    public override void InitializeCard(CardInfo cardInfo)
    {
        base.InitializeCard(cardInfo);
        DefenseValue = cardInfo.DefenseValue;
    }

    public override void TakeEffect(List<BattlePiece> battlePieces)
    {
        foreach (var piece in battlePieces)
        {
            piece.Shield += DefenseValue;
            GD.Print($"{piece.Name}护盾值增加了{DefenseValue}");
        }
    }
}
