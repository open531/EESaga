namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;

public partial class CardDetail : Control
{
    private Label _cardName;
    private Label _cardDescription;

    public static CardDetail Instance() => GD.Load<PackedScene>("res://Scenes/UI/card_detail.tscn").Instantiate<CardDetail>();

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
                card.CardType switch
                {
                    CardType.Null => Tr("CARD_NULL"),
                    CardType.Attack => Tr("CARD_ATTACK"),
                    CardType.Defense => Tr("CARD_DEFENSE"),
                    CardType.Special => Tr("CARD_SPECIAL"),
                    CardType.Item => Tr("CARD_ITEM"),
                    _ => Tr("CARD_NULL")
                } + ")";
            _cardDescription.Text = Tr(card.CardDescription);
        }
    }
}
