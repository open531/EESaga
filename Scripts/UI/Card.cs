namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;

public partial class Card : Control, ICard
{
    private TextureRect _background;
    private TextureRect _image;
    private Label _name;
    private Label _cost;

    [Export] public CardType CardType { get; set; }
    [Export] public string CardName { get; set; }
    [Export] public string CardDescription { get; set; }
    [Export] public int CardCost { get; set; }
    [Export] public CardTarget CardTarget { get; set; }

    public Card Parent { get; set; }

    public static Card Instance() => GD.Load<PackedScene>("res://Scenes/UI/card.tscn").Instantiate<Card>();

    public override void _Ready()
    {
        _background = GetNode<TextureRect>("Background");
        _image = GetNode<TextureRect>("Image");
        _name = GetNode<Label>("Name");
        _cost = GetNode<Label>("Cost");

        _background.Texture = CardType switch
        {
            CardType.Null => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_white.png"),
            CardType.Attack => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_red.png"),
            CardType.Defense => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_blue.png"),
            CardType.Special => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_green.png"),
            CardType.Item => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_yellow.png"),
            _ => _background.Texture
        };
        _name.Text = CardName;
        _cost.Text = CardCost.ToString();
    }

    public void InitializeCard(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget)
    {
        CardType = cardType;
        CardName = cardName;
        CardDescription = cardDescription;
        CardCost = cardCost;
        CardTarget = cardTarget;
    }

    public void InitializeCard(CardInfo cardInfo)
    {
        CardType = cardInfo.CardType;
        CardName = cardInfo.CardName;
        CardDescription = cardInfo.CardDescription;
        CardCost = cardInfo.CardCost;
        CardTarget = cardInfo.CardTarget;
    }

    public void TakeEffect(IEntity entity = null)
    {
        switch (CardTarget)
        {
            case CardTarget.Self:
                break;
            case CardTarget.Enemy:
                break;
            case CardTarget.Ally:
                break;
            case CardTarget.AllEnemies:
                break;
            case CardTarget.AllAllies:
                break;
            case CardTarget.All:
                break;
            default:
                break;
        }
    }
}
