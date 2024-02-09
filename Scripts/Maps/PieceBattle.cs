namespace EESaga.Scripts.Maps;

using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Interfaces;
using System.Collections.Generic;

public partial class PieceBattle : Node2D
{
    public IsometricTileMap TileMap { get; set; }
    public List<IBattlePiece> Pieces { get; set; } = [];
    public List<IBattleParty> Parties { get; set; } = [];
    public List<IBattleEnemy> Enemies { get; set; } = [];
    public List<Obstacle> Obstacles { get; set; } = [];
    public List<Trap> Traps { get; set; } = [];

    public Dictionary<Vector2I, IBattlePiece> PieceMap { get; set; } = [];

    private Room _room;
    private Node2D _enemies;
    private Node2D _parties;
    private Camera2D _camera;

    public static PieceBattle Instance() => GD.Load<PackedScene>("res://Scenes/Maps/piece_battle.tscn").Instantiate<PieceBattle>();

    public override void _Ready()
    {
        TileMap = GetNode<IsometricTileMap>("TileMap");
        _enemies = GetNode<Node2D>("Enemies");
        _parties = GetNode<Node2D>("Parties");
        _camera = GetNode<Camera2D>("Camera2D");

        #region test
        var tileMap = IsometricTileMap.Instance();
        for (var x = 0; x < 6; x++)
        {
            for (var y = 0; y < 6; y++)
            {
                tileMap.SetCell((int)Layer.Ground, new Vector2I(x, y), IsometricTileMap.TileSetId, IsometricTileMap.DefaultTileAtlas);
            }
        }
        Initialize(tileMap);
        AddEnemy(EnemyType.Slime);
        AddEnemy(EnemyType.Slime);
        AddEnemy(EnemyType.Slime);
        AddParty(PartyType.Player);
        #endregion
    }

    public void Initialize(IsometricTileMap tileMap)
    {
        TileMap.CopyFrom(tileMap);
        foreach (var cell in TileMap.AvailableCells)
        {
            PieceMap[cell] = null;
        }
        var rectCenter = TileMap.GetUsedRect().GetCenter();
        _camera.GlobalPosition = TileMap.GlobalPosition +
            new Vector2(24 * (rectCenter.X - rectCenter.Y) + 12,
            12 * (rectCenter.X + rectCenter.Y));
        _camera.Enabled = true;
    }

    public void AddEnemy(EnemyType enemyType)
    {
        var enemy = enemyType switch
        {
            EnemyType.Slime => Slime.Instance(),
            _ => BattleEnemy.Instance(),
        };
        _enemies.AddChild(enemy);
        var rng = new RandomNumberGenerator();
        var cell = TileMap.AvailableCells[rng.RandiRange(0, TileMap.AvailableCells.Count - 1)];
        while (PieceMap[cell] != null)
        {
            cell = TileMap.AvailableCells[rng.RandiRange(0, TileMap.AvailableCells.Count - 1)];
        }
        enemy.GlobalPosition = PosForPiece(cell);
        PieceMap[cell] = enemy;
        Enemies.Add(enemy);
    }

    public void AddParty(PartyType partyType)
    {
        var party = partyType switch
        {
            PartyType.Player => PlayerBattle.Instance(),
            _ => BattleParty.Instance(),
        };
        _parties.AddChild(party);
        var rng = new RandomNumberGenerator();
        var cell = TileMap.AvailableCells[rng.RandiRange(0, TileMap.AvailableCells.Count - 1)];
        while (PieceMap[cell] != null)
        {
            cell = TileMap.AvailableCells[rng.RandiRange(0, TileMap.AvailableCells.Count - 1)];
        }
        party.GlobalPosition = PosForPiece(cell);
        PieceMap[cell] = party;
        Parties.Add(party);
    }

    public void MovePiece(Node2D piece, Vector2I dst)
    {
        var src = TileMap.LocalToMap(piece.GlobalPosition);
        piece.GlobalPosition = PosForPiece(dst);
    }

    public void EndBattle()
    {
        RemoveChild(TileMap);
        _room.AddChild(TileMap);
    }

    private Vector2 PosForPiece(Vector2I coord) => TileMap.MapToLocal(coord) - new Vector2(0, 6);
}
