using Godot;
using System;

public partial class CardsSeletion : Node2D
{
    public static new CardsSeletion Instance() => GD.Load<PackedScene>("res://Scenes/UI/cards_selection.tscn").Instantiate<CardsSeletion>();
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
