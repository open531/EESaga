namespace EESaga.Scripts.Entities;

using EESaga.Scripts.Cards;
using Godot;
using UI;
using Utilities;

public partial class Player : CharacterBody2D
{
    public string PlayerName { get; set; }
    public float Speed { get; set; } = 120.0f;
    public int Level { get; set; }
    protected int _healthMax;
    public int HealthMax
    {
        get => _healthMax;
        set
        {
            if (value < 0) _healthMax = 0;
            else _healthMax = value;
            if (_health < _healthMax) _health = _healthMax;
        }
    }
    protected int _health;
    public int Health
    {
        get => _health;
        set
        {
            if (value < 0) _health = 0;
            else if (value > _healthMax) _health = _healthMax;
            else _health = value;
        }
    }
    public int Shield { get; set; }
    protected int _energyMax;
    public int EnergyMax
    {
        get => _energyMax;
        set
        {
            if (value < 0) _energyMax = 0;
            else _energyMax = value;
            if (_energy < _energyMax) _energy = _energyMax;
        }
    }
    protected int _energy;
    public int Energy
    {
        get => _energy;
        set
        {
            if (value < 0) _energy = 0;
            else if (value > _energyMax) _energy = _energyMax;
            else _energy = value;
        }
    }
    public int Agility { get; set; }
    public BattleCards BattleCards { get; set; }

    private AnimatedSprite2D _sprite;
    private CollisionShape2D _collision;

    public static Player Instance() => GD.Load<PackedScene>("res://Scenes/Entities/player.tscn").Instantiate<Player>();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        var directionX = Input.GetAxis("move_left", "move_right");
        var directionY = Input.GetAxis("move_up", "move_down");
        var direction = new Vector2(directionX, directionY);
        if (direction.Length() < 0.1f)
        {
            direction = Vector2.Zero;
            _sprite.Play("idle");
        }
        else
        {
            if (direction.Length() > 1.0f)
            {
                direction = direction.Normalized();
            }
            if (direction.X != 0.0f)
            {
                _sprite.FlipH = direction.X < 0.0f;
            }
            _sprite.Play("move");
        }
        Velocity = direction * Speed;
        MoveAndSlide();
    }

    public void AddBattleCards(CardInfo card, int num)
    {
        for (int i = 0; i < num; i++)
            BattleCards.DeckCards.Add(card);
    }
}
