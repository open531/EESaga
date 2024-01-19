namespace EESaga.Scripts.Utilities;

using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Adapted from GDScript of https://github.com/uheartbeast/astar-tilemap
/// </summary>
[Tool]
public partial class AstarTileMap : TileMap
{
    [Export] public PairingMethod CurrentPairingMethod { get; set; } = PairingMethod.SzudzikImproved;
    [Export] public bool UseDiagonal { get; set; } = true;
    [Export] public int Layer { get; set; }

    private AStar2D _astar = new();
    private List<Node2D> _obstacles = [];
    private List<Node2D> _units = [];
    private readonly List<Vector2> _directions = [Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right];
    private readonly int _pairingLimit = (int)Math.Pow(2, 30);
    public enum PairingMethod
    {
        CantorUnsigned,  // positive values only
        CantorSigned,    // both positive and negative values	
        SzudzikUnsigned, // more efficient than cantor
        SzudzikSigned,   // both positive and negative values
        SzudzikImproved  // improved version (best option)
    }

    public override void _Ready()
    {
        Update();
    }

    public override void _Draw()
    {
        if (Engine.IsEditorHint() && TileSet != null)
        {
            var offset = TileSet.TileSize / 2;
            foreach (var point in _astar.GetPointIds())
            {
                if (_astar.IsPointDisabled(point)) continue;
                var pointPosition = _astar.GetPointPosition(point);
                if (PositionHasObstacle(pointPosition) || PositionHasUnit(pointPosition)) continue;
                DrawCircle(pointPosition + offset, 4, Colors.White);
                var pointConnections = _astar.GetPointConnections(point);
                foreach (var pointConnection in pointConnections)
                {
                    if (_astar.IsPointDisabled(pointConnection)) continue;
                    var pointConnectionPosition = _astar.GetPointPosition(pointConnection);
                    if (PositionHasObstacle(pointConnectionPosition) || PositionHasUnit(pointConnectionPosition)) continue;
                    DrawLine(pointPosition + offset, pointConnectionPosition + offset, Colors.White, 2);
                }
            }
        }
    }

    public void Update()
    {
        CreatePathFindingPoints();
        var unitNodes = GetTree().GetNodesInGroup("Units");
        foreach (var unitNode in unitNodes)
        {
            AddUnit((Node2D)unitNode);
        }
        var obstacleNodes = GetTree().GetNodesInGroup("Obstacles");
        foreach (var obstacleNode in obstacleNodes)
        {
            AddObstacle((Node2D)obstacleNode);
        }
    }

    private void CreatePathFindingPoints()
    {
        _astar.Clear();
        var cellPositions = GetUsedCellGlobalPositions();
        foreach (var cellPosition in cellPositions)
        {
            _astar.AddPoint(GetPoint(cellPosition), cellPosition);
            ConnectCardinals(cellPosition);
        }
    }

    private void AddObstacle(Node2D obstacle)
    {
        _obstacles.Add(obstacle);
        if (!obstacle.IsConnected(SignalName.TreeExiting, Callable.From(() => RemoveObstacle(obstacle))))
        {
            obstacle.TreeExiting += () => RemoveObstacle(obstacle);
        }
    }

    private void RemoveObstacle(Node2D obstacle)
    {
        _obstacles.Remove(obstacle);
    }

    private void AddUnit(Node2D unit)
    {
        _units.Add(unit);
        if (!unit.IsConnected(SignalName.TreeExiting, Callable.From(() => RemoveUnit(unit))))
        {
            unit.TreeExiting += () => RemoveUnit(unit);
        }
    }

    private void RemoveUnit(Node2D unit)
    {
        _units.Remove(unit);
    }

    private bool PositionHasObstacle(Vector2 position, List<Vector2> ignoreObstaclePositions = null)
    {
        foreach (var ignoreObstaclePosition in ignoreObstaclePositions)
        {
            if (position == ignoreObstaclePosition) return false;
        }
        foreach (var obstacle in _obstacles)
        {
            if (obstacle.GlobalPosition == position) return true;
        }
        return false;
    }

