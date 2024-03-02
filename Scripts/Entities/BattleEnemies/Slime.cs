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
        AttackDamage = 2;
        Shield = 0;
    }

    public void Attack(BattleParty battleParty)
    {
        for (var i = 0; i < AttackTimes; i++)
        {
            battleParty.Health -= AttackDamage;
            GD.Print($"{battleParty.PieceName} 受到了 {AttackDamage} 点伤害");
            if (battleParty.Health == 0)
            {
                GD.Print($"{battleParty.PieceName} 用他的牺牲，为队友争取了时间！");
                break;
            }
        }
    }

    public void Defense(BattleEnemy battleEnemy)
    {
        battleEnemy.Shield += 1;
    }
}
