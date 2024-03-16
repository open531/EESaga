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

    public override BattleParty Attack(BattleParty battleParty)
    {
        for (var i = 0; i < AttackTimes; i++)
        {
            var dif = -battleParty.Shield + AttackDamage;
            var shield = battleParty.Shield;
            if (battleParty.Shield != 0)
            {
                battleParty.Shield -= AttackDamage;
            }
            if (battleParty.Shield <= 0)
            {
                battleParty.Shield = 0;
                battleParty.Health -= dif;
                GD.Print($"{battleParty.Name} 护盾值削减了 {shield} 点");
                GD.Print($"{battleParty.Name} 受到了 {dif} 点伤害");
            }
            else
            {
                GD.Print($"{battleParty.Name} 护盾值削减了 {AttackDamage} 点");
            }
            if (battleParty.Health == 0)
            {
                battleParty.QueueFree();
                GD.Print($"{battleParty.PieceName} 用他的牺牲，为队友争取了时间！");
                break;
            }
        }
        if(battleParty.Health > 0)
        {
            GD.Print($"{battleParty.Name} 还剩下 {battleParty.Shield} 点护盾值");
            GD.Print($"{battleParty.Name} 还剩下 {battleParty.Health} 点生命值");
            return null;
        }
        else
        {
            return battleParty;
        }
    }

    public override void Defend(BattleEnemy battleEnemy)
    {
        battleEnemy.Shield += 3;
    }
}
