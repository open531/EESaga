namespace EESaga.Scripts.Cards;


public interface ICard
{
    public CardType CardType { get; set; }
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public int CardCost { get; set; }
    public CardTarget CardTarget { get; set; }
    public int CardRange { get; set; }
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
    Range,
    Ally,
    AllEnemies,
    AllAllies,
    All,
}

public enum CardAttackType
{
    General,
    DoubleBeat,
    Bash,
}

public enum CardDefenseType
{
    General,
    Consolidate,
}

public enum CardItemType
{
    General,
    Ecs,
}
public enum CardSpecialType
{
    General,
    Cure,
    Struggle,
    AttackByDefense,
}

public class CardInfo(
    CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget, int cardRange = 1,
    int attackDamage = 0, int attackTimes = 0,
    int defenseValue = 0,
    CardAttackType attackType = CardAttackType.General,
    CardDefenseType defenseType = CardDefenseType.General,
    CardSpecialType specialType = CardSpecialType.General,
    CardItemType itemType = CardItemType.General
    ) : ICard
{
    #region Card
    public CardType CardType { get; set; } = cardType;
    public string CardName { get; set; } = cardName;
    public string CardDescription { get; set; } = cardDescription;
    public int CardCost { get; set; } = cardCost;
    public CardTarget CardTarget { get; set; } = cardTarget;
    public int CardRange { get; set; } = cardRange;
    public bool NeedTarget => CardTarget == CardTarget.Enemy || CardTarget == CardTarget.Ally;
    #endregion

    #region Attack
    public int AttackDamage { get; set; } = attackDamage;
    public int AttackTimes { get; set; } = attackTimes;
    #endregion

    #region Defense
    public int DefenseValue { get; set; } = defenseValue;
    #endregion

    #region Special
    public CardSpecialType SpecialType { get; set; } = specialType;
    #endregion

    #region Item
    public CardItemType ItemType { get; set; } = itemType;
    public CardAttackType AttackType { get; set; } = attackType;
    public CardDefenseType DefenseType { get; set; } = defenseType;
    #endregion
}

public static class CardData
{
    public static CardInfo CAStrike = new(CardType.Attack, "C_A_STRIKE", "C_A_STRIKE_DESC", 1, CardTarget.Enemy, 1, attackDamage: 6, attackTimes: 1);
    public static CardInfo CABash = new(CardType.Attack, "C_A_BASH", "C_A_BASH_DESC", 1, CardTarget.Enemy, 1, attackDamage: 6, attackTimes: 1, attackType: CardAttackType.Bash);
    public static CardInfo CADoubleBeat = new(CardType.Attack, "C_A_DOUBLE_BEAT", "C_A_DOUBLE_BEAT_DESC", 1, CardTarget.Enemy, 1, attackDamage: 4, attackTimes: 2, attackType: CardAttackType.DoubleBeat);
    public static CardInfo CDDefend = new(CardType.Defense, "C_D_DEFEND", "C_D_DEFEND_DESC", 1, CardTarget.Self, defenseValue: 3);
    public static CardInfo CDConsolidate = new(CardType.Defense, "C_D_CONSOLIDATE", "C_D_CONSOLIDATE_DESC", 1, CardTarget.Self, defenseValue: 0, defenseType: CardDefenseType.Consolidate);
    public static CardInfo CSStruggle = new(CardType.Special, "C_S_STRUGGLE", "C_S_STRUGGLE_DESC", 1, CardTarget.Self, specialType: CardSpecialType.Struggle);
    public static CardInfo CSShieldStrike = new(CardType.Special, "C_S_SHIELD_STRIKE", "C_S_SHIELD_STRIKE_DESC", 1, CardTarget.Enemy, specialType: CardSpecialType.AttackByDefense);
    public static CardInfo CIECS = new(CardType.Item, "C_I_ECS", "C_I_ECS_DESC", 1, CardTarget.Self, itemType: CardItemType.Ecs);
    public static CardInfo CSCure = new(CardType.Special, "C_S_CURE", "C_S_CURE_DESC", 1, CardTarget.Self, specialType: CardSpecialType.Cure);
}
