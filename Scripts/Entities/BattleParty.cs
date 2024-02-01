namespace EESaga.Scripts.Entities;

using Godot;
using Interfaces;

public partial class BattleParty : Area2D, IBattlePiece
{
    [Export] public int Health { get; set; }
    [Export] public int MaxHealth { get; set; }
    [Export] public int Shield { get; set; }
    private AnimatedSprite2D _sprite;
    private CollisionShape2D _collision;
    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
    }
}
