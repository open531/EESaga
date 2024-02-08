namespace EESaga.Scripts.Maps;

using Entities;
using Godot;
using Interfaces;
using Managers;
using System.Collections.Generic;

public partial class Room : Node2D, IRoom
{
    [Export] public RoomType RoomType { get; set; }
    [ExportGroup("Doors", "Door")]
    [Export] public bool DoorUpExist { get; set; } = false;
    [Export] public bool DoorUpOpen { get; set; } = false;
    [Export] public bool DoorRightExist { get; set; } = false;
    [Export] public bool DoorRightOpen { get; set; } = false;
    [Export] public bool DoorDownExist { get; set; } = false;
    [Export] public bool DoorDownOpen { get; set; } = false;
    [Export] public bool DoorLeftExist { get; set; } = false;
    [Export] public bool DoorLeftOpen { get; set; } = false;
    public List<Obstacle> Obstacles { get; set; }
    public List<Trap> Traps { get; set; }

    public IsometricTileMap TileMap;
    public Node2D Enemies;
    public Node2D Parties;
    private Player _player;

    public override void _Ready()
    {
        TileMap = GetNode<IsometricTileMap>("TileMap");
        Enemies = GetNode<Node2D>("Enemies");
        Parties = GetNode<Node2D>("Parties");
        _player = GetNode<Player>("Player");
    }

    public void OpenDoors()
    {
        if (DoorUpExist)
        {
            DoorUpOpen = true;
        }
        if (DoorRightExist)
        {
            DoorRightOpen = true;
        }
        if (DoorDownExist)
        {
            DoorDownOpen = true;
        }
        if (DoorLeftExist)
        {
            DoorLeftOpen = true;
        }
    }
}
