namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

/// <summary>
/// Adapted from GDScript of https://worldeater-dev.itch.io/bittersweet-birthday/devlog/224241/howto-a-simple-dialogue-system-in-godot
/// </summary>
public partial class Dialogue : Control
{
    private Label _speakerLabel;
    private RichTextLabel _dialogueLabel;
    private AnimatedSprite2D _speakerImage;
    private Timer _typeTimer;
    private Timer _pauseTimer;
    private DialogueHelper _dialogueHelper;

    public bool MessageIsFullyVisible => _dialogueLabel.VisibleCharacters >= _dialogueLabel.Text.Length - 1;

    [Signal] public delegate void MessageCompletedEventHandler();

    public static Dialogue Instance() => GD.Load<PackedScene>("res://Scenes/UI/dialogue.tscn").Instantiate<Dialogue>();

    public override void _Ready()
    {
        _speakerLabel = GetNode<Label>("%SpeakerLabel");
        _dialogueLabel = GetNode<RichTextLabel>("%DialogueLabel");
        _speakerImage = GetNode<AnimatedSprite2D>("%SpeakerImage");
        _typeTimer = GetNode<Timer>("TypeTimer");
        _pauseTimer = GetNode<Timer>("PauseTimer");
        _dialogueHelper = GetNode<DialogueHelper>("DialogueHelper");

        _typeTimer.Timeout += OnTypeTimerTimeout;
        _pauseTimer.Timeout += OnPauseTimerTimeout;
        _dialogueHelper.PauseRequested += OnDialogueHelperPauseRequested;
    }

    public void UpdateDialogue(DialogueMessage dialogueMessage)
    {
        _dialogueLabel.Text = _dialogueHelper.ExtractPauses(dialogueMessage.Message);
        _dialogueLabel.VisibleCharacters = 0;
        _speakerLabel.Text = dialogueMessage.Speaker;
        _speakerImage.SpriteFrames = dialogueMessage.SpeakerImage;
        if (_speakerImage.SpriteFrames != null)
        {
            _speakerImage.Play("move");
        }
        _typeTimer.Start();
    }

    private void OnTypeTimerTimeout()
    {
        _dialogueHelper.CheckAtPosition(_dialogueLabel.VisibleCharacters);
        if (_dialogueLabel.VisibleCharacters < Tr(_dialogueLabel.Text).Length)
        {
            _dialogueLabel.VisibleCharacters += 1;
        }
        else
        {
            _typeTimer.Stop();
        }
    }

    private void OnPauseTimerTimeout()
    {
        _typeTimer.Start();
    }

    private void OnDialogueHelperPauseRequested(float duration)
    {
        _pauseTimer.WaitTime = duration;
        _pauseTimer.Start();
    }
}

public struct DialogueMessage(string speaker, string message, SpriteFrames speakerImage = null)
{
    public string Speaker { get; set; } = speaker;
    public string Message { get; set; } = message;
    public SpriteFrames SpeakerImage { get; set; } = speakerImage;
}
