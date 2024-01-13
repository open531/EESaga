namespace EESaga.Scripts.Managers;

using Godot;
using System.Collections.Generic;
using UI;

public partial class DialogueManager : Node
{
    public PackedScene DialogueScene { get; set; } = ResourceLoader.Load<PackedScene>("res://Scenes/UI/Dialogue.tscn");
    public Dialogue DialogueInstance { get; set; }

    private List<DialogueMessage> _dialogueMessages = [];
    private int _dialogueIndex = 0;
    private bool _isActive = false;

    [Signal] public delegate void MessageRequestedEventHandler();
    [Signal] public delegate void MessageCompletedEventHandler();
    [Signal] public delegate void FinishedEventHandler();

    public override void _Input(InputEvent @event)
    {
        if (@event.IsPressed() && 
            !@event.IsEcho() && 
            @event is InputEventKey && 
            (@event as InputEventKey).GetKeycodeWithModifiers() == Key.Space &&
            _isActive &&
            DialogueInstance.MessageIsFullyVisible)
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

    public void ShowMessages(List<DialogueMessage> dialogueMessages)
    {
        if (_isActive)
        {
            return;
        }
        _isActive = true;
        _dialogueMessages = dialogueMessages;
        _dialogueIndex = 0;
        var dialogue = DialogueScene.Instantiate<Dialogue>();
        dialogue.MessageCompleted += OnMessageCompleted;
        GetTree().Root.AddChild(dialogue);
        DialogueInstance = dialogue;
        ShowCurrent();
    }

    private void ShowCurrent()
    {
        EmitSignal(nameof(MessageRequested));
        var message = _dialogueMessages[_dialogueIndex];
        DialogueInstance.UpdateDialogue(message);
    }

    private void OnMessageCompleted()
    {
        EmitSignal(nameof(MessageCompleted));
    }

    private void Hide()
    {
        DialogueInstance.MessageCompleted -= OnMessageCompleted;
        DialogueInstance.QueueFree();
        DialogueInstance = null;
        _isActive = false;
        EmitSignal(nameof(Finished));
    }
}
