namespace EESaga.Scripts.Cards;

using EESaga.Scripts.Entities;
using EESaga.Scripts.Maps;
using Godot;
using System.Collections.Generic;

public partial class CardSpecial : Card
{
    public static new CardSpecial Instance() => GD.Load<PackedScene>("res://Scenes/Cards/card_special.tscn").Instantiate<CardSpecial>();
}
