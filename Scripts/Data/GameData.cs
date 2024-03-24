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
        new EnemyInfo(
                        new List<EnemyType> {EnemyType.Slime},
                        new List<int> { 5})
        ];

    public static List<MapInfo> mapInfo = [
        new MapInfo(
                       new List<Vector2I>{new Vector2I(0,0),new Vector2I(10,10)},
                       new List<Vector2I>{new Vector2I(3,3),new Vector2I(4,5)}
                                         ),
        new MapInfo(
                       new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
                       new List<Vector2I>{
                            new Vector2I(1,2),new Vector2I(3,4),
                            new Vector2I(5,5),new Vector2I(7,7)}
                                         ),
        ];
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

public struct MapInfo
{
    public List<Vector2I> AvailableCells { get; set; }
    public List<Vector2I> CellsOutOfMap { get; set; }
    public MapInfo(List<Vector2I> availableCells, List<Vector2I> cellsOutOfMap)
    {
        AvailableCells = availableCells;
        CellsOutOfMap = cellsOutOfMap;
    }
}
