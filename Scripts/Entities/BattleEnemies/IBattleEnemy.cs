namespace EESaga.Scripts.Entities.BattleEnemies;

using Godot;

public interface IBattleEnemy : IBattlePiece
{
    public int AttackDamage { get; set; }
    public int AttackTimes { get; set; }
    public void Initialize(IBattleEnemy enemy);
}

public enum EnemyType
{
    Null,
    Slime,
    CapacityGreen,
    CapacityCylinder,
    Chip,
    FourierLeaf,
    PerceptronKun,
    Python
}
