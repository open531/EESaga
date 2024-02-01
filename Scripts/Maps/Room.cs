namespace EESaga.Scripts.Maps;

using Entities;
using Godot;
using Interfaces;
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

    private TileMap _groundTileMap;
    private TileMap _obstacleTileMap;
    private StaticBody2D _wallUp;
    private StaticBody2D _wallRight;
    private StaticBody2D _wallDown;
    private StaticBody2D _wallLeft;
    private Area2D _doorUp;
    private Area2D _doorRight;
    private Area2D _doorDown;
    private Area2D _doorLeft;
    private Player _player;

    public override void _Ready()
    {
        _groundTileMap = GetNode<TileMap>("GroundTileMap");
        _obstacleTileMap = GetNode<TileMap>("ObstacleTileMap");
        _wallUp = GetNode<StaticBody2D>("%WallUp");
        _wallRight = GetNode<StaticBody2D>("%WallRight");
        _wallDown = GetNode<StaticBody2D>("%WallDown");
        _wallLeft = GetNode<StaticBody2D>("%WallLeft");
        _doorUp = GetNode<Area2D>("%DoorUp");
        _doorRight = GetNode<Area2D>("%DoorRight");
        _doorDown = GetNode<Area2D>("%DoorDown");
        _doorLeft = GetNode<Area2D>("%DoorLeft");
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
