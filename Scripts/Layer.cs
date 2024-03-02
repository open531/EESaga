namespace EESaga.Scripts;

using Godot;

public partial class Layer : CanvasLayer
{
    private ColorRect _blackRect;

    [Signal] public delegate void FadedInEventHandler();
    [Signal] public delegate void FadedOutEventHandler();

    public static Layer Instance() => GD.Load<PackedScene>("res://Scenes/layer.tscn").Instantiate<Layer>();

    public override void _Ready()
    {
        _blackRect = GetNode<ColorRect>("BlackRect");
        _blackRect.Color = new Color(0, 0, 0, 0);
    }

    public void FadeIn(float duration = 0.5f)
    {
        var tween = CreateTween();
        tween.TweenProperty(_blackRect, "color", new Color(0, 0, 0, 1), duration);
        tween.Finished += () => EmitSignal(SignalName.FadedIn);
    }

    public void FadeOut(float duration = 0.5f)
    {
        var tween = CreateTween();
        tween.TweenProperty(_blackRect, "color", new Color(0, 0, 0, 0), duration);
        tween.Finished += () => EmitSignal(SignalName.FadedOut);
    }

    public async void FadeInAndOut(float duration = 0.5f)
    {
        var fadeInTween = CreateTween();
        fadeInTween.TweenProperty(_blackRect, "color", new Color(0, 0, 0, 1), duration);
        await ToSignal(fadeInTween, Tween.SignalName.Finished);
        EmitSignal(SignalName.FadedIn);
        var fadeOutTween = CreateTween();
        fadeOutTween.TweenProperty(_blackRect, "color", new Color(0, 0, 0, 0), duration);
        await ToSignal(fadeOutTween, Tween.SignalName.Finished);
        EmitSignal(SignalName.FadedOut);
    }
}
