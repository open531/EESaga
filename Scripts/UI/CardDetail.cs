namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;

public partial class CardDetail : Control
{
    private Label _cardName;
    private Label _cardCost;
    private Label _cardType;
    private Label _cardDescription;

    public override void _Ready()
    {
        _cardName = GetNode<Label>("%CardName");
        _cardCost = GetNode<Label>("%CardCost");
        _cardType = GetNode<Label>("%CardType");
        _cardDescription = GetNode<Label>("%CardDescription");

        _cardName.Text = "";
        _cardCost.Text = "";
        _cardType.Text = "";
        _cardDescription.Text = "";
    }

    public void Update(Card card)
    {
        if (card == null)
        {
            _cardName.Text = "";
            _cardCost.Text = "";
            _cardType.Text = "";
            _cardDescription.Text = "";
        }
        else
        {
            _cardName.Text = card.CardName;
            _cardCost.Text = card.CardCost.ToString();
            _cardType.Text = card.CardType switch
            {
                CardType.Null => "CARD_NULL",
                CardType.Attack => "CARD_ATTACK",
                CardType.Defense => "CARD_DEFENSE",
                CardType.Special => "CARD_SPECIAL",
                CardType.Item => "CARD_ITEM",
                _ => "CARD_NULL"
            };
            _cardDescription.Text = card.CardDescription;
        }
    }
}