    private bool PositionHasUnit(Vector2 position, List<Vector2> ignoreUnitPositions = null)
    {
        foreach (var ignoreUnitPosition in ignoreUnitPositions)
        {
            if (position == ignoreUnitPosition) return false;
        }
        foreach (var unit in _units)
        {
            if (unit.GlobalPosition == position) return true;
        }
        return false;
    }

    private List<Vector2> GetAstarPathAvoidingObstaclesAndUnits(Vector2 src, Vector2 dst, List<Node> exceptionUnits = null, int maxDistance = -1)
    {
        SetObstaclePointsDisabled(true);
        SetUnitPointsDisabled(true, exceptionUnits);
        var astarPath = _astar.GetPointPath(GetPoint(src), GetPoint(dst));
        SetObstaclePointsDisabled(false);
        SetUnitPointsDisabled(false);
        return SetPathLength(new List<Vector2>(astarPath), maxDistance);
    }

    private List<Vector2> GetAstarPathAvoidingObstacles(Vector2 src, Vector2 dst, int maxDistance = -1)
    {
        SetObstaclePointsDisabled(true);
        var astarPath = _astar.GetPointPath(GetPoint(src), GetPoint(dst));
        SetObstaclePointsDisabled(false);
        return SetPathLength(new List<Vector2>(astarPath), maxDistance);
    }

    private List<Vector2> StopPathAtUnit(List<Vector2> potentialPathUnits)
    {
        for (var i = 0; i < potentialPathUnits.Count; i++)
        {
            if (PositionHasUnit(potentialPathUnits[i]))
            {
                return potentialPathUnits.GetRange(0, i);
            }
        }
        return potentialPathUnits;
    }

    private List<Vector2> GetAstarPath(Vector2 src, Vector2 dst, int maxDistance = -1)
    {
        var astarPath = _astar.GetPointPath(GetPoint(src), GetPoint(dst));
        return SetPathLength(new List<Vector2>(astarPath), maxDistance);
    }

    private List<Vector2> SetPathLength(List<Vector2> path, int maxDistance)
    {
        if (maxDistance < 0) return path;
        var newSize = Math.Min(maxDistance, path.Count);
        return path.GetRange(0, newSize);
    }

    private void SetObstaclePointsDisabled(bool value)
    {
        foreach (var obstacle in _obstacles)
        {
            _astar.SetPointDisabled(GetPoint(obstacle.GlobalPosition), value);
        }
    }

    private void SetUnitPointsDisabled(bool value, List<Node> exceptionUnits = null)
    {
        foreach (var unit in _units)
        {
            if (exceptionUnits != null && (exceptionUnits.Contains(unit) || exceptionUnits.Contains(unit.Owner))) continue;
            _astar.SetPointDisabled(GetPoint(unit.GlobalPosition), value);
        }
    }

    private List<Vector2> GetFloodfillPositions(Vector2 src, int minRange, int maxRange, bool skipObstacles = true, bool skipUnits = true, bool returnCenter = false)
    {
        List<Vector2> floodfillPositions = [];
        List<Vector2> checkingPositions = [src];

        while (checkingPositions.Count > 0)
        {
            var currentPosition = checkingPositions[0];
            checkingPositions.RemoveAt(0);
            if (skipObstacles && PositionHasObstacle(currentPosition, [src])) continue;
            if (skipUnits && PositionHasUnit(currentPosition, [src])) continue;
            if (floodfillPositions.Contains(currentPosition)) continue;
            var currentPoint = GetPoint(currentPosition);
            if (!_astar.HasPoint(currentPoint)) continue;
            if (_astar.IsPointDisabled(currentPoint)) continue;
            var distance = currentPosition - src;
            var gridDistance = GetGridDistance(distance);
            if (gridDistance > maxRange) continue;
            floodfillPositions.Add(currentPosition);
            foreach (var direction in _directions)
            {
                var newPosition = currentPosition + MapToLocal((Vector2I)direction);
                if (skipObstacles && PositionHasObstacle(newPosition)) continue;
                if (skipUnits && PositionHasUnit(newPosition)) continue;
                if (floodfillPositions.Contains(newPosition)) continue;
                var newPoint = GetPoint(newPosition);
                if (!_astar.HasPoint(newPoint)) continue;
                if (_astar.IsPointDisabled(newPoint)) continue;
                checkingPositions.Add(newPosition);
            }
        }
        if (!returnCenter) floodfillPositions.Remove(src);
        var floodfillPositionsSize = floodfillPositions.Count;
        for (var i = 0; i < floodfillPositionsSize; i++)
        {
            var floodfillPosition = floodfillPositions[floodfillPositionsSize - i - 1];
            var distance = floodfillPosition - src;
            var gridDistance = GetGridDistance(distance);
            if (gridDistance < minRange) floodfillPositions.Remove(floodfillPosition);
        }
        return floodfillPositions;
    }

