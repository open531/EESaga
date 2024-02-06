namespace EESaga.Scripts.Entities;

using Godot;
using Utilities;

public interface IBattlePiece
{
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
}
