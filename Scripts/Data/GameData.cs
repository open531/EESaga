using EESaga.Scripts.Cards;

namespace EESaga.Scripts.Data;

using EESaga.Scripts.Cards.CardAttacks;
using EESaga.Scripts.Cards.CardDefenses;
using EESaga.Scripts.Entities.BattleEnemies;
using Godot;
using System.Collections.Generic;

public static class GameData
{
    #region EnemyInfo
    public static List<EnemyInfo> enemyInfo = [

        #region 程序设计

        new EnemyInfo(
            new List<EnemyType> {EnemyType.Python},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Python},
            new List<int> { 10}),

        #endregion

        #region 电子电路
        new EnemyInfo(
            new List<EnemyType>{EnemyType.CapacityCylinder},
            new List<int>{1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 1, 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 1, 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 2, 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 2, 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 3, 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 3, 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 4, 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 4,5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.CapacityCylinder, EnemyType.CapacityGreen},
            new List<int> { 5,5}),

        #endregion

        #region 数据算法

        new EnemyInfo(
            new List<EnemyType> {EnemyType.BinaryTree},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.BinaryTree},
            new List<int> { 10}),

        #endregion

        #region 数字逻辑
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Chip},
            new List<int> { 10}),

        #endregion

        #region 概率随机
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SchrodingerCat},
            new List<int> { 10}),

        #endregion

        #region 信号系统
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.FourierLeaf},
            new List<int> { 10}),

        #endregion

        #region 电磁场波

        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Make},
            new List<int> { 10}),

        #endregion

        #region 通信网络

        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.Shanloong},
            new List<int> { 10}),

        #endregion

        #region 媒体认知

        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.PerceptronKun},
            new List<int> { 10}),

        #endregion

        #region 固体物理

        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 1}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 2}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 3}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 4}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 5}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 6}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 7}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 8}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 9}),
        new EnemyInfo(
            new List<EnemyType> { EnemyType.SiliconTurtle},
            new List<int> { 10}),

        #endregion

        #region 紫荆之巅

        new EnemyInfo(
            new List<EnemyType> { EnemyType.Slime},
            new List<int> { 1})
        #endregion
    ];
    #endregion

    #region MapData
    public static List<MapInfo> mapInfo = [
    // 地图 1
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2)}
    ),
    // 地图 2
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(5,5),new Vector2I(5,6),
                            new Vector2I(6,5),new Vector2I(6,6)}
    ),
    // 地图 3
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5)}
    ),
    // 地图 4
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8)}
    ),
    // 地图 5
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3)}
    ),
    // 地图 6
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11)}
    ),
    // 地图 7
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6)}
    ),
    // 地图 8
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                                                        new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9)}
    ),
    // 地图 9
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10)}
    ),
    // 地图 10
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11)}
    ),
    // 地图 11
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,5),
                            new Vector2I(8,6),new Vector2I(3,8),
                            new Vector2I(3,9),new Vector2I(4,8),
                            new Vector2I(4,9),new Vector2I(2,11),
                            new Vector2I(2,10),new Vector2I(3,11),
                            new Vector2I(3,10),new Vector2I(8,10),
                            new Vector2I(8,11),new Vector2I(9,10),
                            new Vector2I(9,11),new Vector2I(5,2),
                            new Vector2I(6,2),new Vector2I(5,3),
                            new Vector2I(6,3),new Vector2I(8,1),
                            new Vector2I(9,1),new Vector2I(8,0),
                            new Vector2I(9,0)}
    ),
    // 地图 12
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11),
                            new Vector2I(5,2),new Vector2I(6,2),
                            new Vector2I(5,3),new Vector2I(6,3),
                            new Vector2I(8,1),new Vector2I(9,1),
                            new Vector2I(8,0),new Vector2I(9,0),
                            new Vector2I(10,5),new Vector2I(10,6),
                            new Vector2I(11,5),new Vector2I(11,6)}
    ),
    // 地图 13
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11),
                            new Vector2I(5,2),new Vector2I(6,2),new Vector2I(5,3),new Vector2I(6,3),
                            new Vector2I(8,1),new Vector2I(9,1),
                            new Vector2I(8,0),new Vector2I(9,0),
                            new Vector2I(10,5),new Vector2I(10,6),
                            new Vector2I(11,5),new Vector2I(11,6),
                            new Vector2I(0,7),new Vector2I(1,7),
                            new Vector2I(0,8),new Vector2I(1,8)}
    ),
    // 地图 14
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11),
                            new Vector2I(5,2),new Vector2I(6,2),
                            new Vector2I(5,3),new Vector2I(6,3),
                            new Vector2I(8,1),new Vector2I(9,1),
                            new Vector2I(8,0),new Vector2I(9,0),
                            new Vector2I(10,5),new Vector2I(10,6),
                            new Vector2I(11,5),new Vector2I(11,6),
                            new Vector2I(0,7),new Vector2I(1,7),
                            new Vector2I(0,8),new Vector2I(1,8),
                            new Vector2I(5,8),new Vector2I(6,8),
                            new Vector2I(5,9),new Vector2I(6,9)}
    ),
    // 地图 15
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11),
                            new Vector2I(5,2),new Vector2I(6,2),
                            new Vector2I(5,3),new Vector2I(6,3),
                            new Vector2I(8,1),new Vector2I(9,1),
                            new Vector2I(8,0),new Vector2I(9,0),
                            new Vector2I(10,5),new Vector2I(10,6),
                            new Vector2I(11,5),new Vector2I(11,6),
                            new Vector2I(0,7),new Vector2I(1,7),
                            new Vector2I(0,8),new Vector2I(1,8),
                            new Vector2I(5,8),new Vector2I(6,8),
                            new Vector2I(5,9),new Vector2I(6,9),
                            new Vector2I(10,8),new Vector2I(11,8),
                            new Vector2I(10,9),new Vector2I(11,9)}
    ),
    // 地图 16
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
        new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                            new Vector2I(2,1),new Vector2I(2,2),
                            new Vector2I(3,4),new Vector2I(3,5),
                            new Vector2I(4,4),new Vector2I(4,5),
                            new Vector2I(6,7),new Vector2I(6,8),
                            new Vector2I(7,7),new Vector2I(7,8),
                            new Vector2I(9,2),new Vector2I(9,3),
                            new Vector2I(10,2),new Vector2I(10,3),
                            new Vector2I(4,10),new Vector2I(5,10),
                            new Vector2I(4,11),new Vector2I(5,11),
                            new Vector2I(7,5),new Vector2I(8,5),
                            new Vector2I(7,6),new Vector2I(8,6),
                            new Vector2I(3,8),new Vector2I(3,9),
                            new Vector2I(4,8),new Vector2I(4,9),
                            new Vector2I(2,11),new Vector2I(2,10),
                            new Vector2I(3,11),new Vector2I(3,10),
                            new Vector2I(8,10),new Vector2I(8,11),
                            new Vector2I(9,10),new Vector2I(9,11),
                            new Vector2I(5,2),new Vector2I(6,2),
                            new Vector2I(5,3),new Vector2I(6,3),
                            new Vector2I(8,1),new Vector2I(9,1),
                            new Vector2I(8,0),new Vector2I(9,0),
                            new Vector2I(10,5),new Vector2I(10,6),
                            new Vector2I(11,5),new Vector2I(11,6),
                            new Vector2I(0,7),new Vector2I(1,7),
                            new Vector2I(0,8),new Vector2I(1,8),
                            new Vector2I(5,8),new Vector2I(6,8),
                            new Vector2I(5,9),new Vector2I(6,9),
                            new Vector2I(10,8),new Vector2I(11,8),
                            new Vector2I(10,9),new Vector2I(11,9),
                            new Vector2I(3,1),new Vector2I(3,0),
                            new Vector2I(4,1),new Vector2I(4,0)}
    ),
    // 地图 17
    // 地图 17
