namespace EESaga.Scripts.UI;

using Godot;
using Interfaces;

public partial class Card : Control, ICard
{
    private TextureRect _background;

    [Export] public CardType CardType { get; set; }
    [Export] public string CardName { get; set; }
    [Export] public string CardDescription { get; set; }
    [Export] public int CardCost { get; set; }
    [Export] public CardTarget CardTarget { get; set; }


    public override void _Ready()
    {
        _background = GetNode<TextureRect>("Background");

        _background.Texture = CardType switch
        {
            CardType.Null => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_white.png"),
            CardType.Attack => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_red.png"),
            CardType.Defense => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_blue.png"),
            CardType.Special => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_green.png"),
            CardType.Item => GD.Load<Texture2D>("res://Assets/Textures/Cards/card_yellow.png"),
            _ => _background.Texture
        };
    }

    public void InitializeCard(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget)
    {
        CardType = cardType;
        CardName = cardName;
        CardDescription = cardDescription;
        CardCost = cardCost;
        CardTarget = cardTarget;
    }
}