    private List<Vector2> PathDirections(List<Vector2> path)
    {
        var directions = new List<Vector2>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            directions.Add(path[i + 1] - path[i]);
        }
        return directions;
    }

    private int GetPoint(Vector2 position) => CurrentPairingMethod switch
    {
        PairingMethod.CantorUnsigned => CantorPair((int)position.X, (int)position.Y),
        PairingMethod.CantorSigned => CantorPairSigned((int)position.X, (int)position.Y),
        PairingMethod.SzudzikUnsigned => SzudzikPair((int)position.X, (int)position.Y),
        PairingMethod.SzudzikSigned => SzudzikPairSigned((int)position.X, (int)position.Y),
        PairingMethod.SzudzikImproved => SzudzikPairImproved((int)position.X, (int)position.Y),
        _ => SzudzikPairImproved((int)position.X, (int)position.Y),
    };

    private static int CantorPair(int x, int y) => (x + y) * (x + y + 1) / 2 + y;

    private static int CantorPairSigned(int x, int y) => CantorPair((x >= 0) ? (x * 2) : (x * -2 - 1), (y >= 0) ? (y * 2) : (y * -2 - 1));

    private static int SzudzikPair(int x, int y) => (x >= y) ? (x * x + x + y) : (y * y + x);

    private static int SzudzikPairSigned(int x, int y) => SzudzikPair((x >= 0) ? (x * 2) : (x * -2 - 1), (y >= 0) ? (y * 2) : (y * -2 - 1));

    private static int SzudzikPairImproved(int x, int y)
    {
        int a, b;
        if (x >= 0) a = x * 2; else a = x * -2 - 1;
        if (y >= 0) b = y * 2; else b = y * -2 - 1;
        int c = SzudzikPair(a, b);
        if (a >= 0 && b < 0 || a < 0 && b >= 0) return c;
        return -c - 1;
    }

    private bool HasPoint(Vector2 position) => _astar.HasPoint(GetPoint(position));

    private List<Vector2> GetUsedCellGlobalPositions()
    {
        var usedCellPositions = new List<Vector2>();
        foreach (var cell in GetUsedCells(Layer))
        {
            usedCellPositions.Add(MapToLocal(cell));
        }
        return usedCellPositions;
    }

    private void ConnectCardinals(Vector2 position)
    {
        var center = GetPoint(position);
        List<Vector2> directions = [Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right];
        if (UseDiagonal)
        {
            List<Vector2> diagonalDirections = [Vector2.Up + Vector2.Left, Vector2.Up + Vector2.Right, Vector2.Down + Vector2.Left, Vector2.Down + Vector2.Right];
            directions.AddRange(diagonalDirections);
        }
        foreach (var direction in directions)
        {
            var cardinalPoint = GetPoint(position + MapToLocal((Vector2I)direction));
            if (cardinalPoint != center && _astar.HasPoint(cardinalPoint))
            {
                _astar.ConnectPoints(center, cardinalPoint);
            }
        }
    }

    private float GetGridDistance(Vector2 distance) => LocalToMap((Vector2I)distance).Abs().X + LocalToMap((Vector2I)distance).Abs().Y;
}
