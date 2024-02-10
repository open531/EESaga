namespace EESaga.Scripts.Entities.BattleEnemies;

using EESaga.Scripts.Maps;
using Godot;
using Utilities;

public partial class BattleEnemy : BattlePiece, IBattleEnemy
{
    public static new BattleEnemy Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/battle_enemy.tscn").Instantiate<BattleEnemy>();

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
}
