using Godot;
using System;

public partial class Player : Area2D
{
    [Export] public int Speed { get; set; } = 200;
    [Signal] public delegate void HitEventHandler();
    private Vector2 screenSize;
    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
        //Hide();
    }

    public override void _Process(double delta)
    {
        Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed("player_move_up"))
        {
            velocity.Y -= 1;
        }

        if (Input.IsActionPressed("player_move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("player_move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("player_move_right"))
        {
            velocity.X += 1;
        }

        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite2D.Play();
        }
        else
        {
            animatedSprite2D.Stop();
        }

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
        );
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    private void OnBodyEntered(Node2D body)
    {
        Hide();
        EmitSignal(SignalName.Hit);
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
}
