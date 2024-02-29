namespace EESaga.Scripts.Entities.BattleEnemies;

using Godot;

public partial class Slime : BattleEnemy
{
    public static new Slime Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/slime.tscn").Instantiate<Slime>();

    public override void _Ready()
    {
        base._Ready();
        PieceName = "SLIME";
        HealthMax = 10;
        Health = HealthMax;
    }
}
