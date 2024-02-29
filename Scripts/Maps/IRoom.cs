namespace EESaga.Scripts.Maps;

using Godot;
using System.Collections.Generic;

public interface IRoom
{ 
    public RoomType RoomType { get; set; }
    public List<Obstacle> Obstacles { get; set; }
    public List<Trap> Traps { get; set; }
}

public enum RoomType
{
    Null,
    Battle,
    Chest,
    Shop,
    Rest,
    Boss,
}

public class Obstacle
{
    public Vector2I Position { get; set; }
    public ObstacleType Type { get; set; }
    public int Health { get; set; }
}

public enum ObstacleType
{
    Null,
    Rock,
    Table,
}

public class Trap
{
    public Vector2I Position { get; set; }
    public TrapType Type { get; set; }
    public int Damage { get; set; }
}

public enum TrapType
{
    Null,
    Spike,
    Fire,
}