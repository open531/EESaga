namespace EESaga.Scripts.UI;

using Godot;
using Utilities;

public partial class Dialogue : Control
{
    private Label _speakerLabel;
    private RichTextLabel _dialogueLabel;
    private TextureRect _speakerImage;
    private Timer _typeTimer;
    private Timer _pauseTimer;
    private DialogueHelper _dialogueHelper;

    public bool MessageIsFullyVisible => _dialogueLabel.VisibleCharacters >= _dialogueLabel.Text.Length - 1;

    [Signal] public delegate void MessageCompletedEventHandler();

    public override void _Ready()
    {
        _speakerLabel = GetNode<Label>("%SpeakerLabel");
        _dialogueLabel = GetNode<RichTextLabel>("%DialogueLabel");
        _speakerImage = GetNode<TextureRect>("%SpeakerImage");
        _typeTimer = GetNode<Timer>("TypeTimer");
        _pauseTimer = GetNode<Timer>("PauseTimer");
        _dialogueHelper = GetNode<DialogueHelper>("DialogueHelper");

        _typeTimer.Timeout += OnTypeTimerTimeout;
        _pauseTimer.Timeout += OnPauseTimerTimeout;
        _dialogueHelper.PauseRequested += OnDialogueHelperPauseRequested;

        UpdateDialogue(new DialogueMessage("EESaga",
            "你说的对，但是《原神》是由米哈游自主研发的一款全新开放世界冒险游戏。游戏发生在一个被称作「提瓦特」的幻想世界，在这里，被神选中的人将被授予「神之眼」，导引元素之力。"));
    }

    public void UpdateDialogue(DialogueMessage dialogueMessage)
    {
        _dialogueLabel.Text = _dialogueHelper.ExtractPauses(dialogueMessage.Message);
        _dialogueLabel.VisibleCharacters = 0;
        _speakerLabel.Text = dialogueMessage.Speaker;
        _speakerImage.Texture = dialogueMessage.SpeakerImage;
        _typeTimer.Start();
    }

    private void OnTypeTimerTimeout()
    {
        _dialogueHelper.CheckAtPosition(_dialogueLabel.VisibleCharacters);
        if (_dialogueLabel.VisibleCharacters < _dialogueLabel.Text.Length)
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

public struct DialogueMessage(string speaker, string message, Texture2D speakerImage = null)
{
    public string Speaker { get; set; } = speaker;
    public string Message { get; set; } = message;
    public Texture2D SpeakerImage { get; set; } = speakerImage;
}
