namespace EESaga.Scripts.Cards;

using EESaga.Scripts.Entities.BattleEnemies;
using EESaga.Scripts.Entities.BattleParties;
using Entities;
using Godot;
using System.Collections.Generic;

public partial class Card : Control, ICard
{
    private TextureRect _background;
    private TextureRect _image;
    private Label _name;
    private Label _cost;

    public CardInfo CardInfo { get; set; }

    public CardType CardType { get; set; }
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public int CardCost { get; set; }
    public CardTarget CardTarget { get; set; }
    public int CardRange { get; set; }

    public Card Parent { get; set; }

    public static Card Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card.tscn").Instantiate<Card>();

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

    public virtual void InitializeCard(CardInfo cardInfo)
    {
        CardInfo = cardInfo;
        CardType = cardInfo.CardType;
        CardName = cardInfo.CardName;
        CardDescription = cardInfo.CardDescription;
        CardCost = cardInfo.CardCost;
        CardTarget = cardInfo.CardTarget;
        CardRange = cardInfo.CardRange;
    }

    public virtual void SetCard(CardType cardType, string cardName, string cardDescription, int cardCost, CardTarget cardTarget, int cardRange)
    {
        CardType = cardType;
        CardName = cardName;
        CardDescription = cardDescription;
        CardCost = cardCost;
        CardTarget = cardTarget;
        CardRange = cardRange;
    }

    public virtual void TakeEffect(List<BattlePiece> battlePieces) { }

    public virtual bool TakePartyCost(BattleParty battleParty)
    {
        if (battleParty.Energy >= CardCost)
        {
            battleParty.Energy -= CardCost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool TakeEnemyCost(BattleEnemy battleEnemy)
    {
        return false;
    }
}
