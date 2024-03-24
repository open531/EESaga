namespace EESaga.Scripts.Data;

using EESaga.Scripts.Entities.BattleEnemies;
using Godot;
using System.Collections.Generic;

public static class GameData
{
    public static List<EnemyInfo> enemyInfo = [
        new EnemyInfo(
                       new List<EnemyType> { EnemyType.Slime},
                                  new List<int> { 3}
                                         ),
        ];
}

public struct EnemyInfo
{
    public Dictionary<EnemyType, int> EnemyCountDic { get; set; }
    public EnemyInfo(List<EnemyType> enemyTypes, List<int> enemyCount)
    {
        EnemyCountDic = new Dictionary<EnemyType, int>();
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            EnemyCountDic.Add(enemyTypes[i], enemyCount[i]);
        }
    }
}
