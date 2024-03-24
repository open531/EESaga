namespace EESaga.Scripts.Maps;

using Cards;
using EESaga.Scripts.Data;
using EESaga.Scripts.Managers;
using EESaga.Scripts.UI;
using EESaga.Scripts.Utilities;
using Entities;
using Entities.BattleEnemies;
using Entities.BattleParties;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

public partial class PieceBattle : Node2D
{
    [Export] public double PieceMoveTime { get; set; } = 0.2;
    public IsometricTileMap TileMap { get; set; }
    public List<BattlePiece> Pieces { get; set; } = [];
    public List<BattleParty> Parties { get; set; } = [];
    public List<BattleEnemy> Enemies { get; set; } = [];
    public List<Obstacle> Obstacles { get; set; } = [];
    public List<Trap> Traps { get; set; } = [];

    public BattlePiece CurrentPiece { get; set; }

    public int CardRange
    {
        get
        {
            if (BattleManager.CardBattle.OperatingCard == null)
            {
                return 3;
            }
            else
            {
                return BattleManager.CardBattle.OperatingCard.CardRange;
            }
        }
    }

    private bool _isRefreshing;

    public System.Collections.Generic.Dictionary<Vector2I, BattlePiece> PieceMap { get; set; } = [];

    public System.Collections.Generic.Dictionary<Vector2I, Vector2I> ColorMap { get; set; } = [];
    public System.Collections.Generic.Dictionary<Vector2I, Vector2I> RangeMap { get; set; } = [];
    public BattleManager BattleManager { get; set; }

    [Signal] public delegate void PieceMovedEventHandler();

    private Room _room;
    private Node2D _enemies;
    private Node2D _parties;
    private Timer _pieceMoveTimer;
    private Camera2D _camera;
    private PieceDetail _pieceDetail;
    public SceneSwitcher _sceneSwitcher;
    private Label _gameLevelLabel;

    private AStarGrid2D _astar = new()
    {
        CellSize = new Vector2I(1, 1),
        DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan,
        DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan,
        DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never,
    };
    private List<Vector2I> _pieceMovePath = [];
    private int _pieceMoveIndex = 0;

    public static PieceBattle Instance() => GD.Load<PackedScene>("res://Scenes/Maps/piece_battle.tscn").Instantiate<PieceBattle>();

    public override void _Ready()
    {
        TileMap = GetNode<IsometricTileMap>("TileMap");
        _enemies = GetNode<Node2D>("Enemies");
        _parties = GetNode<Node2D>("Parties");
        _pieceMoveTimer = GetNode<Timer>("PieceMoveTimer");
        _camera = GetNode<Camera2D>("Camera2D");
        _pieceDetail = GetNode<PieceDetail>("%PieceDetail");
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        _gameLevelLabel = GetNode<Label>("%GameLevelLabel");

        _pieceMoveTimer.Autostart = true;
        _pieceMoveTimer.WaitTime = PieceMoveTime;
        _pieceMoveTimer.Timeout += OnPieceMoveTimerTimeout;

        UpdateLevelLabel();

        _isRefreshing = false;
        #region test
        var tileMap = IsometricTileMap.Instance();
        for (var x = 0; x < 10; x++)
        {
            for (var y = 0; y < 10; y++)
            {
                tileMap.SetCell((int)Layer.Ground, new Vector2I(x, y), IsometricTileMap.TileSetId, IsometricTileMap.DefaultTileAtlas);
            }
        }
        Initialize(tileMap);
        AddEnemyByFloor();
        var player = PlayerBattle.Instance();
        player.BattleCards = new BattleCards()
        {
            DeckCards =
            [
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CSStruggle,
                CardData.CIECS,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CAStrike,
                CardData.CSCure,
                CardData.CSCure,
                CardData.CSCure,
                CardData.CSCure,
            ],
            HandCards = [],
            DiscardCards =
            [
                CardData.CAStrike,
                CardData.CDDefend,
                CardData.CSStruggle,
                CardData.CIECS,
            ],
        };
        AddParty(player);
        #endregion
    }

    public override void _Process(double delta)
    {
        TileMap.UpdateSelectedTile();
        if (TileMap.SelectedCell != null)
        {
            _pieceDetail.Update(PieceMap[TileMap.SelectedCell.Value]);
            if (_isRefreshing)
            {
                RecoverRangeTiles();
                ShowRangeTiles(TileMap.LocalToMap(CurrentPiece.GlobalPosition), TileMap.SelectedCell.Value, CardRange);
            }
        }
        else
        {
            _pieceDetail.Update(null);
        }
    }

