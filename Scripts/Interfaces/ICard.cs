namespace EESaga.Scripts.Interfaces;

using Godot;

public interface ICard
{
    public CardType CardType { get; set; }
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public int CardCost { get; set; }
    public CardTarget CardTarget { get; set; }
    public bool NeedTarget => CardTarget == CardTarget.Enemy || CardTarget == CardTarget.Ally;
}

public enum CardType
{
    Null,
    Attack,
    Defense,
    Special,
    Item,
}

public enum CardTarget
{
    Null,
    Self,
    Enemy,
    Ally,
    AllEnemies,
    AllAllies,
    All,
}

public struct CardInfo(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget) : ICard
{
    public CardType CardType { get; set; } = cardType;
    public string CardName { get; set; } = cardName;
    public string CardDescription { get; set; } = cardDescription;
    public int CardCost { get; set; } = cardCost;
    public CardTarget CardTarget { get; set; } = cardTarget;
    public readonly bool NeedTarget => CardTarget == CardTarget.Enemy || CardTarget == CardTarget.Ally;
}