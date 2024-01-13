namespace EESaga.Scripts.Utilities;

using Godot;
using System;
using System.Collections.Generic;

public partial class DialogueHelper : Node
{
    private List<Pause> _pauses = [];

    private RegEx _bbCodeERegex = new();
    private RegEx _bbCodeIRegex = new();
    private RegEx _customTagRegex = new();
    private RegEx _floatRegex = new();
    private RegEx _pauseRegex = new();

    private const string BBCODE_E_PATTERN = "\\[\\/(.*?)\\]";
    private const string BBCODE_I_PATTERN = "\\[(?!\\/)(.*?)\\]";
    private const string CUSTOM_TAG_PATTERN = "({(.*?)})";
    private const string FLOAT_PATTERN = "\\d+\\.\\d+";
    private const string PAUSE_PATTERN = "({p=\\d([.]\\d+)?[}])";

    [Signal] public delegate void PauseRequestedEventHandler(float duration);

    public override void _Ready()
    {
        _bbCodeERegex.Compile(BBCODE_E_PATTERN);
        _bbCodeIRegex.Compile(BBCODE_I_PATTERN);
        _customTagRegex.Compile(CUSTOM_TAG_PATTERN);
        _floatRegex.Compile(FLOAT_PATTERN);
        _pauseRegex.Compile(PAUSE_PATTERN);
    }

    public string ExtractPauses(string source)
    {
        _pauses.Clear();
        FindPauses(source);
        return ExtractTags(source);
    }

    public void CheckAtPosition(int position)
    {
        foreach (var pause in _pauses)
        {
            if (pause.PausePosition == position)
            {
                EmitSignal(nameof(PauseRequestedEventHandler), pause.PauseDuration);
            }
        }
    }

    private void FindPauses(string source)
    {
        var foundPauses = _pauseRegex.SearchAll(source);
        foreach (var result in foundPauses)
        {
            _pauses.Add(new Pause(AdjustPosition(result.GetStart(), source), result.GetString()));
        }
    }

    private static string ExtractTags(string source)
    {
        var customRegex = new RegEx();
        customRegex.Compile("({(.*?)})");
        return customRegex.Sub(source, "", true);
    }

    private int AdjustPosition(int position, string source)
    {
        var newPosition = position;
        var left = source.Left(position);
        var prevTags = _customTagRegex.SearchAll(left);
        foreach (var tag in prevTags)
        {
            newPosition -= tag.GetString().Length;
        }
        var prevBBCodesStart = _bbCodeIRegex.SearchAll(left);
        foreach (var tag in prevBBCodesStart)
        {
            newPosition -= tag.GetString().Length;
        }
        var prevBBCodesEnd = _bbCodeERegex.SearchAll(left);
        foreach (var tag in prevBBCodesEnd)
        {
            newPosition -= tag.GetString().Length;
        }
        return newPosition;
    }
}

public struct Pause
{
    private const string FLOAT_PATTERN = "\\d+\\.\\d+";
    public int PausePosition { get; set; }
    public float PauseDuration { get; set; }

    public Pause(int position, string tagString)
    {
        var durationRegex = new RegEx();
        durationRegex.Compile(FLOAT_PATTERN);
        PauseDuration = float.Parse(durationRegex.Search(tagString).GetString());
        PausePosition = Math.Clamp(position - 1, 0, Math.Abs(position));
    }
}