    public void UpdateLevelLabel()
    {
        _gameLevelLabel.Text = $"{Tr("GAME_LEVEL")}: {SaveData.GameRecord.Floor}";
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
            12 * (rectCenter.X + rectCenter.Y) + 40);
        _camera.Enabled = true;
    }

    public void AddEnemyByFloor()
    {
        var floor = SaveData.GameRecord.Floor;
        var enemyCountDic = GameData.enemyInfo[floor].EnemyCountDic;
        foreach (var enemyInfo in enemyCountDic)
        {
            var eneType = enemyInfo.Key;
            var eneCount = enemyInfo.Value;
            for (int i = 0; i < eneCount; i++)
            {
                AddEnemy(eneType);
            }
        }
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
        Pieces.Add(enemy);
        enemy.PieceDeath += HandlePieceDeath;
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
        Pieces.Add(party);
        party.PieceDeath += HandlePieceDeath;
    }

    public void AddParty(BattleParty party)
    {
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
        Pieces.Add(party);
        party.PieceDeath += HandlePieceDeath;
    }

    private void HandlePieceDeath(BattlePiece battlePiece)
    {
        PreCheckGameOver(battlePiece);
        ClearByPiece(battlePiece);
    }

    public void ShowAccessibleTiles(int range, bool auto = false)
    {
        TileMap.ClearLayer((int)Layer.Mark);
        _astar.Clear();
        var src = TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var _range = 20;
        var autoAccessibleTiles = TileMap.GetAccessibleTiles(src, _range);
        var accessibleTiles = TileMap.GetAccessibleTiles(src, range);
        if (auto)
        {
            var primaryTiles = new List<Vector2I>();
            foreach (var tile in accessibleTiles)
            {
                if (PieceMap[tile] == null || tile == src)
                {
                    primaryTiles.Add(tile);
                }
            }
            var PrimaryTiles = new List<Vector2I>();//这用于判断自动行走范围，因此较大
            foreach (var tile in autoAccessibleTiles)
            {
                if (PieceMap[tile] == null || tile == src)
                {
                    PrimaryTiles.Add(tile);
                }
            }
            _astar.Region = TileMap.GetUsedRect();
            _astar.Update();
            var checkTiles = IsometricTileMap.Rect2IContains(_astar.Region);
            foreach (var tile in checkTiles)
            {
                if (!PrimaryTiles.Contains(tile))
                {
                    _astar.SetPointSolid(tile);
                }
            }
            foreach (var tile in primaryTiles)
            {
                var path = GetAStarPath(src, tile);
                if (path.Count <= range + 1 && path.Count > 0)
                {
                    TileMap.SetCell((int)Layer.Mark, tile, IsometricTileMap.TileSelectedId, IsometricTileMap.TileDestinationAtlas);
                }
            }
        }
        else
        {
            var primaryTiles = new List<Vector2I>();
            foreach (var tile in accessibleTiles)
            {
                if (PieceMap[tile] == null || tile == src)
                {
                    primaryTiles.Add(tile);
                }
            }
            _astar.Region = TileMap.GetUsedRect();
            _astar.Update();
            var checkTiles = IsometricTileMap.Rect2IContains(_astar.Region);
            foreach (var tile in checkTiles)
            {
                if (!primaryTiles.Contains(tile))
                {
                    _astar.SetPointSolid(tile);
                }
            }
            foreach (var tile in primaryTiles)
            {
                if (GetAStarPath(src, tile).Count <= range + 1 && GetAStarPath(src, tile).Count > 0)
                {
                    TileMap.SetCell((int)Layer.Mark, tile, IsometricTileMap.TileSelectedId, IsometricTileMap.TileDestinationAtlas);
                }
            }
        }
    }

    public void ShowEffectTiles(int range, CardTarget cardTarget)
    {
        if (CurrentPiece.IsMoving)
        {
            return;
        }
        var src = TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var usedCells = TileMap.GetUsedCells((int)Layer.Ground);
        switch (cardTarget)
        {
            case CardTarget.Self:
                ColorMap.Add(src, TileMap.GetCellAtlasCoords((int)Layer.Mark, src));
                TileMap.SetCell((int)Layer.Mark, src, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                break;
            case CardTarget.Enemy:
                foreach (var cell in usedCells)
                {
                    if (Mathf.Abs(cell.X - src.X) <= range && Mathf.Abs(cell.Y - src.Y) <= range && PieceMap[cell] is BattleEnemy)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
            case CardTarget.Range:
                _isRefreshing = true;
                var dirShowTiles = new List<Vector2I>()
                {
                    new Vector2I(src.X+1, src.Y),
                    new Vector2I(src.X-1, src.Y),
                    new Vector2I(src.X, src.Y+1),
                    new Vector2I(src.X, src.Y-1),
                };
                foreach (var cell in dirShowTiles)
                {
                    if (!TileMap.IsBoundary((int)(Layer.Ground), cell))
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
            case CardTarget.Ally:
                foreach (var cell in usedCells)
                {
                    if (Mathf.Abs(cell.X - src.X) <= range && Mathf.Abs(cell.Y - src.Y) <= range && PieceMap[cell] is BattleParty)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
            case CardTarget.AllEnemies:
                foreach (var cell in usedCells)
                {
                    if (PieceMap[cell] is BattleEnemy)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
            case CardTarget.AllAllies:
                foreach (var cell in usedCells)
                {
                    if (PieceMap[cell] is BattleParty)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
            case CardTarget.All:
                foreach (var cell in usedCells)
                {
                    if (PieceMap[cell] is BattleParty)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                    else if (PieceMap[cell] is BattleEnemy)
                    {
                        ColorMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                        TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    }
                }
                break;
        }
    }

    public void RecoverEffectTiles()
    {
        if (_isRefreshing)
        {
            _isRefreshing = false;
            RecoverRangeTiles();
        }
        if (ColorMap.Count() == 0)
        {
            return;
        }
        var vector2I = new Vector2I(-1, -1);
        foreach (var item in ColorMap)
        {
            if (item.Value == vector2I)
            {
                TileMap.SetCell((int)Layer.Mark, item.Key, IsometricTileMap.TileSelectedId, IsometricTileMap.DefaultTileAtlas);
            }
            else
            {
                TileMap.SetCell((int)Layer.Mark, item.Key, IsometricTileMap.TileSelectedId, item.Value);
            }
        }
        ColorMap.Clear();
    }

    public void ShowRangeTiles(Vector2I src, Vector2I dst, int range)
    {
        if (IsNear(src, dst))
        {
            var dir = new Vector2I(dst.X - src.X, dst.Y - src.Y);
            var ortho = new Vector2I();
            if (dir.X != 0)
            {
                ortho.X = 0;
                ortho.Y = 1;
            }
            var usedCells = TileMap.GetUsedCells((int)Layer.Ground);
            var tilesToShow = new List<Vector2I>();
            var tilesToDelete = new List<Vector2I>();
            for (int i = 1; i < range + 1; i++)
            {
                var temp = new Vector2I(src.X + i * dir.X, src.Y + i * dir.Y);
                tilesToShow.Add(temp);
                for (int j = 1; j < i + 1; j++)
                {
                    tilesToShow.Add(new Vector2I(temp.X + ortho.X * j, temp.Y + ortho.Y * j));
                    tilesToShow.Add(new Vector2I(temp.X - ortho.X * j, temp.Y - ortho.Y * j));
                }
            }
            foreach (var cell in tilesToShow)
            {
                if (usedCells.Contains(cell) && !TileMap.IsBoundary((int)Layer.Ground, cell))
                {
                    TileMap.SetCell((int)Layer.Mark, cell, IsometricTileMap.TileSelectedId, IsometricTileMap.TileAttackAtlas);
                    RangeMap.Add(cell, TileMap.GetCellAtlasCoords((int)Layer.Mark, cell));
                }
            }
        }
    }

    public void RecoverRangeTiles()
    {
        if (RangeMap.Count() == 0)
        {
            return;
        }
        var vector2I = new Vector2I(-1, -1);
        foreach (var item in RangeMap)
        {
            if (ColorMap.ContainsKey(item.Key))
            {
                continue;
            }
            else if (item.Value == vector2I)
            {
                TileMap.SetCell((int)Layer.Mark, item.Key, IsometricTileMap.TileSelectedId, IsometricTileMap.DefaultTileAtlas);
            }
            else
            {
                TileMap.SetCell((int)Layer.Mark, item.Key, IsometricTileMap.TileSelectedId, item.Value);
            }
        }
        RangeMap.Clear();
    }

    public void MoveCurrentPiece(Vector2I dst)
    {
        var src = TileMap.LocalToMap(CurrentPiece.GlobalPosition);
        var path = GetAStarPath(src, dst);
        if (path.Count > 1)
        {
            _pieceMovePath = path.Skip(1).ToList();
            _pieceMoveIndex = 0;
            CurrentPiece.IsMoving = true;
            if (BattleManager.CardBattle.OperatingCard != null)
            {
                BattleManager.CardBattle.RecoverCardStatus();
            }
            PieceMap[src] = null;
            PieceMap[dst] = CurrentPiece;
            if (_pieceMoveTimer.IsInsideTree())
            {
                _pieceMoveTimer.Start();
            }
            else
            {
                AddChild(_pieceMoveTimer);
                _pieceMoveTimer.Start();
            }
        }
        else
        {
            EmitSignal(SignalName.PieceMoved);
        }
    }

    private Vector2 PosForPiece(Vector2I coord) => TileMap.MapToLocal(coord) - new Vector2(0, 6);

    public List<Vector2I> GetAStarPath(Vector2I src, Vector2I dst) => new(_astar.GetIdPath(src, dst));

    private void OnPieceMoveTimerTimeout()
    {
        if (_pieceMovePath != null && _pieceMovePath.Count > 0)
        {
            if (_pieceMoveIndex >= _pieceMovePath.Count)
            {
                _pieceMovePath = [];
                _pieceMoveIndex = 0;
                CurrentPiece.IsMoving = false;
                _pieceMoveTimer.Stop();
                EmitSignal(SignalName.PieceMoved);
            }
            else
            {
                var tween = CreateTween();
                tween.TweenProperty(CurrentPiece, "global_position",
                    PosForPiece(_pieceMovePath[_pieceMoveIndex]), PieceMoveTime);
                CurrentPiece.FlipH = PosForPiece(_pieceMovePath[_pieceMoveIndex]).X - CurrentPiece.GlobalPosition.X < 0;
                _pieceMoveIndex++;
            }
        }
    }

    public void ClearByCell(Vector2I cell)
    {
        if (PieceMap.TryGetValue(cell, out var piece))
        {
            if (piece == null)
            {
                return;
            }
            if (piece is BattleEnemy enemy)
            {
                Enemies.Remove(enemy);
                Pieces.Remove(enemy);
            }
            else if (piece is BattleParty party)
            {
                Parties.Remove(party);
                Pieces.Remove(party);
            }
            piece.QueueFree();
            PieceMap[cell] = null;
        }
    }

    public void ClearByPiece(BattlePiece battlePiece)
    {
        var cell = TileMap.LocalToMap(battlePiece.GlobalPosition);
        ClearByCell(cell);
    }

    public void PreCheckGameOver(BattlePiece battlePiece)
    {
        if (battlePiece is BattleParty && Parties.Count == 1)
        {
            GD.Print(" Game Over");
            _sceneSwitcher.PushScene(SceneSwitcher.GameOver);
        }
        else if (battlePiece is BattleEnemy && Enemies.Count == 1)
        {
            GD.Print("Game Win");
            _sceneSwitcher.PushScene(SceneSwitcher.GameWin);
        }
    }

    /// <summary>
    /// 按照距离cell的距离以及棋子的生命值依次排序，返回排序后的棋子列表
    /// </summary>
    /// <param name="cell">参考点</param>
    public List<BattleParty> GetNearestParty(Vector2I cell)
    {
        List<BattleParty> battleParties = new List<BattleParty>();
        foreach (var party in Parties)
        {
            battleParties.Add(party);
        }
        if (battleParties.Count > 1)
        {
            List<BattleParty> orderedBattleParties = battleParties.OrderBy(p => GetAStarPath(cell, TileMap.LocalToMap(p.GlobalPosition)).Count).ThenBy(x => x.Health).ToList();
            return orderedBattleParties;
        }
        else
        {
            return battleParties;
        }
    }

    public static int GetManhattanDistance(Vector2I src, Vector2I dst) => Mathf.Abs(src.X - dst.X) + Mathf.Abs(src.Y - dst.Y);
    public static bool IsNear(Vector2I src, Vector2I dst)
    {
        var manhattanDistance = GetManhattanDistance(src, dst);
        if (manhattanDistance == 1 || (manhattanDistance == 2 && Mathf.Abs(src.X - dst.X) == 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsNeighbor(Vector2I src, Vector2I dst)
    {
        if (GetManhattanDistance(src, dst) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
