namespace EESaga.Scripts.Cards;

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
                _ => CardSpecial.Instance(),
            },
            CardType.Item => CardItem.Instance(),
            _ => Card.Instance(),
        };

        card.InitializeCard(cardInfo);
        return card;
    }
}
