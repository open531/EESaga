namespace EESaga.Scripts.Cards.CardItems;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardUST : CardItem
{
    public int CardNumMAX = 6;
    public static new CardUST Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardItems/card_ust.tscn").Instantiate<CardUST>();

    [Signal] public delegate void USTGetCardsEventHandler();
    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>();
        var actionInfo = new string($"{Tr("T_USE")} {Tr("C_I_UST")}\n");
        var cardInfo = new string($"{Tr("T_NOW")} {CardNumMAX} {Tr("T_CARD")}\n");
        EmitSignal(SignalName.USTGetCards);
        effectInfo.Add(actionInfo);
        effectInfo.Add(cardInfo);
        return effectInfo;
    }
}
