namespace EESaga.Scripts.Data;

using EESaga.Scripts.Entities.BattleEnemies;
using Godot;
using System.Collections.Generic;

public static class GameData
{
    public static List<EnemyInfo> enemyInfo = [new EnemyInfo([EnemyType.Slime], [3]),];
}

public struct EnemyInfo
{
    public Dictionary<EnemyType, int> EnemyCountDic { get; set; }
    public EnemyInfo(List<EnemyType> enemyTypes, List<int> enemyCount)
    {
        EnemyCountDic = [];
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            EnemyCountDic.Add(enemyTypes[i], enemyCount[i]);
        }
    }
}
