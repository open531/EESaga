namespace EESaga.Scripts.Maps;

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
    private Area2D _wallUp;
    private Area2D _wallRight;
    private Area2D _wallDown;
    private Area2D _wallLeft;
    private Area2D _doorUp;
    private Area2D _doorRight;
    private Area2D _doorDown;
    private Area2D _doorLeft;

    public override void _Ready()
    {
        _groundTileMap = GetNode<TileMap>("GroundTileMap");
        _obstacleTileMap = GetNode<TileMap>("ObstacleTileMap");
        _wallUp = GetNode<Area2D>("%WallUp");
        _wallRight = GetNode<Area2D>("%WallRight");
        _wallDown = GetNode<Area2D>("%WallDown");
        _wallLeft = GetNode<Area2D>("%WallLeft");
        _doorUp = GetNode<Area2D>("%DoorUp");
        _doorRight = GetNode<Area2D>("%DoorRight");
        _doorDown = GetNode<Area2D>("%DoorDown");
        _doorLeft = GetNode<Area2D>("%DoorLeft");
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
