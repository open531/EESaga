namespace EESaga.Scripts.Entities.BattleEnemies;

using EESaga.Scripts.Maps;
using Godot;
using Utilities;

public partial class BattleEnemy : Area2D, IBattleEnemy
{
    public IsometricTileMap TileMap { get; set; }
    public Vector2I TileMapPos => TileMap.LocalToMap(GlobalPosition);
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    public bool IsMoving { get; set; }
    protected AnimatedSprite2D _sprite;
    protected CollisionShape2D _collision;

    public static BattleEnemy Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/battle_enemy.tscn").Instantiate<BattleEnemy>();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    public void Initialize(IBattleEnemy enemy)
    {
        Level = enemy.Level;
        Health = enemy.Health;
        Shield = enemy.Shield;
        Agility = enemy.Agility;
    }

    void IBattlePiece.Move(Vector2I direction)
    {
        throw new System.NotImplementedException();
    }

    void IBattlePiece.MoveTo(Vector2I tileMapPos)
    {
        throw new System.NotImplementedException();
    }
}
