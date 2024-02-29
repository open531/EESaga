namespace EESaga.Scripts.Entities;

using Godot;

public interface IEntity
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Shield { get; set; }
}