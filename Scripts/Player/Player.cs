namespace EESaga.Scripts.Player;

using Godot;

public partial class Player : Area2D
{
    [Export] public int Speed { get; set; } = 200;
    [Signal] public delegate void HitEventHandler();

    private AnimatedSprite2D _animatedSprite2D;
    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
}
