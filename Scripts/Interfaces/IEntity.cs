namespace EESaga.Scripts.Interfaces;

using Godot;

public interface IEntity
{
    public int Health { get; set; }
    public int Speed { get; set; }
}