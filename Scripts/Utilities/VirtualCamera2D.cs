namespace EESaga.Scripts.Utilities;

using Godot;

/// <summary>
/// <para>
/// Node that represents a virtual camera. A scene may have several virtual cameras, one of them
/// can be assigned to a CinematicCamera2D node to make the camera copy virtual camera's properties.
/// </para>
/// <para>
/// Adapted from GDScript of https://github.com/HexagonNico/CinematicCamera2D
/// </para>
/// </summary>
[Tool]
public partial class VirtualCamera2D : Node2D
{
    /// <summary>
    /// The camera's relative offset.
    /// </summary>
    [Export] public Vector2 Offset { get; set; } = Vector2.Zero;

    /// <summary>
    /// The camera's zoom.
    /// </summary>
    [Export] public Vector2 Zoom { get; set; } = Vector2.One;

    /// <summary>
    /// Left scroll limit in pixels. The camera stops moving when reaching this value.
    /// </summary>
    [ExportGroup("Limit", "Limit")]
    [Export] public int LimitLeft { get; set; } = -10000000;
    /// <summary>
    /// Top scroll limit in pixels. The camera stops moving when reaching this value.
    /// </summary>
    [Export] public int LimitRight { get; set; } = -10000000;
    /// <summary>
    /// Right scroll limit in pixels. The camera stops moving when reaching this value.
    /// </summary>
    [Export] public int LimitTop { get; set; } = 10000000;
    /// <summary>
    /// Bottom scroll limit in pixels. The camera stops moving when reaching this value.
    /// </summary>
    [Export] public int LimitBottom { get; set; } = 10000000;

    /// <summary>
    /// If true, draws the camera's limits rectangle in the editor.
    /// </summary>
    [ExportGroup("Editor", "Editor")]
    [Export] public bool EditorDrawLimits { get; set; } = true;

    public override void _Draw()
    {
        if (Engine.IsEditorHint())
        {
            if (EditorDrawLimits)
            {
                var rect = new Rect2(LimitRight, LimitTop, LimitLeft - LimitRight, LimitBottom - LimitTop);
                DrawRect(rect, Colors.Yellow, false);
            }
        }
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
        }
    }
}
