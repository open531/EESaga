namespace EESaga.Scripts.Entities;

using Godot;
using Maps;
using Utilities;

public interface IBattlePiece
{
    public string PieceName { get; set; }
    public int Level { get; set; }
    public int HealthMax { get; set; }
    public int Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    public int MoveRange { get; set; }
    public bool IsMoving { get; set; }
}
