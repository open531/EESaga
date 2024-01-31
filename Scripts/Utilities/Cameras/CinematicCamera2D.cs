namespace EESaga.Scripts.Utilities.Cameras;

using Godot;
using System;

public partial class CinematicCamera2D : Camera2D
{
    [Export] public Node2D FollowNode { get; set; } = null;
    [Export] public CameraHost CameraHost { get; set; } = null;
    [Export] public float TweenSpeed { get; set; } = 2.0f;
    public override void _Process(double delta)
    {
        if (IsInstanceValid(CameraHost))
        {
            Zoom = Zoom.MoveToward(CameraHost.Zoom, (float)delta * Math.Max(0.0f, TweenSpeed));
            Offset = Offset.MoveToward(CameraHost.Offset, (float)delta * Math.Max(0.0f, TweenSpeed));
            var halfBounds = GetViewportRect().Size / Zoom / 2;
            var topLeft = new Vector2(CameraHost.LimitLeft, CameraHost.LimitTop);
            var bottomRight = new Vector2(CameraHost.LimitRight, CameraHost.LimitBottom);
            topLeft += CameraHost.GlobalPosition + halfBounds - Offset;
            bottomRight += CameraHost.GlobalPosition - halfBounds - Offset;
            if (IsInstanceValid(FollowNode))
            {
                GlobalPosition = FollowNode.GlobalPosition.Clamp(topLeft, bottomRight);
            }
            else
            {
                GlobalPosition = GlobalPosition.Clamp(topLeft, bottomRight);
            }
        }
        else if (IsInstanceValid(FollowNode))
        {
            GlobalPosition = FollowNode.GlobalPosition;
        }
    }
}
