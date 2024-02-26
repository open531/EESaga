namespace EESaga.Scripts.Entities.BattleEnemies;

using Godot;

public partial class Slime : BattleEnemy
{
    public override int Health { get; set; } = 10;
    public override int HealthMax { get; set; } = 10;
    public static new Slime Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/slime.tscn").Instantiate<Slime>();
}
