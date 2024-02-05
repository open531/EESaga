namespace EESaga.Scripts.Entities.BattleEnemies;

using Godot;

public interface IBattleEnemy : IBattlePiece
{
    public void Initialize(IBattleEnemy enemy);
}
