namespace EESaga.Scripts.Maps;

using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Interfaces;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Utilities;

public partial class PieceBattle : Node2D
{
    public IsometricTileMap TileMap { get; set; }
    public List<IBattlePiece> Pieces { get; set; }
    public List<IBattleParty> Parties { get; set; }
    public List<IBattleEnemy> Enemies { get; set; }
    public List<Obstacle> Obstacles { get; set; }
    public List<Trap> Traps { get; set; }

    private Room _room;

    private static readonly PackedScene _battlePartyScene = GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/battle_party.tscn");
    private static readonly PackedScene _playerBattleScene = GD.Load<PackedScene>("res://Scenes/Entities/BattleParties/player_battle.tscn");
    private static readonly PackedScene _battleEnemyScene = GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/battle_enemy.tscn");

    public static PieceBattle Instance() => GD.Load<PackedScene>("res://Scenes/Maps/piece_battle.tscn").Instantiate<PieceBattle>();

    public override void _Ready()
    {
    }

    public void Initialize(Room room)
    {
        _room = room;
        room.RemoveChild(room.TileMap);
        AddChild(room.TileMap);
        //foreach (var party in GameData.Parties)
        //{
        //    var battleParty = party.PartyName switch
        //    {
        //        var partyName when partyName == GameData.Player.PlayerName => _playerBattleScene.Instance<PlayerBattle>(),
        //        _ => _battlePartyScene.Instance<BattleParty>()
        //    };
        //    battleParty.Initialize(party);
        //    battleParty.Name = "BattleParty" + battleParty.PartyName;
        //    //TileMap.Parties.AddChild(battleParty);
        //    Parties.Add(battleParty);
        //}

        //// TODO: Load Enemy from Room?

        //foreach (var party in Parties)
        //{
        //    Pieces.Add(party);
        //}
        //foreach (var enemy in Enemies)
        //{
        //    Pieces.Add(enemy);
        //}

        //Parties.Sort((x, y) => x.Agility - y.Agility);
        //Enemies.Sort((x, y) => x.Agility - y.Agility);
        //Pieces.Sort((x, y) => x.Agility - y.Agility);
    }
}
