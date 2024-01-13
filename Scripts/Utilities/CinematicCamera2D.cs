namespace EESaga.Scripts.Utilities;

using Godot;
using System;

/// <summary>
/// <para>
/// Cinematic camera node for 2D scenes.
/// Uses a VirtualCamera2D to create transitions between cameras.
/// </para>
/// <para>
/// Adapted from GDScript of https://github.com/HexagonNico/CinematicCamera2D
/// </para>
/// </summary>
public partial class CinematicCamera2D : Camera2D
{
    /// <summary>
    /// The node that this camera is supposed to follow.
    /// The camera's global position will be set to this node's global position every frame.
    /// </summary>
    [Export] public Node2D FollowNode { get; set; } = null;

    /// <summary>
    /// The current virtual camera node. A scene may have several virtual cameras, but only needs one
    /// cinematic camera. Update this value to transition between different cameras.
    /// </summary>
    [Export] public VirtualCamera2D VirtualCamera { get; set; } = null;

    [Export] public float TransitionSpeed { get; set; } = 1.0f;

    public override void _Process(double delta)
    {
        if (IsInstanceValid(VirtualCamera))
        {
            Zoom = Zoom.MoveToward(VirtualCamera.Zoom, (float)delta * (float)Math.Max(0.0, TransitionSpeed));
            Offset = Offset.MoveToward(VirtualCamera.Offset, (float)delta * (float)Math.Max(0.0, TransitionSpeed));
            var halfBounds = GetViewportRect().Size / Zoom / 2;
            var topLeft = new Vector2(VirtualCamera.LimitLeft, VirtualCamera.LimitTop);
            var bottomRight = new Vector2(VirtualCamera.LimitRight, VirtualCamera.LimitBottom);
            topLeft += VirtualCamera.GlobalPosition + halfBounds - Offset;
            bottomRight += VirtualCamera.GlobalPosition - halfBounds - Offset;
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
