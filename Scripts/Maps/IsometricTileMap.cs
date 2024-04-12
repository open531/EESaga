namespace EESaga.Scripts.Maps;

using Entities;
using Godot;
using System.Collections.Generic;

public partial class IsometricTileMap : TileMap
{
    public Vector2I? SelectedCell { get; private set; } = null;

    /// <summary>
    /// 可以用来放置棋子的地图坐标
    /// </summary>
    public List<Vector2I> AvailableCells = [];

    public static List<Vector2I> ColorList = new()
    {
        new Vector2I(0, 0),
        new Vector2I(1, 0),
        new Vector2I(2, 0),
        new Vector2I(3, 0),
        new Vector2I(4, 0),
        new Vector2I(5, 0),
        new Vector2I(6, 0),
        new Vector2I(7, 0),
        new Vector2I(8, 0),
        new Vector2I(9, 0),
        new Vector2I(10, 0),
        new Vector2I(11, 0),// 罗姆之巅楼层颜色，暂时使用10风格
    };

    public const int TileSetId = 0;
    public static Vector2I BoundaryAtlas = new(0, 0);
    public static Vector2I DefaultTileAtlas = new(1, 0);
    public static Vector2I BlackAtlas = new(2, 0);
    public static Vector2I GreyAtlas = new(3, 0);
    public static Vector2I PurpleAtlas = new(4, 0);
    public static Vector2I GreenAtlas = new(5, 0);
    public static Vector2I LittlePurpleAtlas = new(6, 0);
    public static Vector2I OrangeAtlas = new(7, 0);
    public static Vector2I LittleBlueAtlas = new(8, 0);
    public static Vector2I RedAtlas = new(9, 0);
    public static Vector2I YellowAtlas = new(10, 0);
    public const int TileSetAdvancedId = 1;
    public const int TileSelectedId = 999;
    public static Vector2I TileSelectedAtlas = new(0, 0);
    public static Vector2I TileDestinationAtlas = new(0, 1);
    public static Vector2I TileAttackAtlas = new(1, 1);

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

    public List<Vector2I> GetAccessibleTiles(Vector2I src, int range)
    {
        var accessibleTiles = new List<Vector2I>();
        var usedCells = GetUsedCells((int)Layer.Ground);
        foreach (var cell in usedCells)
        {
            if (GetManhattanDistance(src, cell) <= range &&
                !IsBoundary((int)Layer.Ground, cell))
            {
                if (GetAStarPath(src, cell).Count <= range + 1 && GetAStarPath(src, cell).Count > 0)
                {
                    accessibleTiles.Add(cell);
                }
            }
        }
        return accessibleTiles;
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

    public void UpdateSelectedTile()
    {
        var mousePos = GetGlobalMousePosition();
        var tileMapPos = LocalToMap(mousePos);
        if (tileMapPos != SelectedCell)
        {
            if (SelectedCell != null)
            {
                SetCell((int)Layer.Cursor, (Vector2I)SelectedCell, TileSelectedId, null);
            }
            if (GetCellTileData((int)Layer.Ground, tileMapPos) != null &&
                !IsBoundary((int)Layer.Ground, tileMapPos))
            {
                SetCell((int)Layer.Cursor, tileMapPos, TileSelectedId, TileSelectedAtlas);
                SelectedCell = tileMapPos;
            }
            else
            {
                SelectedCell = null;
            }
        }
    }

    public void UpdateAStar()
    {
        _astar.Region = GetUsedRect();
        _astar.Update();
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
    }

    private static int GetManhattanDistance(Vector2I src, Vector2I dst)
    {
        return Mathf.Abs(src.X - dst.X) + Mathf.Abs(src.Y - dst.Y);
    }

    public static List<Vector2I> Rect2IContains(Rect2I rect)
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

    public bool IsBoundary(int layer, Vector2I coords)
        => GetCellSourceId(layer, coords) == TileSetId &&
        GetCellAtlasCoords(layer, coords) == BoundaryAtlas;

    public bool IsDestination(Vector2I coords)
        => GetCellSourceId((int)Layer.Mark, coords) == TileSelectedId &&
        GetCellAtlasCoords((int)Layer.Mark, coords) == TileDestinationAtlas;
}

public enum Layer
{
    Ground,
    Obstacle,
    Mark,
    Cursor,
}
