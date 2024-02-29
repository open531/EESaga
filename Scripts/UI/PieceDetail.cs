namespace EESaga.Scripts.UI;

using Entities;
using Godot;

public partial class PieceDetail : Control
{
    private Label _pieceName;
    private Label _pieceHealth;

    public static PieceDetail Instance() => GD.Load<PackedScene>("res://Scenes/UI/piece_detail.tscn").Instantiate<PieceDetail>();

    public override void _Ready()
    {
        _pieceName = GetNode<Label>("%PieceName");
        _pieceHealth = GetNode<Label>("%PieceHealth");

        _pieceName.Text = "";
        _pieceHealth.Text = "";
    }

    public void Update(BattlePiece piece)
    {
        if (piece == null)
        {
            _pieceName.Text = "";
            _pieceHealth.Text = "";
        }
        else
        {
            _pieceName.Text = Tr(piece.PieceName);
            _pieceHealth.Text = piece.Health.ToString() + "/" + piece.HealthMax.ToString();
        }
    }
}
