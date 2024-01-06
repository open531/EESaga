using Godot;
using System;

namespace EESaga.Scripts.Player
{
    public partial class Player : Area2D
    {
        [Export] public int Speed { get; set; } = 200;
        [Signal] public delegate void HitEventHandler();
        private Vector2 screenSize;
        private MoveManager moveManager = new MoveManager();
        public override void _Ready()
        {
            screenSize = GetViewportRect().Size;
            //Hide();
        }

        public override void _Process(double delta)
        {
            var velocity = moveManager.MoveFromInput();

            var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            if (velocity.Length() > 0)
            {
                velocity *= Speed;
                animatedSprite2D.Play();
            }
            else
            {
                animatedSprite2D.Stop();
            }

            Position += velocity * (float)delta;
            //Position = new Vector2(
            //    x: Mathf.Clamp(Position.X, 0, screenSize.X),
            //    y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
            //);
        }

        public void Start(Vector2 pos)
        {
            Position = pos;
            Show();
            GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
        }
    }

}