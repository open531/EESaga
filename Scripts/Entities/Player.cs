namespace EESaga.Scripts.Entities;

using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 120.0f;

    private AnimatedSprite2D _sprite;
    private CollisionShape2D _collision;

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
            _sprite.Play("walk");
        }
        Velocity = direction * Speed;
        MoveAndSlide();
    }
}
