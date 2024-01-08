namespace EESaga.Scripts.Player;

using Godot;

public partial class Player : Area2D
{
    [Export] public int Speed { get; set; } = 200;
    [Signal] public delegate void HitEventHandler();

    private AnimatedSprite2D _animatedSprite2D;
    private MoveManager _moveManager = new MoveManager();
    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _Process(double delta)
    {
        var velocity = _moveManager.MoveFromInput() * Speed;

        if (velocity.Length() > 0)
        {
            _animatedSprite2D.Play();
        }
        else
        {
            _animatedSprite2D.Stop();
        }

        Position += velocity * (float)delta;
    }
}
