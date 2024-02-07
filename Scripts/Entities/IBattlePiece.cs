namespace EESaga.Scripts.Entities;

using Godot;
using Utilities;

public interface IBattlePiece
{
    public Vector2I TileMapPos { get; set; }
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
}
