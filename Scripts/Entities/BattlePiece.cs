namespace EESaga.Scripts.Entities;

using Godot;
using Maps;
using Utilities;

public partial class BattlePiece : Area2D, IBattlePiece
{
    public IsometricTileMap TileMap { get; set; }
    public Vector2I TileMapPos { get; }
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    public bool IsMoving { get; set; }

    protected AnimatedSprite2D _sprite;
    protected CollisionShape2D _collision;

    public static BattlePiece Instance() => GD.Load<PackedScene>("res://Scenes/Entities/battle_piece.tscn").Instantiate<BattlePiece>();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
    }
}
