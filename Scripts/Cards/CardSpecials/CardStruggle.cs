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
        return effectInfo;
    }
}
