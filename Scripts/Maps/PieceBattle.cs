namespace EESaga.Scripts.Maps;

using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using System.Collections.Generic;
using Utilities;

public partial class PieceBattle : Node2D
{
    public Room Room { get; set; }
    public List<IBattlePiece> Pieces { get; set; }
    public List<IBattleParty> Parties { get; set; }
    public List<IBattleEnemy> Enemies { get; set; }

    private GameData _gameData;
    private static readonly PackedScene _battlePartyScene = GD.Load<PackedScene>("res://Scenes/Entities/battle_party.tscn");
    private static readonly PackedScene _battleEnemyScene = GD.Load<PackedScene>("res://Scenes/Entities/battle_enemy.tscn");
    public override void _Ready()
    {
        _gameData = GD.Load<GameData>("/root/GameData");

        foreach (var party in _gameData.Parties)
        {
            var battleParty = _battlePartyScene.Instantiate<BattleParty>();
            battleParty.Initialize(party);
            battleParty.Name = "BattleParty" + battleParty.PartyName;
            Room.Parties.AddChild(battleParty);
            Parties.Add(battleParty);
        }

        // TODO: Load Enemy from Room?

        foreach (var party in Parties)
        {
            Pieces.Add(party);
        }
        foreach (var enemy in Enemies)
        {
            Pieces.Add(enemy);
        }

        Parties.Sort((x, y) => x.Agility - y.Agility);
        Enemies.Sort((x, y) => x.Agility - y.Agility);
        Pieces.Sort((x, y) => x.Agility - y.Agility);
    }
}
