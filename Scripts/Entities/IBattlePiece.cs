namespace EESaga.Scripts.Entities;

using Godot;
using Maps;
using Utilities;

public interface IBattlePiece
{
    public IsometricTileMap TileMap { get; set; }
    public Vector2I TileMapPos { get; }
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    public bool IsMoving { get; set; }
}
