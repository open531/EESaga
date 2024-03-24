namespace EESaga.Scripts.Cards.CardItems;

using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardEcs : CardItem
{
    public int CardNum = 2;
    public static new CardEcs Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardItems/card_ecs.tscn").Instantiate<CardEcs>();

    [Signal] public delegate void EcsGetCardsEventHandler();
    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>();
        var actionInfo = new string($"{Tr("T_USE")}{Tr("C_I_ECS")}\n");
        var cardInfo = new string($"{Tr("T_GET")} {CardNum} {Tr("T_CARD")}\n");
        EmitSignal(SignalName.EcsGetCards);
        effectInfo.Add(actionInfo);
        effectInfo.Add(cardInfo);
        return effectInfo;
    }
}
