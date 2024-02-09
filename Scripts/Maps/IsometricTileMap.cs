namespace EESaga.Scripts.Maps;

using Godot;
using System.Collections.Generic;

public partial class IsometricTileMap : TileMap
{
    public Vector2I? SelectedTile { get; private set; } = null;
    public List<Vector2I> AvailableCells = [];

    public const int TileSetId = 0;
    public static Vector2I BoundaryAtlas = new(0, 0);
    public static Vector2I DefaultTileAtlas = new(1, 0);
    public const int TileSetAdvancedId = 1;
    public const int TileSelectedId = 999;
    public static Vector2I TileSelectedAtlas = new(0, 0);
    public static Vector2I TileDestinationAtlas = new(0, 1);

    private AStarGrid2D _astar = new()
    {
        CellSize = new Vector2I(1, 1),
        DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan,
        DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan,
        DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never,
    };

    public static IsometricTileMap Instance() => GD.Load<PackedScene>("res://Scenes/Maps/isometric_tile_map.tscn").Instantiate<IsometricTileMap>();

    public override void _Ready()
    {
        UpdateCells();
        UpdateAStar();
        GenerateBoundary();
    }

    public override void _Process(double delta)
    {
        UpdateSelectedTile();
    }

    private void GenerateBoundary()
    {
        List<Vector2I> offsets = [
            Vector2I.Up,
            Vector2I.Down,
            Vector2I.Left,
            Vector2I.Right,
        ];
        var usedCells = GetUsedCells((int)Layer.Ground);
        foreach (var cell in usedCells)
        {
            foreach (var offset in offsets)
            {
                var neighbor = cell + offset;
                if (!usedCells.Contains(neighbor))
                {
                    SetCell((int)Layer.Ground, neighbor, TileSetId, BoundaryAtlas);
                }
            }
        }
    }

    public List<Vector2I> GetAStarPath(Vector2I src, Vector2I dst) => new(_astar.GetIdPath(src, dst));

    public List<Vector2I> GetAccessableTiles(Vector2I src, int range)
    {
        var availableTiles = new List<Vector2I>();
        foreach (var cell in GetUsedCells((int)Layer.Ground))
        {
            if (GetManhattanDistance(src, cell) <= range &&
                !IsBoundary((int)Layer.Ground, cell))
            {
                if (GetAStarPath(src, cell).Count <= range + 1)
                {
                    availableTiles.Add(cell);
                }
            }
        }
        return availableTiles;
    }

    public void CopyFrom(IsometricTileMap tileMap, bool filterBoundary = true)
    {
        Clear();
        var layersCount = tileMap.GetLayersCount();
        for (var layer = 0; layer < layersCount; layer++)
        {
            var usedCells = tileMap.GetUsedCells(layer);
            foreach (var cell in usedCells)
            {
                if (!(filterBoundary && IsBoundary(layer, cell)))
                {
                    SetCell(layer, cell,
                    tileMap.GetCellSourceId(layer, cell),
                    tileMap.GetCellAtlasCoords(layer, cell));
                }
            }
        }
        UpdateCells();
        UpdateAStar();
    }

    private void UpdateCells()
    {
        AvailableCells.Clear();
        var usedCells = GetUsedCells((int)Layer.Ground);
        foreach (var cell in usedCells)
        {
            if (!IsBoundary((int)Layer.Ground, cell) &&
                GetCellTileData((int)Layer.Obstacle, cell) == null &&
                !AvailableCells.Contains(cell))
            {
                AvailableCells.Add(cell);
            }
        }
    }

    private void UpdateSelectedTile()
    {
        var mousePos = GetGlobalMousePosition();
        var tileMapPos = LocalToMap(mousePos);
        if (tileMapPos != SelectedTile)
        {
            if (SelectedTile != null)
            {
                SetCell((int)Layer.Cursor, (Vector2I)SelectedTile, TileSelectedId, null);
            }
            if (GetCellTileData((int)Layer.Ground, tileMapPos) != null &&
                !IsBoundary((int)Layer.Ground, tileMapPos))
            {
                SetCell((int)Layer.Cursor, tileMapPos, TileSelectedId, TileSelectedAtlas);
                SelectedTile = tileMapPos;
            }
            else
            {
                SelectedTile = null;
            }
        }
    }

    private void UpdateAStar()
    {
        _astar.Region = GetUsedRect();
        var rect2IList = Rect2IContains(_astar.Region);
        foreach (var cell in rect2IList)
        {
            if (GetCellTileData((int)Layer.Ground, cell) == null ||
                IsBoundary((int)Layer.Ground, cell) ||
                GetCellTileData((int)Layer.Obstacle, cell) != null)
            {
                _astar.SetPointSolid(cell);
            }
        }
        _astar.Update();
    }

    private static int GetManhattanDistance(Vector2I src, Vector2I dst)
    {
        return Mathf.Abs(src.X - dst.X) + Mathf.Abs(src.Y - dst.Y);
    }

    private static List<Vector2I> Rect2IContains(Rect2I rect)
    {
        var cells = new List<Vector2I>();
        for (var x = rect.Position.X; x < rect.Position.X + rect.Size.X; x++)
        {
            for (var y = rect.Position.Y; y < rect.Position.Y + rect.Size.Y; y++)
            {
                cells.Add(new Vector2I(x, y));
            }
        }
        return cells;
    }

    private bool IsBoundary(int layer, Vector2I coords)
        => GetCellSourceId(layer, coords) == TileSetId &&
        GetCellAtlasCoords(layer, coords) == BoundaryAtlas;
}

public enum Layer
{
    Ground,
    Obstacle,
    Cursor,
}