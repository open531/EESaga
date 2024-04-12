namespace EESaga.Scripts.Utilities.Cameras;

using Godot;

public partial class CameraHost : Node2D
{
    [Export] public Vector2 Offset { get; set; } = Vector2.Zero;
    [Export] public Vector2 Zoom { get; set; } = Vector2.One;
    [ExportGroup("Limit", "Limit")]
    [Export] public float LimitLeft { get; set; } = -352.0f;
    [Export] public float LimitRight { get; set; } = 352.0f;
    [Export] public float LimitTop { get; set; } = -198.0f;
    [Export] public float LimitBottom { get; set; } = 198.0f;
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
