namespace EESaga.Scripts.Interfaces;

using Godot;

public interface IBattlePiece
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Shield { get; set; }
}
