namespace EESaga.Scripts.Maps;

using Godot;
using System;
using System.Collections.Generic;

public partial class IsometricTileMap : TileMap
{
    public Vector2I SelectedTile { get; private set; }
    public const int tileSetId = 0;
    public const int tileSelectedId = 999;
    public static Vector2I tileSelectedAtlas = new Vector2I(0, 0);
    public override void _Process(double delta)
    {
        var mousePos = GetGlobalMousePosition();
        var tileMapPos = LocalToMap(mousePos);
        if (tileMapPos != SelectedTile && GetCellTileData((int)Layer.Ground, tileMapPos) != null)
        {
            SetCell((int)Layer.Cursor, SelectedTile, tileSelectedId, null);
            SetCell((int)Layer.Cursor, tileMapPos, tileSelectedId, tileSelectedAtlas);
            SelectedTile = tileMapPos;
            GD.Print(string.Join(", ", GetAStarPath(new Vector2I(0, 0), tileMapPos)));
        }
    }
    public List<Vector2I> GetAStarPath(Vector2I src, Vector2I dst, bool avoidObstacles = true, bool avoidEntities = true)
    {
        AStarGrid2D astarGrid = new AStarGrid2D();
        astarGrid.Region = new Rect2I(0, 0, 10, 10);
        astarGrid.CellSize = new Vector2I(16, 16);
        astarGrid.DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan;
        astarGrid.DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan;
        astarGrid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Vector2I cellPos = new Vector2I(x, y);
                if (GetCellTileData((int)Layer.Obstacle, cellPos) != null)
                {
                    astarGrid.SetPointSolid(cellPos, true);
                }
            }
        }
        astarGrid.Update();
        List<Vector2I> temp = new List<Vector2I>();
        foreach (var point in astarGrid.GetIdPath(src, dst))
        {
            temp.Add(point);
        }
        return temp;
    }
}

public enum Layer
{
    Ground,
    Obstacle,
    Cursor,
}