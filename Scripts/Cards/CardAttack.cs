namespace EESaga.Scripts.Cards;

using Godot;

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
}
