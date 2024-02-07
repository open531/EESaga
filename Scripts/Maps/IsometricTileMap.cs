namespace EESaga.Scripts.Maps;

using Godot;
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
        }
    }
    public List<Vector2I> GetAStarPath(Vector2I src, Vector2I dst, bool avoidObstacles = true, bool avoidEntities = true) => throw new System.NotImplementedException();
}

public enum Layer
{
    Ground,
    Cursor,
}