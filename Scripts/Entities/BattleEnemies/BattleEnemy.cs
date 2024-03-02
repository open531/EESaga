namespace EESaga.Scripts.Entities.BattleEnemies;

using EESaga.Scripts.Entities.BattleParties;
using EESaga.Scripts.Maps;
using Godot;
using Utilities;

public partial class BattleEnemy : BattlePiece, IBattleEnemy
{
    public int AttackDamage { get; set; }
    public int AttackTimes { get; set; }
    public static new BattleEnemy Instance() => GD.Load<PackedScene>("res://Scenes/Entities/BattleEnemies/battle_enemy.tscn").Instantiate<BattleEnemy>();

    public virtual void Attack(BattleParty battleParty){}
    public virtual void Defense(BattleEnemy battleEnemy) { }
    public void Initialize(IBattleEnemy enemy)
    {
        Level = enemy.Level;
        HealthMax = enemy.HealthMax;
        Health = enemy.Health;
        Shield = enemy.Shield;
        Agility = enemy.Agility;
        MoveRange = enemy.MoveRange;
        AttackDamage = enemy.AttackDamage;
        AttackTimes = enemy.AttackTimes;
    }
}
