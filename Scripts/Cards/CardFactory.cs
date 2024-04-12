namespace EESaga.Scripts.Cards;

using Cards.CardAttacks;
using Cards.CardDefenses;
using Cards.CardItems;
using Cards.CardSpecials;

public static class CardFactory
{
    public static Card CreateCard(CardInfo cardInfo)
    {
        Card card = cardInfo.CardType switch
        {
            CardType.Attack => cardInfo.AttackType switch
            {
                CardAttackType.Bash => CardBash.Instance(),
                CardAttackType.DoubleBeat => CardDoubleBeat.Instance(),
                _ => CardAttack.Instance(),
            },
            CardType.Defense => cardInfo.DefenseType switch
            {
                CardDefenseType.Consolidate => CardConsolidate.Instance(),
                _ => CardDefense.Instance(),
            },
            CardType.Special => cardInfo.SpecialType switch
            {
                CardSpecialType.Cure => CardCure.Instance(),
                CardSpecialType.Survive => CardSurvive.Instance(),    
                CardSpecialType.AttackByDefense => CardAttackByDefense.Instance(),
                CardSpecialType.Fury => CardFury.Instance(),
                CardSpecialType.Struggle => CardStruggle.Instance(),
                CardSpecialType.CarRush => CardCarRush.Instance(),
                CardSpecialType.CodeBoom => CardCodeBoom.Instance(),
                _ => CardSpecial.Instance(),
            },
            CardType.Item => cardInfo.ItemType switch
            {
                CardItemType.Ecs => CardEcs.Instance(),
                CardItemType.UST => CardUST.Instance(),
                _ => Card.Instance(),
            },
            _ => Card.Instance(),
        };

        card.InitializeCard(cardInfo);
        return card;
    }
}