new MapInfo(
    new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
    new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                        new Vector2I(2,1),new Vector2I(2,2),
                        new Vector2I(3,4),new Vector2I(3,5),
                        new Vector2I(4,4),new Vector2I(4,5),
                        new Vector2I(6,7),new Vector2I(6,8),
                        new Vector2I(7,7),new Vector2I(7,8),
                        new Vector2I(9,2),new Vector2I(9,3),
                        new Vector2I(10,2),new Vector2I(10,3),
                        new Vector2I(4,10),new Vector2I(5,10),
                        new Vector2I(4,11),new Vector2I(5,11),
                        new Vector2I(7,5),new Vector2I(8,5),
                        new Vector2I(7,6),new Vector2I(8,6),
                        new Vector2I(3,8),new Vector2I(3,9),
                        new Vector2I(4,8),new Vector2I(4,9),
                        new Vector2I(2,11),new Vector2I(2,10),
                        new Vector2I(3,11),new Vector2I(3,10),
                        new Vector2I(8,10),new Vector2I(8,11),
                        new Vector2I(9,10),new Vector2I(9,11),
                        new Vector2I(5,2),new Vector2I(6,2),
                        new Vector2I(5,3),new Vector2I(6,3),
                        new Vector2I(8,1),new Vector2I(9,1),
                        new Vector2I(8,0),new Vector2I(9,0),
                        new Vector2I(10,5),new Vector2I(10,6),
                        new Vector2I(11,5),new Vector2I(11,6),
                        new Vector2I(0,7),new Vector2I(1,7),
                        new Vector2I(0,8),new Vector2I(1,8),
                        new Vector2I(5,8),new Vector2I(6,8),
                        new Vector2I(5,9),new Vector2I(6,9),
                        new Vector2I(10,8),new Vector2I(11,8),
                        new Vector2I(10,9),new Vector2I(11,9),
                        new Vector2I(3,1),new Vector2I(3,0),
                        new Vector2I(4,1),new Vector2I(4,0),
                        new Vector2I(1,6),new Vector2I(1,7),
                        new Vector2I(2,6),new Vector2I(2,7),
                        new Vector2I(6,10),new Vector2I(6,11),
                        new Vector2I(7,10),new Vector2I(7,11)}
),
// 地图 18
new MapInfo(
    new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
    new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                        new Vector2I(2,1),new Vector2I(2,2),
                        new Vector2I(3,4),new Vector2I(3,5),
                        new Vector2I(4,4),new Vector2I(4,5),
                        new Vector2I(6,7),new Vector2I(6,8),
                        new Vector2I(7,7),new Vector2I(7,8),
                        new Vector2I(9,2),new Vector2I(9,3),
                        new Vector2I(10,2),new Vector2I(10,3),
                        new Vector2I(4,10),new Vector2I(5,10),
                        new Vector2I(4,11),new Vector2I(5,11),
                        new Vector2I(7,5),new Vector2I(8,5),
                        new Vector2I(7,6),new Vector2I(8,6),
                        new Vector2I(3,8),new Vector2I(3,9),
                        new Vector2I(4,8),new Vector2I(4,9),
                        new Vector2I(2,11),new Vector2I(2,10),
                        new Vector2I(3,11),new Vector2I(3,10),
                        new Vector2I(8,10),new Vector2I(8,11),
                        new Vector2I(9,10),new Vector2I(9,11),
                        new Vector2I(5,2),new Vector2I(6,2),
                        new Vector2I(5,3),new Vector2I(6,3),
                        new Vector2I(8,1),new Vector2I(9,1),
                        new Vector2I(8,0),new Vector2I(9,0),
                        new Vector2I(10,5),new Vector2I(10,6),
                        new Vector2I(11,5),new Vector2I(11,6),
                        new Vector2I(0,7),new Vector2I(0,8),
                        new Vector2I(1,8),
                        new Vector2I(5,8),new Vector2I(6,8),
                        new Vector2I(5,9),new Vector2I(6,9),
                        new Vector2I(10,8),new Vector2I(11,8),
                        new Vector2I(10,9),new Vector2I(11,9),
                        new Vector2I(3,1),new Vector2I(3,0),
                        new Vector2I(4,1),new Vector2I(4,0),
                        new Vector2I(1,6),new Vector2I(1,7),
                        new Vector2I(2,6),new Vector2I(2,7),
                        new Vector2I(6,10),new Vector2I(6,11),
                        new Vector2I(7,10),new Vector2I(7,11),
                        new Vector2I(11,1),new Vector2I(11,0)}
),
// 地图 19
new MapInfo(
    new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
    new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                        new Vector2I(2,1),new Vector2I(2,2),
                        new Vector2I(3,4),new Vector2I(3,5),
                        new Vector2I(4,4),new Vector2I(4,5),
                        new Vector2I(6,7),new Vector2I(6,8),
                        new Vector2I(7,7),new Vector2I(7,8),
                        new Vector2I(9,2),new Vector2I(9,3),
                        new Vector2I(10,2),new Vector2I(10,3),
                        new Vector2I(4,10),new Vector2I(5,10),
                        new Vector2I(4,11),new Vector2I(5,11),
                        new Vector2I(7,5),new Vector2I(8,5),
                        new Vector2I(7,6),new Vector2I(8,6),
                        new Vector2I(3,8),new Vector2I(3,9),
                        new Vector2I(4,8),new Vector2I(4,9),
                        new Vector2I(2,11),new Vector2I(2,10),
                        new Vector2I(3,11),new Vector2I(3,10),
                        new Vector2I(8,10),new Vector2I(8,11),
                        new Vector2I(9,10),new Vector2I(9,11),
                        new Vector2I(5,2),new Vector2I(6,2),
                        new Vector2I(5,3),new Vector2I(6,3),
                        new Vector2I(8,1),new Vector2I(9,1),
                        new Vector2I(8,0),new Vector2I(9,0),
                        new Vector2I(10,5),new Vector2I(10,6),
                        new Vector2I(11,5),new Vector2I(11,6),
                        new Vector2I(0,7),new Vector2I(1,7),
                        new Vector2I(0,8),new Vector2I(1,8),
                        new Vector2I(5,8),new Vector2I(6,8),
                        new Vector2I(5,9),new Vector2I(6,9),
                        new Vector2I(10,8),new Vector2I(11,8),
                        new Vector2I(10,9),new Vector2I(11,9),
                        new Vector2I(3,1),new Vector2I(3,0),
                        new Vector2I(4,1),new Vector2I(4,0),
                        new Vector2I(1,6),new Vector2I(1,7),
                        new Vector2I(2,6),new Vector2I(2,7),
                        new Vector2I(6,10),new Vector2I(6,11),
                        new Vector2I(7,10),new Vector2I(7,11),
                        new Vector2I(11,1),new Vector2I(11,0),
                        new Vector2I(0,5),new Vector2I(1,5),
                        new Vector2I(0,6),new Vector2I(1,6)}
),
// 地图 20
new MapInfo(
    new List<Vector2I>{new Vector2I(0,0),new Vector2I(11,11)},
    new List<Vector2I>{new Vector2I(1,1),new Vector2I(1,2),
                        new Vector2I(2,1),new Vector2I(2,2),
                        new Vector2I(3,4),new Vector2I(3,5),
                        new Vector2I(4,4),new Vector2I(4,5),
                        new Vector2I(6,7),new Vector2I(6,8),
                        new Vector2I(7,7),new Vector2I(7,8),
                        new Vector2I(9,2),new Vector2I(9,3),
                        new Vector2I(10,2),new Vector2I(10,3),
                        new Vector2I(4,10),new Vector2I(5,10),
                        new Vector2I(4,11),new Vector2I(5,11),
                        new Vector2I(7,5),new Vector2I(8,5),
                        new Vector2I(7,6),new Vector2I(8,6),
                        new Vector2I(3,8),new Vector2I(3,9),
                        new Vector2I(4,8),new Vector2I(4,9),
                        new Vector2I(2,11),new Vector2I(2,10),
                        new Vector2I(3,11),new Vector2I(3,10),
                        new Vector2I(8,10),new Vector2I(8,11),
                        new Vector2I(9,10),new Vector2I(9,11),
                        new Vector2I(5,2),new Vector2I(6,2),
                        new Vector2I(5,3),new Vector2I(6,3),
                        new Vector2I(8,1),new Vector2I(9,1),
                        new Vector2I(8,0),new Vector2I(9,0),
                        new Vector2I(10,5),new Vector2I(10,6),
                        new Vector2I(11,5),new Vector2I(11,6),
                        new Vector2I(0,7),new Vector2I(1,7),
                        new Vector2I(0,8),new Vector2I(1,8),
                        new Vector2I(5,8),new Vector2I(6,8),
                        new Vector2I(5,9),new Vector2I(6,9),
                        new Vector2I(10,8),new Vector2I(11,8),
                        new Vector2I(10,9),new Vector2I(11,9),
                        new Vector2I(3,1),new Vector2I(3,0),
                        new Vector2I(4,1),new Vector2I(4,0),
                        new Vector2I(1,6),new Vector2I(1,7),
                        new Vector2I(2,6),new Vector2I(2,7),
                        new Vector2I(6,10),new Vector2I(6,11),
                        new Vector2I(7,10),new Vector2I(7,11),
                        new Vector2I(11,1),new Vector2I(11,0),
                        new Vector2I(0,5),new Vector2I(1,5),
                        new Vector2I(0,6),new Vector2I(1,6)}
    ),
    // 罗姆之巅
    new MapInfo(
        new List<Vector2I>{new Vector2I(0,0),new Vector2I(10,10)},
        new List<Vector2I>{
            new Vector2I(0,0),new Vector2I(2,2),
            new Vector2I(8,8),new Vector2I(10,10),
            new Vector2I(0,10),new Vector2I(2,8),
            new Vector2I(8,2),new Vector2I(10,0),
        }
    )
        ];
    #endregion

    #region CardSelection


    #endregion
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

public struct CardsInfo
{
    public List<CardInfo> CardTypes { get; set; }

    public CardsInfo(CardInfo card1, CardInfo card2, CardInfo card3)
    {
        CardTypes.Add(card1);
        CardTypes.Add(card2);
        CardTypes.Add(card3);
    }
}
