namespace EESaga.Scripts.Data;

using Godot;
using System.Collections.Generic;

public static class GameData
{
    public static List<EnemyInfo> EnemyInfo =
    [
        new() { SlimeCount = 3 },
    ];
}

public struct EnemyInfo
{
    public int SlimeCount;
}
