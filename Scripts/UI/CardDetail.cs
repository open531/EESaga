namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;

public partial class CardDetail : Control
{
    private Label _cardName;
    private Label _cardDescription;

    public override void _Ready()
    {
        _cardName = GetNode<Label>("%CardName");
        _cardDescription = GetNode<Label>("%CardDescription");

        _cardName.Text = "";
        _cardDescription.Text = "";
    }

    public void Update(Card card)
    {
        if (card == null)
        {
            _cardName.Text = "";
            _cardDescription.Text = "";
        }
        else
        {
            _cardName.Text = Tr(card.CardName) +
                "(" + card.CardCost.ToString() + "," +
                Tr(card.CardType switch
                {
                    CardType.Null => "CARD_NULL",
                    CardType.Attack => "CARD_ATTACK",
                    CardType.Defense => "CARD_DEFENSE",
                    CardType.Special => "CARD_SPECIAL",
                    CardType.Item => "CARD_ITEM",
                    _ => "CARD_NULL"
                }) + ")";
            _cardDescription.Text = Tr(card.CardDescription);
        }
    }
}
