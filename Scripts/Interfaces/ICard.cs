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

public class CardInfo : ICard
{
    public CardType CardType { get; set; }
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public int CardCost { get; set; }
    public CardTarget CardTarget { get; set; }
    public bool NeedTarget => CardTarget == CardTarget.Enemy || CardTarget == CardTarget.Ally;
    public CardInfo(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget)
    {
        CardType = cardType;
        CardName = cardName;
        CardDescription = cardDescription;
        CardCost = cardCost;
        CardTarget = cardTarget;
    }
}