namespace EESaga.Scripts.Entities.BattleEnemies;

using Godot;
using Utilities;

public partial class BattleEnemy : Area2D, IBattleEnemy
{
    public int Level { get; set; }
    public RangedInt Health { get; set; }
    public int Shield { get; set; }
    public int Agility { get; set; }
    protected AnimatedSprite2D _sprite;
    protected CollisionShape2D _collision;
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
