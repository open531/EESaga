namespace EESaga.Scripts.Cards.CardSpecials;

using EESaga.Scripts.Entities.BattleParties;
using Entities;
using Godot;
using System.Collections.Generic;
public partial class CardFury : CardSpecial
{
    public static new CardFury Instance() => GD.Load<PackedScene>("res://Scenes/Cards/CardSpecials/card_fury.tscn").Instantiate<CardFury>();

    public override List<string> TakeEffect(List<BattlePiece> battlePieces)
    {
        var effectInfo = new List<string>();
        effectInfo.Add($"{Tr("T_USE")} {Tr("C_S_FURY")}\n");
        var furyInfo = new string("");
        foreach (var piece in battlePieces)
        {
            var _health = piece.Health;
            var party = piece as BattleParty;
            if (piece.Health <= 10)
                piece.Health = 1;
            else piece.Health -= 10;
            furyInfo += $"{piece.PieceName} {Tr("PIECE_HEALTH")} {Tr("T_LOST")} {_health - piece.Health}\n";
            GD.Print($"{piece.Name} 失去了 {_health - piece.Health} 点生命");
            var _energy = party.Energy;
            party.Energy += 2;
            party.Energy = Mathf.Min(party.Energy, party.EnergyMax);
            furyInfo += $"{piece.PieceName} {Tr("T_ENERGY")} {Tr("T_INCREASE")} {party.Energy-_energy}\n";
            GD.Print($"{piece.Name}能量增加了20");
            furyInfo += $"{piece.PieceName} {Tr("PIECE_HEALTH")} : {piece.Health}\n";
        }
        effectInfo.Add(furyInfo);
        return effectInfo;
    }
}
