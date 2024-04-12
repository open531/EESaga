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
    BinaryTree,
    CapacityGreen,
    CapacityCylinder,
    Chip,
    CrystalTurtle,
    FourierLeaf,
    PerceptronKun,
    Python,
    SchodingerCat,
    Shanloong,
}
