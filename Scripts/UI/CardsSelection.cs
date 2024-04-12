using Godot;
using EESaga.Scripts.Data;
using System.Collections.Generic;
using EESaga.Scripts.Cards;
using EESaga.Scripts.Utilities;

public partial class CardsSelection : Node2D
{

    private bool _inArea0;
    private bool _inArea1;
    private bool _inArea2;
    private SceneSwitcher _sceneSwitcher;

    public static CardsSelection Instance() => GD.Load<PackedScene>("res://Scenes/UI/cards_selection.tscn").Instantiate<CardsSelection>();

    private List<Control> _cardsInstance = [];
    private List<CardInfo> _cardsInfo = [];

    public override void _Ready()
    {
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        var rng = new RandomNumberGenerator();
        for (int i = 0; i < 3; i++)
        {
            var rngNum = rng.RandiRange(0, 11);
            var cardInfo = rngNum switch
            {
                0 => CardData.CAStrike,
                1 => CardData.CABash,
                2 => CardData.CADoubleBeat,
                3 => CardData.CDDefend,
                4 => CardData.CDConsolidate,
                5 => CardData.CSSurvive,
                6 => CardData.CSStruggle,
                7 => CardData.CSShieldStrike,
                8 => CardData.CIECS,
                9 => CardData.CIUST,
                10 => CardData.CSCure,
                11 => CardData.CSFury
            };
            _cardsInstance.Add(CardFactory.CreateCard(cardInfo));
            _cardsInfo.Add(cardInfo);
        }

        for (int i = 0; i < _cardsInstance.Count; i++)
        {
            AddChild(_cardsInstance[i]);
        }

        _cardsInstance[0].SetPosition(new Vector2(122, 160));
        _cardsInstance[0].Scale = new Vector2I(2, 2);
        _cardsInstance[0].MouseEntered += OnMouseEnteredArea0;
        _cardsInstance[0].MouseExited += OnMouseExitedArea0;
        _cardsInstance[1].SetPosition(new Vector2(280, 160));
        _cardsInstance[1].Scale = new Vector2I(2, 2);
        _cardsInstance[1].MouseEntered += OnMouseEnteredArea1;
        _cardsInstance[1].MouseExited += OnMouseExitedArea1;
        _cardsInstance[2].SetPosition(new Vector2(438, 160));
        _cardsInstance[2].Scale = new Vector2I(2, 2);
        _cardsInstance[2].MouseEntered += OnMouseEnteredArea2;
        _cardsInstance[2].MouseExited += OnMouseExitedArea2;

        _inArea0 = false;
        _inArea1 = false;
        _inArea2 = false;
    }

    public void OnCardSelected(CardInfo cardInfo)
    {
        var card = cardInfo.CardType switch
        {
            CardType.Attack => cardInfo.AttackType switch
            {
                CardAttackType.Bash => CardData.CABash,
                CardAttackType.DoubleBeat => CardData.CADoubleBeat,
                _ => CardData.CAStrike,
            },
            CardType.Defense => cardInfo.DefenseType switch
            {
                CardDefenseType.Consolidate => CardData.CDConsolidate,
                _ => CardData.CDDefend,
            },
            CardType.Special => cardInfo.SpecialType switch
            {
                CardSpecialType.Cure => CardData.CSCure,
                CardSpecialType.Survive => CardData.CSSurvive,
                CardSpecialType.AttackByDefense => CardData.CSShieldStrike,
                CardSpecialType.Fury => CardData.CSFury,
                CardSpecialType.Struggle => CardData.CSStruggle,
                _ => CardData.CSSurvive,
            },
            CardType.Item => cardInfo.ItemType switch
            {
                CardItemType.Ecs => CardData.CIECS,
                CardItemType.UST => CardData.CIUST,
                _ => CardData.CIECS
            },
            _ => CardData.CAStrike,
        };
        SaveData.Player.BattleCards.DeckCards.Add(card);
        if (SaveData.Coder.Dancing)
        {
            SaveData.Coder.BattleCards.DeckCards.Add(card);
        }

        if (SaveData.HardwareWarrior.Dancing)
        {
            SaveData.HardwareWarrior.BattleCards.DeckCards.Add(card);
        }

        if (SaveData.SignalMaster.Dancing)
        {
            SaveData.SignalMaster.BattleCards.DeckCards.Add(card);
        }

        _sceneSwitcher.PushScene(SceneSwitcher.BattleManagerScene);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.Pressed)
                {
                    if (_inArea0)
                    {
                        OnCardSelected(_cardsInfo[0]);
                    }
                    else if (_inArea1)
                    {
                        OnCardSelected(_cardsInfo[1]);
                    }
                    else if (_inArea2)
                    {
                        OnCardSelected(_cardsInfo[2]);
                    }
                }
            }
        }
    }

    public void OnMouseEnteredArea0()
    {
        _inArea0 = true;
    }

    public void OnMouseExitedArea0()
    {
        _inArea0 = false;
    }

    public void OnMouseEnteredArea1()
    {
        _inArea1 = true;
    }

    public void OnMouseExitedArea1()
    {
        _inArea1 = false;
    }

    public void OnMouseEnteredArea2()
    {
        _inArea2 = true;
    }

    public void OnMouseExitedArea2()
    {
        _inArea2 = false;
    }
}
