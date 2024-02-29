namespace EESaga.Scripts.Cards;

using Godot;

public partial class CardDefense : Card
{
    public int DefenseValue { get; set; }

    public static new CardDefense Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card_defense.tscn").Instantiate<CardDefense>();

    public override void InitializeCard(CardInfo cardInfo)
    {
        base.InitializeCard(cardInfo);
        DefenseValue = cardInfo.DefenseValue;
    }
}
