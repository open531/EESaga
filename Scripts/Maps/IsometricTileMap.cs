namespace EESaga.Scripts.Maps;

using Godot;
using System.Collections.Generic;

public partial class IsometricTileMap : TileMap
{
    public Vector2I SelectedTile => throw new System.NotImplementedException();
    public List<Vector2I> GetAStarPath(Vector2I src,Vector2I dst, bool avoidObstacles = true, bool avoidEntities = true) => throw new System.NotImplementedException();
}

public struct TileSetInfo
{
    public int SourceId { get; set; }
    public Vector2I Position { get; set; }
    public readonly Vector2I Cube => Position;
    public readonly Vector2I SlabTop => Position + new Vector2I(1, 0);
    public readonly Vector2I SlabBottom => Position + new Vector2I(1, 1);
    public readonly Vector2I SlabLeft => Position + new Vector2I(1, 2);
    public readonly Vector2I SlabRight => Position + new Vector2I(1, 3);
    public readonly Vector2I WallNW => Position + new Vector2I(2, 0);
    public readonly Vector2I WallNE => Position + new Vector2I(2, 1);
    public readonly Vector2I WallSE => Position + new Vector2I(2, 2);
    public readonly Vector2I WallSW => Position + new Vector2I(2, 3);
    public readonly Vector2I WallN => Position + new Vector2I(3, 0);
    public readonly Vector2I WallW => Position + new Vector2I(3, 1);
    public readonly Vector2I WallE => Position + new Vector2I(3, 2);
    public readonly Vector2I WallS => Position + new Vector2I(3, 3);
    public readonly Vector2I SlopeLeft => Position + new Vector2I(4, 0);
    public readonly Vector2I SlopeRight => Position + new Vector2I(4, 1);
    public readonly Vector2I StairsLeft => Position + new Vector2I(4, 2);
    public readonly Vector2I StairsRight => Position + new Vector2I(4, 3);
}