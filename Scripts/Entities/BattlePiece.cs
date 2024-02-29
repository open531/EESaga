namespace EESaga.Scripts.Entities;

using Godot;

public partial class BattlePiece : Area2D, IBattlePiece
{
    public virtual int Level { get; set; }
    protected int _healthMax;
    public virtual int HealthMax
    {
        get => _healthMax;
        set
        {
            if (value < 0) _healthMax = 0;
            else _healthMax = value;
            if (_health < _healthMax) _health = _healthMax;
            if (_healthBar != null)
            {
                _healthBar.MaxValue = _healthMax;
                _healthBar.Value = _health;
            }
        }
    }
    protected int _health;
    public virtual int Health
    {
        get => _health;
        set
        {
            if (value < 0) _health = 0;
            else if (value > _healthMax) _health = _healthMax;
            else _health = value;
            if (_healthBar != null)
            {
                _healthBar.MaxValue = _healthMax;
                _healthBar.Value = _health;
            }
        }
    }
    public virtual int Shield { get; set; }
    public virtual int Agility { get; set; }
    public virtual int MoveRange { get; set; }
    protected bool _isMoving;
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            if (_isMoving)
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
    protected TextureProgressBar _healthBar;

    public static BattlePiece Instance() => GD.Load<PackedScene>("res://Scenes/Entities/battle_piece.tscn").Instantiate<BattlePiece>();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
        _healthBar = GetNode<TextureProgressBar>("HealthBar");

        IsMoving = false;
    }
}
