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
    public int MoveRange { get; set; }
    protected bool isMoving;
    public bool IsMoving
    {
        get => isMoving;
        set
        {
            isMoving = value;
            if (isMoving)
            {
                _sprite.Play("move");
            }
            else
            {
                _sprite.Play("idle");
            }
        }
    }

    public bool FlipH
    {
        get => _sprite.FlipH;
        set => _sprite.FlipH = value;
    }

    protected AnimatedSprite2D _sprite;
    protected CollisionShape2D _collision;

    public static BattlePiece Instance() => GD.Load<PackedScene>("res://Scenes/Entities/battle_piece.tscn").Instantiate<BattlePiece>();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");

        IsMoving = false;
    }
}
