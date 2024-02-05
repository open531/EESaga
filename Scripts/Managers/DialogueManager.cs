namespace EESaga.Scripts.Managers;

using Godot;
using System.Collections.Generic;
using UI;

/// <summary>
/// Adapted from GDScript of https://worldeater-dev.itch.io/bittersweet-birthday/devlog/224241/howto-a-simple-dialogue-system-in-godot
/// </summary>
public partial class DialogueManager : Node
{
    private static readonly PackedScene _dialogueScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/Dialogue.tscn");
    public Dialogue Dialogue { get; set; }

    private List<DialogueMessage> _dialogueMessages = [];
    private int _dialogueIndex = 0;
    private bool _isActive = false;

    [Signal] public delegate void MessageRequestedEventHandler();
    [Signal] public delegate void MessageCompletedEventHandler();
    [Signal] public delegate void FinishedEventHandler();

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.Space)
            {
                if (_isActive && Dialogue.MessageIsFullyVisible)
                {
                    if (_dialogueIndex < _dialogueMessages.Count - 1)
                    {
                        _dialogueIndex += 1;
                        ShowCurrent();
                    }
                    else
                    {
                        Hide();
                    }
                }
            }
        }
    }

    public void ShowMessages(List<DialogueMessage> dialogueMessages)
    {
        if (_isActive)
        {
            return;
        }
        _isActive = true;
        _dialogueMessages = dialogueMessages;
        _dialogueIndex = 0;
        var dialogue = _dialogueScene.Instantiate<Dialogue>();
        dialogue.MessageCompleted += OnDialogueMessageCompleted;
        AddChild(dialogue);
        Dialogue = dialogue;
        ShowCurrent();
    }

    private void ShowCurrent()
    {
        EmitSignal(nameof(MessageRequested));
        var message = _dialogueMessages[_dialogueIndex];
        Dialogue.UpdateDialogue(message);
    }

    private void OnDialogueMessageCompleted()
    {
        EmitSignal(nameof(MessageCompleted));
    }

    private void Hide()
    {
        Dialogue.MessageCompleted -= OnDialogueMessageCompleted;
        Dialogue.QueueFree();
        Dialogue = null;
        _isActive = false;
        EmitSignal(nameof(Finished));
    }
}
