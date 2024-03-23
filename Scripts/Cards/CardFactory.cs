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
            CardType.Attack => CardAttack.Instance(),
            CardType.Defense => CardDefense.Instance(),
            CardType.Special => cardInfo.SpecialType switch
            {
                CardSpecialType.Cure => CardCure.Instance(),
                CardSpecialType.Struggle => CardStruggle.Instance(),
                _ => CardSpecial.Instance(),
            },
            CardType.Item => cardInfo.ItemType switch
            {
                CardItemType.Ecs => CardEcs.Instance(),
                _ => Card.Instance(),
            },
            _ => Card.Instance(),
        };

        card.InitializeCard(cardInfo);
        return card;
    }
}
