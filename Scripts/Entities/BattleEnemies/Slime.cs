namespace EESaga.Scripts.Entities.BattleEnemies;

using EESaga.Scripts.Entities.BattleParties;
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
        Agility = 1;
        MoveRange = 3;
        AttackTimes = 1;
        AttackDamage = 10;
        Shield = 0;
    }

    public override void Attack(BattleParty battleParty)
    {
        for (var i = 0; i < AttackTimes; i++)
        {
            battleParty.BeAttacked(AttackDamage);
        }
        battleParty.CheckDeath();
    }

    public override void Defend(BattleEnemy battleEnemy)
    {
        battleEnemy.Shield += 3;
    }
}
