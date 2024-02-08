namespace EESaga.Scripts.Maps;

using Godot;
using System.Collections.Generic;

public partial class IsometricTileMap : TileMap
{
    public Vector2I? SelectedTile { get; private set; } = null;
    public const int TileSetId = 0;
    public static Vector2I BoundaryAtlas = new(0, 0);
    public static Vector2I DefaultTileAtlas = new(1, 0);
    public const int TileSetAdvancedId = 1;
    public const int TileSelectedId = 999;
    public static Vector2I TileSelectedAtlas = new(0, 0);

    public override void _Ready()
    {
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
                GetCellAtlasCoords((int)Layer.Ground, tileMapPos) != BoundaryAtlas)
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

    public List<Vector2I> GetAStarPath(Vector2I src, Vector2I dst, bool avoidObstacles = true, bool avoidEntities = true) => throw new System.NotImplementedException();
}

public enum Layer
{
    Ground,
    Cursor,
}